import { Component, OnInit } from '@angular/core';
import { AccountModel } from 'src/app/models/account.model';
import { AccountService } from 'src/app/services/account.service';
import { CurrentUserService } from 'src/app/services/current-user.service';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.scss']
})
export class AccountInfoComponent implements OnInit {

  account!: AccountModel;
  constructor(private _accountService: AccountService,private _currentUserService: CurrentUserService) { }

  ngOnInit(): void {
    let accountId = this._currentUserService.getAccountId();
    this._accountService.getAccountInfoById(accountId).subscribe(data => {
      this.account = data;
    })
  }
}
