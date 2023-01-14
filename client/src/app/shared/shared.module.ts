import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from "ngx-spinner";
import { FormsInputComponent } from './forms-input/forms-input.component';
import { LoginExpiredModalComponent } from './login-expired-modal/login-expired-modal.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FormsInputComponent,
    LoginExpiredModalComponent,
  ],
  imports: [
    CommonModule,
    NgbCollapseModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-top-center'
    }),
    NgxSpinnerModule.forRoot({
      type: 'ball-spin-clockwise'
    })
  ],
  exports: [
    NgbCollapseModule,
    FormsInputComponent,
    LoginExpiredModalComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    ModalModule,
    ToastrModule,
    NgxSpinnerModule
  ]
})
export class SharedModule { }
