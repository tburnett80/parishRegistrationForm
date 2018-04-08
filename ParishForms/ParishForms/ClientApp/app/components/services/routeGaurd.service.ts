import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Adal4Service, Adal4HTTPService } from 'adal-angular4';
import { Observable } from 'rxjs/Observable';
import { AuthService } from './auth.service';

@Injectable()
export class RouteGuard implements CanActivate {
    constructor(
        private adalService: Adal4Service,
        private authService: AuthService,
        private router: Router,
        private authHttp: Adal4HTTPService,
        private http: Http
    ) { }

    canActivate() {
        if (this.adalService.userInfo.authenticated) {
            return true;
        } else {
            localStorage.clear();
            this.adalService.clearCache();
            this.router.navigate(['/login']);
            return false;
        }
    }

    private configureHttpOptions(token: string): RequestOptions {
        const headers = new Headers({
            'Content-Type': 'application/x-www-form-urlencoded',
            'Authorization': 'Bearer ' + token
        });

        return new RequestOptions({ headers: headers });
    }
}
