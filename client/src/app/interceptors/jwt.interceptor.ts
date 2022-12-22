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
import { LoginExpiredModalComponent } from '../modals/login-expired-modal/login-expired-modal.component';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  stopRequest: boolean = false;
  modalRef: BsModalRef<LoginExpiredModalComponent> = new BsModalRef<LoginExpiredModalComponent>();

  constructor(private accountService: AccountService, private router: Router, private modalService: BsModalService) {}

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
      this.openModal();
      return EMPTY;
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
      this.accountService.setCurrentUser(null, false);
      this.router.navigate(['/home']);
    });
  }

}
