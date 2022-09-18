import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountModel } from 'src/app/models/account.model';
import { AccountService } from 'src/app/services/account.service';
import { CurrentUserService } from 'src/app/services/current-user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.scss']
})
export class AccountInfoComponent implements OnInit {
  unexeptedErrorAlert = Swal.mixin({
    title: "Oppps...",
    text: "we are sorry... \n we try to fix it",
    icon: "error"
  });
  account!: AccountModel;
  constructor(private _router: Router, private _accountService: AccountService, private _currentUserService: CurrentUserService) { }

  ngOnInit(): void {
    let accountId = this._currentUserService.getAccountId();
    this._accountService.getAccountInfoById(accountId).subscribe(data => {
      if (data) {
        this.account = data;
      }
      else {
        this.unexeptedErrorAlert.fire();
      }
    }, error => {
      if (error.status == 510) {
        this.unexeptedErrorAlert.fire();
      }
      if (error.status == 401) {
        this._router.navigate(['/login']);
      }
    })
  }
}
