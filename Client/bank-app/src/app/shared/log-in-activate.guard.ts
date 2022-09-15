import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
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
