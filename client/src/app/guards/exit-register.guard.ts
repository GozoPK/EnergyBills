import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable, of, tap } from 'rxjs';
import { ModalService } from '../services/modal.service';
import { RegisterComponent } from '../account/register/register.component';

@Injectable({
  providedIn: 'root'
})
export class ExitRegisterGuard implements CanDeactivate<unknown> {

  constructor(private modalService: ModalService) { }

  canDeactivate(component: RegisterComponent): Observable<boolean> {
    const token = localStorage.getItem('token');
    if (component.registerForm.dirty && !component.isSuccesfulRegister) {
      return this.modalService.openConfirmModal('Είστε σίγουρος ότι θέλετε να αποχωρήσετε;')
        .pipe(tap(response => {
          if (response) {
            if (token) localStorage.removeItem('token');
          }
        }));
    }
    if (token && !component.isSuccesfulRegister) {
      localStorage.removeItem('token');
    }
    return of(true);
  }
  
}
