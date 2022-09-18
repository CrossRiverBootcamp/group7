import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogInComponent } from './log-in/log-in.component';
import { PageNotFoundComponent } from './shared/page-not-found/page-not-found.component';


const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'logIn' },
  { path:'logIn', component: LogInComponent},
  { path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule) },
  { path: 'transaction', loadChildren: () => import('./transaction/transaction.module').then(m => m.TransactionModule) },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
