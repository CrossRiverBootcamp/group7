import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmailVerificationService {

  baseUrl: string = "/api/EmailVerification/";
  constructor(private _http: HttpClient) { }

  verifyCustomer(email: string):Observable<boolean> {
    return this._http.post<boolean>(this.baseUrl,JSON.stringify(email),{headers: {'Content-Type': 'application/json'}})
  }
}
