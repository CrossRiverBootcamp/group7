import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogInActivateGuard } from '../shared/log-in-activate.guard';
import { TransactionComponent } from './add-transaction/transaction.component';
import { OperationsHistoryComponent } from '../account/operations-history/operations-history.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'add-transaction' },
  { path: 'add-transaction', component: TransactionComponent, canActivate: [LogInActivateGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransactionRoutingModule { }
