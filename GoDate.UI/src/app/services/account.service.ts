import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../models/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);                                             // Alternative to constructor injection
  baseUrl = 'https://localhost:5001/api/';
  currentUser = signal<User | null>(null);                                       // Initial value is null

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(    // Sends the request and returns an Observable<User>
      map(user => {
        if (user) {                                                             // If the response contains a User object
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(    
      map(user => {
        if (user) {                                                             
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;                                                           
      })
    )
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
  
}


// Services are singeleton and instantiated when the application starts, and disposed when the application is closed.
// We can inject services into needed components.
// Signal is a wrapper around a value that notifies interested parties when the value changes. 