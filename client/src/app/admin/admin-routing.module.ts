import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { BillsListComponent } from './bills-list/bills-list.component';
import { BillEditComponent } from './bill-edit/bill-edit.component';
import { CreateAdminComponent } from './create-admin/create-admin.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

const routes: Routes = [
  { path: '', 
    component: AdminPanelComponent,
    children: [
      { path: 'bills', component: BillsListComponent },
      { path: 'bills/:id', component: BillEditComponent },
      { path: 'create', component: CreateAdminComponent },
      { path: 'change-password', component: ChangePasswordComponent },
      { path: '', redirectTo: 'bills', pathMatch: 'full' }
    ]
  }
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
export class AdminRoutingModule { }
