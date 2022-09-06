import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LogInModel } from 'src/app/models/logIn.model';
import { CurrentUserService } from 'src/app/service/current-user.service';
import { CustomerService } from 'src/app/service/customer.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.scss']
})
export class LogInComponent implements OnInit {
  newUser:LogInModel = {
    email: '',
    password: ''
  }
  LogInForm: FormGroup = new FormGroup({
    email: new FormControl("", [Validators.required,Validators.email]),
    password: new FormControl("", [Validators.required,Validators.minLength(4)])
  });
  constructor(private _customService:CustomerService ,private _router: Router,private _currentUserService: CurrentUserService) { }

  ngOnInit(): void {
    if(this._currentUserService.getUser() != null) {
      this.LogInForm.controls["email"].setValue(this._currentUserService.getUser().email);
      this.LogInForm.controls["password"].setValue(this._currentUserService.getUser().password);
    }
  }

  logIn()
  { 
    this.newUser.email = this.LogInForm.controls["email"].value;
    this.newUser.password = this.LogInForm.controls["password"].value;
    this._customService.logIn(this.newUser).subscribe(data=>{
      if(data != 0){
        this._currentUserService.accuontId=data
        Swal.fire({
          title:"Hi!!",
          text:"wellcame :)",
          icon: "success",
        }).then( ()=> this._router.navigate(['account-info']))
      }
      else{
        Swal.fire({
          title:"Oppps...",
          text:"we dont Know you :( ",
          icon: "error",
          cancelButtonText:"Click her to register"    
        }).then( ()=> this._router.navigate(['register']))
      }
    })    
  }
}
