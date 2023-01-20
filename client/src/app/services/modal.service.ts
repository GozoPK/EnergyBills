import { Injectable } from '@angular/core';
import { Router, RouterStateSnapshot } from '@angular/router';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { map, Observable } from 'rxjs';
import { ConfirmModalComponent } from '../shared/modals/confirm-modal/confirm-modal.component';
import { ModalComponent } from '../shared/modals/modal/modal.component';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  modalRef = new BsModalRef<ModalComponent>();
  confirmModalRef = new BsModalRef<ConfirmModalComponent>();

  constructor(private bsdModalService: BsModalService, private accountService: AccountService, 
    private router: Router) { }

  openLoginExpiredModal(returnUrl: string) {
    const config: ModalOptions = {
      class: 'modal-dialog-centered',
      initialState: {
        imgUrl: './assets/warning.jpg',
        text: 'Η σύνδεση σας έληξε, παρακαλώ συνδεθείτε ξανά.'
      }     
    };

    this.modalRef = this.bsdModalService.show(ModalComponent, config);
    this.modalRef.onHide?.subscribe(() => {
      this.accountService.logout();
      this.router.navigate(['/login'], {queryParams: { returnUrl }});
    });
  }

  operModal(text: string) {
    const config: ModalOptions = {
      class: 'modal-dialog-centered',
      initialState: {
        text: text
      }     
    };

    this.modalRef = this.bsdModalService.show(ModalComponent, config);
  }

  openRegisterModal(text: string) {
    const config: ModalOptions = {
      class: 'modal-dialog-centered',
      initialState: {
        text: text
      }     
    };

    this.modalRef = this.bsdModalService.show(ModalComponent, config);
    this.modalRef.onHide?.subscribe(() => {
      this.router.navigate(['/']);
    });
  }

  openConfirmModal(text: string): Observable<boolean> {
    const config: ModalOptions = {
      class: 'modal-dialog-centered',
      initialState: {
        text: text
      }     
    };

    this.confirmModalRef = this.bsdModalService.show(ConfirmModalComponent, config);
    return this.confirmModalRef.onHide!.pipe(
      map(() => {
        return this.confirmModalRef.content!.confirmResult
      })
    )
  }
}
