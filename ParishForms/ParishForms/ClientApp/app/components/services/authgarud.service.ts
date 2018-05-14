import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AdalService } from 'adal-angular4';
import { AuthHelperService } from './auth-helper.service';
import { Observable } from "rxjs";

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private readonly adalService: AdalService, private readonly router: Router,
        private readonly authHelper: AuthHelperService) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

        if (this.adalService.userInfo && this.adalService.userInfo.authenticated) {
            if (this.authHelper.canNavigateRoute(this.adalService.userInfo.profile.roles, state.url))
                return true;
            else 
                this.router.navigate(['/unauthorized']);
        } else {
            //if not logged in, that needs to happen to proceed
            this.router.navigate(['/login']);
        }

        //default is false
        return false;
    }
}