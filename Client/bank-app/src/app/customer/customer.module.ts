import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogInComponent } from './log-in/log-in.component';
import { RegisterComponent } from './register/register.component';
import { AccountInfoComponent } from './account-info/account-info.component';
import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    LogInComponent,
    RegisterComponent,
    AccountInfoComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    AngularMaterialModule
  ]
})
export class CustomerModule { }
