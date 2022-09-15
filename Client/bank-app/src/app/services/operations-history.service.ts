import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OperationHistoryModel } from '../models/operation-history.model';
import { TransactionDetailsModel } from '../models/transaction-details.model';

@Injectable({
  providedIn: 'root'
})
export class OperationsHistoryService {

  baseUrl: string = "/api/OperationHistory/";
  constructor(private _http: HttpClient) { }

  getOperationsHistory(accountId: number):Observable<OperationHistoryModel[]> {
    return this._http.get<OperationHistoryModel[]>(this.baseUrl+accountId)
  }

  getTransactionDetails(accountId: number):Observable<TransactionDetailsModel> {
    return this._http.get<TransactionDetailsModel>(this.baseUrl+"transactionDetailes/"+accountId)
  }
}
