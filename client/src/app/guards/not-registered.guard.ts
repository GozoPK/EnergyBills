import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { map, Observable } from 'rxjs';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})
export class NotRegistered implements CanActivate {

  constructor(private accountService: AccountService, private router: Router) { }

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map(user => {
        if (user) {
          if (user.isRegistered) {
            this.router.navigate(['/']);
            return false
          }
        }
        return true;
      })
    )
  }
  
}
