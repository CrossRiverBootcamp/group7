import { Injectable } from '@angular/core';
import {  CanActivate } from '@angular/router';
import { CurrentUserService } from '../services/current-user.service';


@Injectable({
  providedIn: 'root'
})
export class LogInActivateGuard implements CanActivate {
 
  constructor(private _currentUserService : CurrentUserService) {
  }
  canActivate(): boolean  {
    return this._currentUserService._isLogIn; 
  }
}
