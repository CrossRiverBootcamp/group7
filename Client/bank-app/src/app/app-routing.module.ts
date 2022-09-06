import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountInfoComponent } from './customer/account-info/account-info.component';
import { LogInComponent } from './customer/log-in/log-in.component';
import { RegisterComponent } from './customer/register/register.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'logIn' },
  { path: 'logIn', component: LogInComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'account-info', component: AccountInfoComponent },
  // { path: '**', component: AccountInfoComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
