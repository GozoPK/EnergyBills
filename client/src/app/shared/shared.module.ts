import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from "ngx-spinner";
import { FormsInputComponent } from './forms-input/forms-input.component';
import { LoginExpiredModalComponent } from './login-expired-modal/login-expired-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingComponent } from './paging/paging.component';
import { FormsSelectComponent } from './forms-select/forms-select.component';
import { DatePickerComponent } from './date-picker/date-picker.component';



@NgModule({
  declarations: [
    FormsInputComponent,
    LoginExpiredModalComponent,
    PagingComponent,
    FormsSelectComponent,
    DatePickerComponent,
  ],
  imports: [
    CommonModule,
    NgbCollapseModule,
    FormsModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-top-center'
    }),
    NgxSpinnerModule.forRoot({
      type: 'ball-spin-clockwise'
    }),
    BsDatepickerModule.forRoot()
  ],
  exports: [
    NgbCollapseModule,
    FormsInputComponent,
    LoginExpiredModalComponent,
    FormsModule,
    ReactiveFormsModule,
    PaginationModule,
    BsDropdownModule,
    ModalModule,
    ToastrModule,
    NgxSpinnerModule,
    BsDatepickerModule,
    PagingComponent,
    FormsSelectComponent,
    DatePickerComponent
  ]
})
export class SharedModule { }
