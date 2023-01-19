import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable, of } from 'rxjs';
import { AddBillComponent } from '../energy-bills/add-bill/add-bill.component';
import { ModalService } from '../services/modal.service';

@Injectable({
  providedIn: 'root'
})
export class PreventExitGuard implements CanDeactivate<unknown> {

  constructor(private modalService: ModalService) { }

  canDeactivate(component: AddBillComponent): Observable<boolean> {
    if (component.createForm.dirty) {
      return this.modalService.openConfirmModal('Είστε σίγουρος ότι θέλετε να αποχωρήσετε;');
    }
    return of(true);
  }
  
}
