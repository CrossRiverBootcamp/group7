import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { registeringModel } from '../models/registering.model';

@Injectable({
  providedIn: 'root'
})
export class EmailVerificationService {

  baseUrl: string = "/api/EmailVerification/";
  constructor(private _http: HttpClient) { }

  verifyCustomer(customer: registeringModel):Observable<boolean> {
    return this._http.post<boolean>(this.baseUrl,customer)
  }
}
