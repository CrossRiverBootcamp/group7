import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionComponent } from './add-transaction/transaction.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    TransactionComponent,
    OperationsHistoryComponent,
   
  ],
  imports: [
    CommonModule,
    AngularMaterialModule,
    SharedModule
  ]
})
export class TransactionModule { }
