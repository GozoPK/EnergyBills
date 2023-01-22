import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { TaxisnetLoginComponent } from './account/taxisnet-login/taxisnet-login.component';
import { PageNotFoundComponent } from './errors/page-not-found/page-not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { IsUserGuard } from './guards/is-user.guard';
import { TaxisRegistered } from './guards/taxis-registered.guard';
import { HomeComponent } from './intro/home/home.component';
import { IntroComponent } from './intro/intro.component';
import { BillsDataResolver } from './resolvers/bills-data.resolver';

const routes: Routes = [
  { path: '', component: IntroComponent, 
    children: [
      { path: 'home', component: HomeComponent },
      { path: 'register', component: RegisterComponent, canActivate: [TaxisRegistered] },
      { 
        path: 'energy-bills', 
        loadChildren: () => import('./energy-bills/energy-bills.module').then(m => m.EnergyBillsModule),
        canActivate: [IsUserGuard]
      },
      {
        path: 'user',
        loadChildren: () => import('./user-edit/user-edit.module').then(m => m.UserEditModule),
        resolve: { savedBills: BillsDataResolver },
        canActivate: [IsUserGuard]
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
