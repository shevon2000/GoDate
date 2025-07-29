import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../models/user';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);                                             // Alternative to constructor injection
  baseUrl = environment.apiUrl;
  currentUser = signal<User | null>(null);                                       // Initial value is null

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(    // Sends the request and returns an Observable<User>
      map(user => {
        if (user) {                                                             // If the response contains a User object
          this.setCurrentUser(user);   
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(    
      map(user => {
        if (user) {                                                             
          this.setCurrentUser(user);                                          
        }
        return user;                                                           
      })
    )
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
  
}


// Services are singeleton and instantiated when the application starts, and disposed when the application is closed.
// We can inject services into needed components.
// Signal is a wrapper around a value that notifies interested parties when the value changes. 