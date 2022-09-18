import { NumberInput } from '@angular/cdk/coercion';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { OperationHistoryModel } from 'src/app/models/operation-history.model';
import { TransactionDetailsModel } from 'src/app/models/transaction-details.model';
import { CurrentUserService } from 'src/app/services/current-user.service';
import { OperationsHistoryService } from 'src/app/services/operations-history.service';
import Swal from 'sweetalert2';


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
  numOfOperaitons!: number;
  numberOfRecords!: NumberInput;
  pageNumber: NumberInput = 0;
  pageSizeOptions: number[] = [5, 10, 15];
  dataSource= new MatTableDataSource<OperationHistoryModel>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  
  constructor(private _operationsHistoryService: OperationsHistoryService, private _currentUserService: CurrentUserService) { }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  ngOnInit(): void {
    this.uploudData(); 
  }
  uploudData(pageNumber: number = 0, numberOfRecords: number = 5) {
    let accountId = this._currentUserService.getAccountId();
    //???
    this.numOfOperaitons = 11;
    this._operationsHistoryService.getOperationsHistory(accountId, pageNumber, numberOfRecords).subscribe(data => {
      //if data
      this.operationsHistoryList = data;
      this.dataSource.data = data;
      this.paginator.pageIndex = this.pageNumber;
      setTimeout(() => {
        this.paginator.pageIndex = this.pageNumber;
        this.paginator.length =this.numOfOperaitons;
      })})
  }

  getNextPage(event: PageEvent) {
    this.pageNumber = event.pageIndex;
    this.numberOfRecords = event.pageSize;
    this.uploudData(this.pageNumber, this.numberOfRecords)
  }
 
  getAccountInformation(accountId: number) {
    this._operationsHistoryService.getTransactionDetails(accountId).subscribe(data => {
      this.transactionDetails = data;
      Swal.fire({
        title: "Transaction Details:",
        text: `${data.firstName}  ${this.transactionDetails.lastName}\n ${this.transactionDetails.email}`
      })
    });
  }
}