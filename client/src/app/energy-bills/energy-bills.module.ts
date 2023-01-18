import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserBillsComponent } from './user-bills/user-bills.component';
import { EnergyBillsRoutingModule } from './energy-bills-routing.module';
import { SharedModule } from '../shared/shared.module';
import { FilterFormComponent } from './filter-form/filter-form.component';



@NgModule({
  declarations: [
    UserBillsComponent,
    FilterFormComponent
  ],
  imports: [
    CommonModule,
    EnergyBillsRoutingModule,
    SharedModule
  ]
})
export class EnergyBillsModule { }
