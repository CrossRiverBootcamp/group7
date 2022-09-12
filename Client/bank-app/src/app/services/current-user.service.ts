import { Injectable } from '@angular/core';
import { LogInModel } from '../models/logIn.model';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserService {

  accuontId!: number;
  user!: LogInModel;
  constructor() { }
  getAccountId(): number { return this.accuontId; }
  getUser(): LogInModel { return this.user; }
}
