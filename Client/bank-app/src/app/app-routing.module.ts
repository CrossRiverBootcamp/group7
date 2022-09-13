import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountInfoComponent } from './account/account-info/account-info.component';
import { LogInComponent } from './account/log-in/log-in.component';
import { RegisterComponent } from './account/register/register.component';
import { PageNotFoundComponent } from './shared/page-not-found/page-not-found.component';
import { TransactionComponent } from './transaction/add-transaction/transaction.component';
import { OperationsHistoryComponent } from './transaction/operations-history/operations-history.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'logIn' },
  { path: 'logIn', component: LogInComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'account-info', component: AccountInfoComponent },
  { path: 'add-transaction', component: TransactionComponent },
  { path: 'operation-history', component: OperationsHistoryComponent },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
