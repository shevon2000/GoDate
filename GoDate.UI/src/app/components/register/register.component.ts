import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  //usersFromHomeComponent = input.required<any>();  // Parent to child communication, using signals
  cancelRegister = output<boolean>();               // Child to parent communication, using signals
  model: any = {}

  register() {
    this.accountService.register(this.model).subscribe({
      next: response => {
        console.log(response);
        this.cancel();
      },
      error: error => console.error(error)
    });
  }

  cancel() {
    this.cancelRegister.emit(false);                // Emit false to parent component to close the register form
  }

}
