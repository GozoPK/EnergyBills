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
import { LoginExpiredModalComponent } from '../shared/login-expired-modal/login-expired-modal.component';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  stopRequest: boolean = false;
  modalRef: BsModalRef<LoginExpiredModalComponent> = new BsModalRef<LoginExpiredModalComponent>();

  constructor(private accountService: AccountService, private router: Router, private modalService: BsModalService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.url.includes('login')) {
      return next.handle(request);
    }

    const token = localStorage.getItem('token');

    if (token) {
      if (this.isExpired(token)) {
        this.openModal();
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

  openModal() {
    const config: ModalOptions = {
      class: 'model-dialog-centered',
      initialState: {
        imgUrl: './assets/warning.jpg',
        text: 'Η σύνδεση σας έληξε, παρακαλώ συνδεθείτε ξανά.'
      }     
    };

    this.modalRef = this.modalService.show(LoginExpiredModalComponent, config);
    this.modalRef.onHide?.subscribe(() => {
      this.accountService.setCurrentUser(null);
      this.router.navigate(['/home']);
    });
  }

}
