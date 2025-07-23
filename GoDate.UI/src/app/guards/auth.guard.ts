import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../services/account.service';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  if (accountService.currentUser()) {
    return true;
  }
  else {
    toastr.error('You shall not pass!');
    return false;
  }
};



// CanActivateFn - If returns true, navigation continues else, navigation is cancelled.
// (So we can use this to protect our routes)
// As this is a JS function, not a class, we cannot use constructor injection approach.

// ** But we primarily concern about API end ppoints when securing. This is just use as a UI feature to hide the things from user.
// ** As this a JS based app, program downloads all the components and services. So we can use this to hide the components from user. 