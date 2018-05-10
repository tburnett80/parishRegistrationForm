import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Adal4Service } from 'adal-angular4';
import { Observable } from "rxjs";

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private readonly adalService: Adal4Service, private readonly router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

        if (this.adalService.userInfo && this.adalService.userInfo.authenticated) {
            if (this.canNavigateRoute(this.adalService.userInfo.profile.roles, ""))
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

    private canNavigateRoute(roles: string[], route: string): boolean {

        if (!roles || roles.length < 1)
            return false;

        //admins can always activate a route
        if (roles.indexOf('administrator') > -1)
            return true;


        //default is false
        return false;
    }
}