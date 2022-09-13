import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomerModel } from 'src/app/models/customer.model';
import { LogInModel } from 'src/app/models/logIn.model';
import { AccountService } from 'src/app/services/account.service';
import { CurrentUserService } from 'src/app/services/current-user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  newCustomer: CustomerModel = {
    id: 0,
    firstName: '',
    lastName: '',
    email: '',
    password: ''
  };
  RegisterForm: FormGroup = new FormGroup({
    firstName: new FormControl("", [Validators.required,Validators.minLength(2)]),
    lastName: new FormControl("", [Validators.required,Validators.minLength(2)]),
    email: new FormControl("", [Validators.required,Validators.email]),
    password: new FormControl("", [Validators.required,Validators.minLength(4)])
  });
  constructor(private _accountService: AccountService, private _router: Router, private _currentUserService: CurrentUserService) { }

  ngOnInit(): void {

  }
  register() {
    this.newCustomer.firstName = this.RegisterForm.controls["firstName"].value;
    this.newCustomer.lastName = this.RegisterForm.controls["lastName"].value
    this.newCustomer.email = this.RegisterForm.controls["email"].value;
    this.newCustomer.password = this.RegisterForm.controls["password"].value;
    this._accountService.createNewAccount(this.newCustomer).subscribe(data => {
      if (data) {
        let user: LogInModel = {
          email: this.newCustomer.email,
          password: this.newCustomer.password
        }
        this._currentUserService.user = user;
        this._router.navigate(['logIn']);
      }
      else {
        Swal.fire({
          title: "Oppps...",
          text: "the registering didn't succeeds :(  \n let's try again",
          icon: "error"
        })
      }
    })
  }
}
