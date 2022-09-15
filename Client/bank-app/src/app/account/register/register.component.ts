import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { registeringModel } from 'src/app/models/registering.model';
import { LogInModel } from 'src/app/models/logIn.model';
import { AccountService } from 'src/app/services/account.service';
import { CurrentUserService } from 'src/app/services/current-user.service';
import Swal from 'sweetalert2';
import { EmailVerificationService } from 'src/app/services/email-verification.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  newCustomer: registeringModel = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    verificationCode: '',
    ExpirationTime: new Date()
  };
  RegisterForm: FormGroup = new FormGroup({
    firstName: new FormControl("", [Validators.required, Validators.minLength(2)]),
    lastName: new FormControl("", [Validators.required, Validators.minLength(2)]),
    email: new FormControl("", [Validators.required, Validators.email]),
    password: new FormControl("", [Validators.required, Validators.minLength(4)])
  });
  constructor(private _accountService: AccountService, private _emailVerificationService: EmailVerificationService, private _router: Router, private _currentUserService: CurrentUserService) { }

  ngOnInit(): void {

  }
  // register() {
  //   this.newCustomer.firstName = this.RegisterForm.controls["firstName"].value;
  //   this.newCustomer.lastName = this.RegisterForm.controls["lastName"].value
  //   this.newCustomer.email = this.RegisterForm.controls["email"].value;
  //   this.newCustomer.password = this.RegisterForm.controls["password"].value;
  //   this._accountService.createNewAccount(this.newCustomer).subscribe(data => {
  //     if (data) {
  //       let user: LogInModel = {
  //         email: this.newCustomer.email,
  //         password: this.newCustomer.password
  //       }
  //       this._currentUserService.user = user;
  //       this._router.navigate(['logIn']);
  //     }
  //     else {
  //       Swal.fire({
  //         title: "Oppps...",
  //         text: "the registering didn't succeeds :(  \n let's try again",
  //         icon: "error"
  //       })
  //     }
  //   })
  // }
  register() {
    this.newCustomer.firstName = this.RegisterForm.controls["firstName"].value;
    this.newCustomer.lastName = this.RegisterForm.controls["lastName"].value
    this.newCustomer.email = this.RegisterForm.controls["email"].value;
    this.newCustomer.password = this.RegisterForm.controls["password"].value;
    this._accountService.createNewAccount(this.newCustomer.email).subscribe(data => {
      if (data) {
        Swal.fire({
          title: "Check your email for varifiction code",
          input: "password",
          showDenyButton: true,
          denyButtonText: "Send the code again",
          timer: 300000
        })
          .then(code => {
            if (code.value) {
              this.newCustomer.verificationCode = code.value;
              this.newCustomer.ExpirationTime = new Date();
              this._emailVerificationService.verifyCustomer(this.newCustomer).subscribe(data => {
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
                    text: "the code is invalid :(  \n let's try again",
                    icon: "error"
                  })
                }
              });
            }
            else {
              Swal.fire({
                text: "Pleace enter a code...",
                icon: "error"
              })
            }
          })
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
