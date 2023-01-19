import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { UserBillsComponent } from './user-bills/user-bills.component';
import { AddBillComponent } from './add-bill/add-bill.component';
import { PreventExitGuard } from '../guards/prevent-exit.guard';

const routes: Routes = [
  { path: '', component: UserBillsComponent },
  { path: 'add-bill', component: AddBillComponent, canDeactivate: [PreventExitGuard] }
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
