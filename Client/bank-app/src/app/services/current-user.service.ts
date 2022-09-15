import { Injectable } from '@angular/core';
import { LogInModel } from '../models/logIn.model';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserService {
  _isLogIn: boolean = false;
  accuontId!: number;
  user!: LogInModel;
  constructor() { }
  getAccountId(): number { return this.accuontId; }
  getUser(): LogInModel { return this.user; }
  isLogIn(): boolean { return this._isLogIn = true; }
}
