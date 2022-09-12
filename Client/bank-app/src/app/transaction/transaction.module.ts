import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionComponent } from './transaction/transaction.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';



@NgModule({
  declarations: [
    TransactionComponent,
    OperationsHistoryComponent
  ],
  imports: [
    CommonModule
  ]
})
export class TransactionModule { }
