import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs';
import { AccountModel } from '../models/account.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl: string = "/api/Account/";
  // headers= new HttpHeaders()
  // .set( 'Authorization', 'Bearer ' + 'userToken')

  constructor(private _http: HttpClient) { }

  getAccountInfoById(accountId: number):Observable<AccountModel> {
    return this._http.get<AccountModel>(this.baseUrl+accountId)
  }
  createNewAccount(email: string):Observable<boolean> {
    return this._http.post<boolean>(this.baseUrl,JSON.stringify(email),{headers: {'Content-Type': 'application/json'}})
  }
  
}
