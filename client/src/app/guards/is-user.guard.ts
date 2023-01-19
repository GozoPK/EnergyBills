import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { delay, finalize, map, Observable } from 'rxjs';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})
export class IsUserGuard implements CanActivate {

  constructor(private accountService: AccountService, private router: Router) { }

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map(user => {
        if (user) 
          return true;
        this.router.navigate(['/']);
        return false;
      })
    );
  }
  
}
