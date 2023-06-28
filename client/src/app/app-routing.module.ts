import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { TaxisnetLoginComponent } from './account/taxisnet-login/taxisnet-login.component';
import { PageNotFoundComponent } from './errors/page-not-found/page-not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { IsAdminGuard } from './guards/is-admin.guard';
import { IsMemberGuard } from './guards/is-member.guard';
import { TaxisRegistered } from './guards/taxis-registered.guard';
import { HomeComponent } from './intro/home/home.component';
import { IntroComponent } from './intro/intro.component';
import { ExitRegisterGuard } from './guards/exit-register.guard';

const routes: Routes = [
  { path: '', component: IntroComponent, 
    children: [
      { path: 'home', component: HomeComponent },
      { path: 'register', component: RegisterComponent, canActivate: [TaxisRegistered], canDeactivate: [ExitRegisterGuard] },
      { 
        path: 'energy-bills', 
        loadChildren: () => import('./energy-bills/energy-bills.module').then(m => m.EnergyBillsModule),
        canActivate: [IsMemberGuard]
      },
      {
        path: 'user',
        loadChildren: () => import('./user-edit/user-edit.module').then(m => m.UserEditModule),
        canActivate: [IsMemberGuard]
      },
      {
        path: 'admin',
        loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
        canActivate: [IsAdminGuard]
      },
      { path: '', redirectTo: 'home', pathMatch: 'full' }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: 'taxisnet-login', component: TaxisnetLoginComponent },
  { path: 'not-found', component: PageNotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
