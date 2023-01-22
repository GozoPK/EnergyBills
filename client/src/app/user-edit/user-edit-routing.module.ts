import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { UserEditShellComponent } from './user-edit-shell/user-edit-shell.component';
import { UserEditDetailsComponent } from './user-edit-details/user-edit-details.component';
import { UserEditBillsComponent } from './user-edit-bills/user-edit-bills.component';
import { UserEditProfileComponent } from './user-edit-profile/user-edit-profile.component';
import { UserEditSecurityComponent } from './user-edit-security/user-edit-security.component';

const routes: Routes = [
  { path: '', 
    component: UserEditShellComponent,
    children: [
      { path: 'profile', component: UserEditProfileComponent },
      { path: 'security', component: UserEditSecurityComponent },
      { path: 'details', component: UserEditDetailsComponent },
      { path: 'bills', component: UserEditBillsComponent },
      { path: '', redirectTo: 'profile', pathMatch: 'full' }
    ]
  }
];

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
export class UserEditRoutingModule { }
