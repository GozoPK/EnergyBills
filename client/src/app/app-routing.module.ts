import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { TaxisnetLoginComponent } from './account/taxisnet-login/taxisnet-login.component';
import { NotRegistered } from './guards/not-registered.guard';
import { HomeComponent } from './intro/home/home.component';
import { IntroComponent } from './intro/intro.component';
import { BillsComponent } from './members/bills/bills.component';

const routes: Routes = [
  { path: '', component: IntroComponent, 
    children: [
      { path: 'home', component: HomeComponent },
      { path: 'register', component: RegisterComponent, canActivate: [NotRegistered] },
      { path: 'members', component: BillsComponent },
      { path: '', redirectTo: 'home', pathMatch: 'full' }
    ]
  },
  { path: 'login', component: LoginComponent, canActivate: [NotRegistered] },
  { path: 'taxisnet-login', component: TaxisnetLoginComponent, canActivate: [NotRegistered]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
