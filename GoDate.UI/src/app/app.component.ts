import { RouterOutlet } from '@angular/router';
import { Component, inject, OnInit } from '@angular/core';
import { NavComponent } from "./components/nav/nav.component";
import { AccountService } from './services/account.service';

@Component({
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [RouterOutlet ,NavComponent]
})
export class AppComponent implements OnInit {
  private accountService = inject(AccountService);

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);              // Convert string to JSON object
    this.accountService.currentUser.set(user);
  }

}
