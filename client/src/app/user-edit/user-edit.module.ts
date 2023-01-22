import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserEditShellComponent } from './user-edit-shell/user-edit-shell.component';
import { UserEditDetailsComponent } from './user-edit-details/user-edit-details.component';
import { UserEditBillsComponent } from './user-edit-bills/user-edit-bills.component';
import { UserEditRoutingModule } from './user-edit-routing.module';
import { SharedModule } from '../shared/shared.module';
import { UserEditSecurityComponent } from './user-edit-security/user-edit-security.component';
import { UserEditProfileComponent } from './user-edit-profile/user-edit-profile.component';



@NgModule({
  declarations: [
    UserEditShellComponent,
    UserEditDetailsComponent,
    UserEditBillsComponent,
    UserEditSecurityComponent,
    UserEditProfileComponent
  ],
  imports: [
    CommonModule,
    UserEditRoutingModule,
    SharedModule
  ]
})
export class UserEditModule { }
