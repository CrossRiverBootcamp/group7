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
  invalidCodeAlert = Swal.mixin({
    title: "Oppps...",
    text: "the code is invalid :(  \n let's try again",
    icon: "error"
  });
  regiseringFaildAlert = Swal.mixin({
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
  unexeptedErrorAlert = Swal.mixin({
    title: "Oppps...",
    text: "we are sorry... \n we try to fix it",
    icon: "error"
  });
  code!: string;
  constructor(public dialog: MatDialog, private _accountService: AccountService, private _emailVerificationService: EmailVerificationService, private _router: Router, private _currentUserService: CurrentUserService) { }

  ngOnInit(): void {
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(EmailVerifictionComponent);
    dialogRef.afterClosed().subscribe(result => {
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
      this._accountService.createNewAccount(this.newCustomer).subscribe(CorrectCode => {
        if (CorrectCode) {
          this.logIn()
        }
        else {
          this.invalidCodeAlert.fire()
            .then(data => {
              this.openDialog();
            })
        }
      }, error => {
        this.unexeptedErrorAlert.fire();
      });
    }
  }

  logIn() {
    let user: LogInModel = {
      email: this.newCustomer.email,
      password: this.newCustomer.password
    }
    this._currentUserService.user = user;
    this._router.navigate(['logIn']);
  }

  updateRegisterModel() {
    this.newCustomer.firstName = this.RegisterForm.controls["firstName"].value;
    this.newCustomer.lastName = this.RegisterForm.controls["lastName"].value
    this.newCustomer.email = this.RegisterForm.controls["email"].value;
    this.newCustomer.password = this.RegisterForm.controls["password"].value;
  }

  sendEmail() {
    this.updateRegisterModel();
    this._emailVerificationService.verifyCustomer(this.newCustomer.email).subscribe(sendingEmail => {
      if (sendingEmail) {
        this.openDialog();
      }
      else {
        this.regiseringFaildAlert.fire()
      }
    }, error => {
      this.unexeptedErrorAlert.fire();
    });
  }
}
