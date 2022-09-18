import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { DebitOrCreditPipe } from './debit-or-credit.pipe';
import { CommonModule } from '@angular/common';


const sharedModule = [
  FormsModule,
  ReactiveFormsModule,
  HttpClientModule,
  RouterModule,
  CommonModule
]
@NgModule({
  declarations: [
    PageNotFoundComponent,
    DebitOrCreditPipe
  ],
  imports: [
    ...sharedModule
  ],
  exports: [
    ...sharedModule,
    DebitOrCreditPipe
  ]
})
export class SharedModule { }
