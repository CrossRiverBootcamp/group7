import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TransactionModel } from '../models/transaction.model copy';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  baseUrl: string = "/api/Transaction/";
  constructor(private _http: HttpClient) { }

  createTransaction(transaction: TransactionModel):Observable<boolean> {
    return this._http.post<boolean>(this.baseUrl,transaction)
  }
}
