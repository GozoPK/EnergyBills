import { Injectable } from '@angular/core';
import { Router, RouterStateSnapshot } from '@angular/router';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { map, Observable } from 'rxjs';
import { UserBill } from '../models/userBill';
import { ConfirmModalComponent } from '../shared/modals/confirm-modal/confirm-modal.component';
import { EditBillModalComponent } from '../shared/modals/edit-bill-modal/edit-bill-modal.component';
import { ModalComponent } from '../shared/modals/modal/modal.component';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  modalRef = new BsModalRef<ModalComponent>();
  confirmModalRef = new BsModalRef<ConfirmModalComponent>();
  editBillModal = new BsModalRef<EditBillModalComponent>();

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

  openEditBillModal(bill: UserBill): Observable<boolean> {
    const config: ModalOptions = {
      class: 'modal-dialog-centered',
      backdrop: 'static',
      keyboard: false,
      initialState: {
        bill: bill
      }    
    };

    this.editBillModal = this.bsdModalService.show(EditBillModalComponent, config);
    return this.editBillModal.onHide!.pipe(
      map(() => {
        return this.editBillModal.content!.billChanged
      })
    );
  }
}
