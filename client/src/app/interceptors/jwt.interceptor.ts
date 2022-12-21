import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { EMPTY, Observable, take } from 'rxjs';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  stopRequest: boolean = false;

  constructor(private accountService: AccountService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          if (this.isExpired(user.token)) {
            this.stopRequest = true;
          }
          
          request = request.clone({
            setHeaders: {
              "Authorization": `Bearer ${user.token}`
            }
          });
        }
      }
    });

    if (this.stopRequest) {
      this.router.navigate(['/login']);
      return EMPTY;
    }
    
    return next.handle(request);
  }

  isExpired(token: string) {
    const expireDate = (JSON.parse(atob(token.split('.')[1]))).exp;
    return (Math.floor((new Date).getTime() / 1000)) > expireDate;
  }
}
