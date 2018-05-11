import { Injectable } from '@angular/core';

@Injectable()
export class AuthHelperService {

    constructor() {
    }

    canNavigateRoute(roles: string[], route: string): boolean {
        if (!roles || roles.length < 1)
            return false;

        //admins can always activate a route
        if (roles.indexOf('administrator') > -1)
            return true;

        //Exporter role
        if (roles.indexOf('exporter') > -1) {
            switch (route) {
                case "/export":
                    return true;
                default:
                    return false;
            }
        }

        //default is false
        return false;
    }
}