import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { AdminRoutingModule } from './admin-routing.module';
import { BillsListComponent } from './bills-list/bills-list.component';
import { BillEditComponent } from './bill-edit/bill-edit.component';
import { CreateAdminComponent } from './create-admin/create-admin.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    AdminPanelComponent,
    BillsListComponent,
    BillEditComponent,
    CreateAdminComponent,
    ChangePasswordComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule
  ]
})
export class AdminModule { }
