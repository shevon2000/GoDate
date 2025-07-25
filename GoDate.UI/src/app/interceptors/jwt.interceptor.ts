import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../services/account.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => { // req is a mutable object, so we can clone it
  const accountService = inject(AccountService); 

  if (accountService.currentUser()) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${accountService.currentUser()?.token}` 
      }
    })
  }
  
  return next(req);   // Send new request with the authorization headers
};
