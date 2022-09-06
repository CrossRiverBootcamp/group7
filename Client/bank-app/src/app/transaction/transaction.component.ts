import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Status } from '../models/transaction.model copy';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})
export class TransactionComponent implements OnInit {

  TransactionForm: FormGroup = new FormGroup({
    fromAccount: new FormControl(0, Validators.required),
    toAccount: new FormControl(0, Validators.required),
    amount: new FormControl("", [Validators.required,Validators.min(1), Validators.max(1000000)]),
    date: new FormControl(Date.now, Validators.required),
    status: new FormControl(Status.processing, Validators.required),
    failureReason: new FormControl("", Validators.required)
  });
  faild:boolean=false;
  constructor() { }

  ngOnInit(): void {
  }
  sendTransaction()
  {

  }

}
