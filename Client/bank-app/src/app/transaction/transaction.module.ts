import { NgModule } from '@angular/core';
import { TransactionComponent } from './add-transaction/transaction.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { SharedModule } from '../shared/shared.module';
import { TransactionRoutingModule } from './transaction-routing.module';


@NgModule({
  declarations: [
    TransactionComponent,
    OperationsHistoryComponent
  ],
  imports: [
    AngularMaterialModule,
    SharedModule,
    TransactionRoutingModule
  ]
})
export class TransactionModule { }
