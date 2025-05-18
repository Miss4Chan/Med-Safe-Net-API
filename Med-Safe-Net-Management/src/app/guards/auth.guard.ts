import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);
  const router = inject(Router);

  if (accountService.currentUser()) {
    return true;
  } else {
    toastr.error('Session Expired');
    router.navigateByUrl('/login');
    return false;  
  }
};
