import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { EMPTY, Observable } from 'rxjs';
import { ModalService } from '../services/modal.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  stopRequest: boolean = false;

  constructor(private modalService: ModalService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.url.includes('login')) {
      return next.handle(request);
    }

    const token = localStorage.getItem('token');

    if (token) {
      if (this.isExpired(token)) {
        this.modalService.openLoginExpiredModal();
        return EMPTY;
      }

      request = request.clone({
        setHeaders: {
          "Authorization": `Bearer ${token}`
        }
      });
    }
    
    return next.handle(request);
  }

  isExpired(token: string) {
    const expireDate = (JSON.parse(atob(token.split('.')[1]))).exp;
    return (Math.floor((new Date).getTime() / 1000)) > expireDate;
  }

}
