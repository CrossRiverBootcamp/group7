import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountModel } from '../models/account.model';
import { CustomerModel } from '../models/customer.model';
import { LogInModel } from '../models/logIn.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  baseUrl: string = "/api/Customer/";
  constructor(private _http: HttpClient) { }

  logIn(user: LogInModel):Observable<number> {
    return this._http.post<number>(this.baseUrl,user)
  }

  register(customer: CustomerModel):Observable<boolean> {
    return this._http.post<boolean>(this.baseUrl,customer)
  }
}
