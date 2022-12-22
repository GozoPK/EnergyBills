import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from "ngx-spinner";



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgbCollapseModule,
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
    BsDropdownModule,
    ModalModule,
    ToastrModule,
    NgxSpinnerModule
  ]
})
export class SharedModule { }
