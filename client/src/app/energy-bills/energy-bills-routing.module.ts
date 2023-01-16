import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { UserBillsComponent } from './user-bills/user-bills.component';

const routes: Routes = [
  { path: '', component: UserBillsComponent },
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class EnergyBillsRoutingModule { }
