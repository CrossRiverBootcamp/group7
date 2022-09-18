import { NgModule } from '@angular/core';
import { LogInComponent } from './log-in/log-in.component';
import { RegisterComponent } from './register/register.component';
import { AccountInfoComponent } from './account-info/account-info.component';
import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { SharedModule } from '../shared/shared.module';
import { AccountRoutingModule } from './account-routing.module';
import { EmailVerifictionComponent } from './email-verifiction/email-verifiction.component';

@NgModule({
  declarations: [
    LogInComponent,
    RegisterComponent,
    AccountInfoComponent,
    EmailVerifictionComponent
  ],
  imports: [
    SharedModule,
    AngularMaterialModule,
    AccountRoutingModule
  ]
})
export class AccountModule { }
