import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgbCollapseModule,
    BsDropdownModule.forRoot()
  ],
  exports: [
    NgbCollapseModule,
    BsDropdownModule
  ]
})
export class SharedModule { }
