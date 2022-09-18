import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { registeringModel } from 'src/app/models/registering.model';
import { LogInModel } from 'src/app/models/logIn.model';
import { AccountService } from 'src/app/services/account.service';
import { CurrentUserService } from 'src/app/services/current-user.service';
import Swal from 'sweetalert2';
import { EmailVerificationService } from 'src/app/services/email-verification.service';
import { MatDialog } from '@angular/material/dialog';
import { EmailVerifictionComponent } from '../email-verifiction/email-verifiction.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  //  registerAlert = Swal.mixin({
  //   title: "Check your email for varifiction code",
  //   input: "password",
  //   inputAttributes: {required: "true"},
  //   validationMessage: "This field is required",
  //   showDenyButton: true,
  //   denyButtonText: "Send the code again",
  //   denyButtonColor: "blue",
  //   confirmButtonColor: "blue",
  //   timer: 300000
  // })
  invalidCodeAlert = Swal.mixin({
    title: "Oppps...",
    text: "the code is invalid :(  \n let's try again",
    icon: "error"
  });
  regiseringFaildAlert=Swal.mixin({
    title: "Oppps...",
    text: "the registering didn't succeeds :(  \n let's try again",
    icon: "error"
  });
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
  code!: string;
  constructor(public dialog: MatDialog, private _accountService: AccountService, private _emailVerificationService: EmailVerificationService, private _router: Router, private _currentUserService: CurrentUserService) { }

  ngOnInit(): void {
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(EmailVerifictionComponent);
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      if (result) {
        this.code = result;
        this.register(this.code);
      }
      else {
        this.sendEmail();
      }

    });
  }

  register(code: string) {
    if (code) {
      this.newCustomer.verificationCode = code;
      this.newCustomer.ExpirationTime = new Date();
      this._emailVerificationService.verifyCustomer(this.newCustomer).subscribe(CorrectCode => {
        if (CorrectCode) {
          this.logIn()
        }
        else {
          this.invalidCodeAlert.fire();
          ///??????????
        }
      });
    }
  }
  // this.updateRegisterModel()
  // this._accountService.createNewAccount(this.newCustomer.email).subscribe(data => {
  //   if (data) {
  //     this.registerAlert.fire()
  //       .then(data => {
  //           this.newCustomer.verificationCode = data.value;
  //           this.newCustomer.ExpirationTime = new Date();
  //           this._emailVerificationService.verifyCustomer(this.newCustomer).subscribe(data => {
  //             if (data) {
  //               this.logIn()
  //             }
  //             //the code isn't correct
  //             else {
  //               this.invalidCodeAlert.fire()
  //               .then(data=>this.registerAlert.fire())
  //             }
  //           });

  //         //sending the email again
  //          if (data.isDenied) {
  //           this._accountService.createNewAccount(this.newCustomer.email).subscribe(data => 
  //             this.registerAlert.fire()
  //           )  
  //         }
  //       })
  //   }
  //   //the registering is faild
  //   else {
  //     this.regiseringFaildAlert.fire()
  //   }
  // })

  logIn() {
    let user: LogInModel = {
      email: this.newCustomer.email,
      password: this.newCustomer.password
    }
    this._currentUserService.user = user;
    this._router.navigate(['account/logIn']);
  }

  updateRegisterModel() {
    this.newCustomer.firstName = this.RegisterForm.controls["firstName"].value;
    this.newCustomer.lastName = this.RegisterForm.controls["lastName"].value
    this.newCustomer.email = this.RegisterForm.controls["email"].value;
    this.newCustomer.password = this.RegisterForm.controls["password"].value;
  }

  sendEmail() {
      this.updateRegisterModel();
      this._accountService.createNewAccount(this.newCustomer.email).subscribe(sendingEmail => {
      console.log("sending...");
      if (sendingEmail) {
        this.openDialog();
      }
      else {
        this.regiseringFaildAlert.fire()
      }
    });
  }
}
