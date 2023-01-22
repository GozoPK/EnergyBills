import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from "ngx-spinner";
import { FormsInputComponent } from './forms-input/forms-input.component';
import { ModalComponent } from './modals/modal/modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingComponent } from './paging/paging.component';
import { FormsSelectComponent } from './forms-select/forms-select.component';
import { DatePickerComponent } from './date-picker/date-picker.component';
import { ConfirmModalComponent } from './modals/confirm-modal/confirm-modal.component';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { EditBillModalComponent } from './modals/edit-bill-modal/edit-bill-modal.component';



@NgModule({
  declarations: [
    FormsInputComponent,
    ModalComponent,
    PagingComponent,
    FormsSelectComponent,
    DatePickerComponent,
    ConfirmModalComponent,
    EditBillModalComponent,
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
    BsDatepickerModule.forRoot(),
    ButtonsModule.forRoot()
  ],
  exports: [
    NgbCollapseModule,
    FormsInputComponent,
    ModalComponent,
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
    DatePickerComponent,
    ButtonsModule
  ]
})
export class SharedModule { }
