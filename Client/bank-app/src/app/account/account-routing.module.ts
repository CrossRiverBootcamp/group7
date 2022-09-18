import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogInActivateGuard } from '../shared/log-in-activate.guard';
import { AccountInfoComponent } from './account-info/account-info.component';
import { LogInComponent } from './log-in/log-in.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  //{ path: '', component:LogInComponent }, 
  //{ path: '', pathMatch: 'full', redirectTo: 'account/logIn' }, 
  { path: 'logIn', component: LogInComponent},
  { path: 'register', component: RegisterComponent },
  { path: 'account-info', component: AccountInfoComponent,canActivate:[LogInActivateGuard] },
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
