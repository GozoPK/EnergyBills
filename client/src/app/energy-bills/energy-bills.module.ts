import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserBillsComponent } from './user-bills/user-bills.component';
import { EnergyBillsRoutingModule } from './energy-bills-routing.module';
import { SharedModule } from '../shared/shared.module';
import { FilterFormComponent } from './filter-form/filter-form.component';
import { AddBillComponent } from './add-bill/add-bill.component';



@NgModule({
  declarations: [
    UserBillsComponent,
    FilterFormComponent,
    AddBillComponent
  ],
  imports: [
    CommonModule,
    EnergyBillsRoutingModule,
    SharedModule
  ]
})
export class EnergyBillsModule { }
