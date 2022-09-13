import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';


const sharedModule = [
  BrowserAnimationsModule,
  BrowserModule,
  HttpClientModule,
  FormsModule,
  ReactiveFormsModule,
  RouterModule
]
@NgModule({
  declarations: [
    PageNotFoundComponent
  ],
  imports: [
    CommonModule,
    ...sharedModule
  ],
  exports: [
    ...sharedModule
  ]
})
export class SharedModule { }
