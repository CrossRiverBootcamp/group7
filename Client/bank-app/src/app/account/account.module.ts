import { NgModule } from '@angular/core';
import { RegisterComponent } from './register/register.component';
import { AccountInfoComponent } from './account-info/account-info.component';
import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { SharedModule } from '../shared/shared.module';
import { AccountRoutingModule } from './account-routing.module';
import { EmailVerifictionComponent } from './email-verifiction/email-verifiction.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';

@NgModule({
  declarations: [
    RegisterComponent,
    AccountInfoComponent,
    EmailVerifictionComponent,
    OperationsHistoryComponent
  ],
  imports: [
    SharedModule,
    AngularMaterialModule,
    AccountRoutingModule
  ]
})
export class AccountModule { }
