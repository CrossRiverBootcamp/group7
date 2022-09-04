import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscriber } from 'rxjs';
import { CustomerModel } from 'src/app/models/customer.model';
import { LogInModel } from 'src/app/models/logIn.model';
import { CurrentUserService } from 'src/app/service/current-user.service';
import { CustomerService } from 'src/app/service/customer.service';
import Swal from 'sweetalert2';
import { CustomerModule } from '../customer.module';

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
    firstName: new FormControl("", Validators.required),
    lastName: new FormControl("", Validators.required),
    email: new FormControl("", Validators.required),
    password: new FormControl("", Validators.required)
  });
  constructor(private _customService: CustomerService, private _router: Router, private _currentUserService: CurrentUserService) { }

  ngOnInit(): void {

  }
  register() {
    this.newCustomer.firstName = this.RegisterForm.controls["firstName"].value;
    this.newCustomer.lastName = this.RegisterForm.controls["lastName"].value
    this.newCustomer.email = this.RegisterForm.controls["email"].value;
    this.newCustomer.password = this.RegisterForm.controls["password"].value;
    this._customService.register(this.newCustomer).subscribe(data => {
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
