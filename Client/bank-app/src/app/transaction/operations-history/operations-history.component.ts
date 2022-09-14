// import { AfterViewInit, Component } from '@angular/core';
// import { MatTableDataSource } from '@angular/material/table';
// import { OperationHistoryModel } from 'src/app/models/OperationHistory.model';
// import { CurrentUserService } from 'src/app/services/current-user.service';
// import { OperationsHistoryService } from 'src/app/services/operations-history.service';



// export class OperationsHistoryComponent implements  AfterViewInit {

//   operationsHistoryList: OperationHistoryModel[] = [];
//   displayedColumns: string[] = ['accountId', 'isDebit', 'amount', 'balance', 'date'];
//   dataSource!: MatTableDataSource<OperationHistoryModel>;
//   @ViewChild

//   constructor(private _operationsHistoryService: OperationsHistoryService, private _currentUserService: CurrentUserService) { }

//   ngAfterViewInit() {
//     let accountId = this._currentUserService.getAccountId();
//     this._operationsHistoryService.getOperationsHistory(accountId).subscribe(data=>{
//       this.operationsHistoryList=data;
//       this.dataSource = new MatTableDataSource<OperationHistoryModel>(this.operationsHistoryList);
//     })
//     this.dataSource.paginator = this.paginator;
//   }
// }
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { OperationHistoryModel } from 'src/app/models/operation-history.model';
import { TransactionDetailsModel } from 'src/app/models/transaction-details.model';
import { CurrentUserService } from 'src/app/services/current-user.service';
import { OperationsHistoryService } from 'src/app/services/operations-history.service';
import Swal from 'sweetalert2';

/**
 * @title Table with pagination
 */
@Component({
  selector: 'app-operations-history',
  templateUrl: './operations-history.component.html',
  styleUrls: ['./operations-history.component.scss']
})
export class OperationsHistoryComponent implements AfterViewInit {
  operationsHistoryList: OperationHistoryModel[] = [];
  transactionDetails: TransactionDetailsModel = {
    firstName: '',
    lastName: '',
    email: ''
  }
  displayedColumns: string[] = ['accountId', 'isDebit', 'amount', 'balance', 'date'];
  dataSource!: MatTableDataSource<OperationHistoryModel>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  constructor(private _operationsHistoryService: OperationsHistoryService, private _currentUserService: CurrentUserService) { }

  ngAfterViewInit() {
    let accountId = this._currentUserService.getAccountId();
    this._operationsHistoryService.getOperationsHistory(accountId).subscribe(data => {
      this.operationsHistoryList = data;
      this.dataSource = new MatTableDataSource<OperationHistoryModel>(this.operationsHistoryList);
      this.dataSource.paginator = this.paginator;
    })
  }
  getAccountInformation(accountId: number) {
    this._operationsHistoryService.getTransactionDetails(accountId).subscribe(data => {
      this.transactionDetails = data;
      Swal.fire({
        title:"Transaction Details",
        text:`from ${this.transactionDetails.firstName}  ${this.transactionDetails.firstName}\n ${this.transactionDetails.email}`
      })
    });
  }
}

