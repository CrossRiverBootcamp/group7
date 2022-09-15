import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { TransactionModel } from 'src/app/models/transaction.model';
import { CurrentUserService } from 'src/app/services/current-user.service';
import { TransactionService } from 'src/app/services/transaction.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})
export class TransactionComponent implements OnInit {

  TransactionForm: FormGroup = new FormGroup({
    toAccount: new FormControl(0, Validators.required),
    amount: new FormControl("", [Validators.required, Validators.min(1), Validators.max(1000000)]),
    date: new FormControl(new Date(), Validators.required),
    status: new FormControl("", Validators.required),
    failureReason: new FormControl("", Validators.required)
  });
  succses: boolean = false;

  constructor(private _transactionService: TransactionService, private _currentUserService: CurrentUserService) { }

  ngOnInit(): void {
  }
  sendTransaction() {
    let accountId = this._currentUserService.getAccountId();
    let transaction: TransactionModel = {
      fromAccountId: accountId,
      toAccountId: this.TransactionForm.controls["toAccount"].value,
      amount: this.TransactionForm.controls["amount"].value,
      date: new Date()
    }
    this._transactionService.createTransaction(transaction).subscribe(data => {
      this.succses = data;
      if (this.succses) {
        this.TransactionForm.controls["status"].setValue("SUCCESS");
        Swal.fire({
          text: "the transaction has been created:)",
          icon: "success",
        })
      }

      else {
        this.TransactionForm.controls["status"].setValue("Faild");
        let failureReason = this.TransactionForm.controls["failureReason"].value()
        Swal.fire({
          title: "Oppps...",
          text: "the transaction failed becuse " + failureReason,
          icon: "error",
          cancelButtonText: "Click her to register"
        })
      }
    });
  }
}

