import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);  //Inject HTTP client service
  baseUrl = 'https://localhost:5001/api/';
  currentUser = signal<User | null>(null);

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));   // Set a variable in local storage
          this.currentUser.set(user);                           // Set a signal
        }
      })
    )
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
