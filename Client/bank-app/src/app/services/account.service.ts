import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { AccountModel } from '../models/account.model';
import { registeringModel } from '../models/registering.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl: string = "/api/Account/";
  constructor(private _http: HttpClient) { }

  getAccountInfoById(accountId: number):Observable<AccountModel> {
    return this._http.get<AccountModel>(this.baseUrl+accountId)
  }
  createNewAccount(customer: registeringModel):Observable<boolean> {
    return this._http.post<boolean>(this.baseUrl,customer)
  }
}
