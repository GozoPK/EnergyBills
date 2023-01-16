import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserBillsComponent } from './user-bills/user-bills.component';
import { EnergyBillsRoutingModule } from './energy-bills-routing.module';



@NgModule({
  declarations: [
    UserBillsComponent
  ],
  imports: [
    CommonModule,
    EnergyBillsRoutingModule
  ]
})
export class EnergyBillsModule { }
