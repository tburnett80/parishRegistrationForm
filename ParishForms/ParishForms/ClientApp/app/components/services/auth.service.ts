import { Injectable } from '@angular/core';
import { Adal4Service } from 'adal-angular4';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { AuthorizationToken } from '../models/AuthorizationToken';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';
import { CookieService } from '../../cookie-module/services/cookies.service';

const AUTH_TOKEN_COOKIE: string = 'authToken';

@Injectable()
export class AuthService {

    private acquireTokens: Subscription;

    constructor(private readonly adalService: Adal4Service, private readonly http: Http,
        private readonly cookieService: CookieService) { }

    /**
     * Azure Active Directory Configuration Definition
     */
    get adalConfig(): any {
        return {
            tenant: '<todo>.onmicrosoft.com',
            clientId: '',
            redirectUri: window.location.origin + '/login',
            postLogoutRedirectUri: window.location.origin + '/login',
            resourceId: 'https://<todo>.onmicrosoft.com/<client id>',
            endpoints: {
                'https://graph.microsoft.com/v1.0/me/':
                'https://graph.microsoft.com/'
            }
        };
    }

    /**
     * Azure Active Directory get custom profile information
     */
    checkUserProfile(): Observable<boolean> {
        //console.log('AuthService.checkUserProfile')
        const id_token: string = localStorage.getItem('id_token');

        //if we don't have their profile, go get it
        if ((id_token && id_token != null)) {
            return Observable.of(true);
        } else {
            return this.adalService.acquireToken(this.adalConfig.resourceId)
                .map((tokenOut: string) => {
                    //console.log('checkUserProfile.acquireToken -> map');
                    localStorage.setItem('id_token', tokenOut);
                    localStorage.setItem('resource_token', 'true');
                    return true;
                });
        }
    }

    get resourceTokenAcquired(): boolean {
        let tokenExists: string = localStorage.getItem('resource_token')
        return tokenExists != null;
    }

    /**
     * @description Logout from the server to purge the credentail cache
     */
    logOut() {
        localStorage.clear();
        //let endpoint: string = this.config.authenticationUrl + '/logout';
        //return this.httpService.post(endpoint, null);
    }
}
