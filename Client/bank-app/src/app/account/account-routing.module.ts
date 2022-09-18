import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogInActivateGuard } from '../shared/log-in-activate.guard';
import { AccountInfoComponent } from './account-info/account-info.component';
import { RegisterComponent } from './register/register.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';

const routes: Routes = [
  { path: 'register', component: RegisterComponent },
  { path: 'account-info', component: AccountInfoComponent,canActivate:[LogInActivateGuard] },
  { path: 'operation-history', component: OperationsHistoryComponent, canActivate: [LogInActivateGuard] }
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
