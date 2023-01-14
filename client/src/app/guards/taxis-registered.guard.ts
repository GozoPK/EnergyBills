import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { map, Observable, take } from 'rxjs';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})
export class TaxisRegistered implements CanActivate {

  constructor(private accountService: AccountService, private router: Router) { }

  canActivate(): boolean {
    if (this.accountService.user) return true;
    this.router.navigate(['/']);
    return false;
  }
  
}
