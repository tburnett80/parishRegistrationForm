import { Injectable, NgZone } from '@angular/core';
import { Adal4Service } from 'adal-angular4';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { AuthorizationToken } from '../models/AuthorizationToken';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';
import { CookieService } from '../../cookie-module/services/cookies.service';
import { CacheService } from './cache.service';

const AUTH_TOKEN_COOKIE: string = 'authToken';

@Injectable()
export class AuthService {

    private acquireTokens: Subscription;
    private readonly ttl: number = 86400000;
    private readonly tokenKey: string = 'id_token';
    private readonly resourceTokenKey: string = 'resource_token';

    constructor(private readonly adalService: Adal4Service, private readonly http: Http, private readonly zone: NgZone,
        private readonly cookieService: CookieService, private readonly cache: CacheService) { }

    /**
     * Azure Active Directory Configuration Definition
     */
    get adalConfig(): any {
        return {
            tenant: 'borromeoparish.onmicrosoft.com',
            clientId: '043e77fd-9913-4259-90ac-02ac61b90e89',
            redirectUri: window.location.origin + '/login',
            postLogoutRedirectUri: window.location.origin + '/login',
            resourceId: 'https://borromeoparish.onmicrosoft.com/043e77fd-9913-4259-90ac-02ac61b90e89',
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
        const idToken: string | null = this.cache.getCache(this.tokenKey);

        //if we don't have their profile, go get it
        if (idToken) {
            return Observable.of(true);
        } else {
            return this.adalService.acquireToken(this.adalConfig.resourceId)
                .map((tokenOut: string) => {
                    this.cache.setCache(this.tokenKey, tokenOut, this.ttl);
                    this.cache.setCache(this.resourceTokenKey, 'true', this.ttl);

                    return true;
                });
        }
    }

    get resourceTokenAcquired(): boolean {
        const tokenExists: string | null = this.cache.getCache(this.resourceTokenKey);
        return tokenExists !== null && tokenExists === 'true';
    }

    /**
     * @description Logout from the server to purge the credentail cache
     */
    logOut() {
        this.adalService.logOut();
        this.cache.invalidateKey(this.tokenKey);
        this.cache.invalidateKey(this.resourceTokenKey);
        //let endpoint: string = this.config.authenticationUrl + '/logout';
        //return this.httpService.post(endpoint, null);
    }

    login() {
        this.adalService.login();
    }

    get isAuthenticated(): boolean {
        return this.adalService.userInfo.authenticated;
    }

    aquireToken() {
        if (this.isAuthenticated && this.resourceTokenAcquired) {
            //this.router.navigateByUrl('/export');
            window.location.href = window.location.origin + '/export';
            return true;
        } else {
            this.zone.run(() => {
                console.log('resource: ', this.adalConfig.resourceId);
                this.adalService.acquireToken(this.adalConfig.resourceId)
                    .subscribe((tokenOut: string) => {

                        this.cache.setCache(this.tokenKey, tokenOut, this.ttl);
                        this.cache.setCache(this.resourceTokenKey, 'true', this.ttl);

                        window.location.href = window.location.origin + '/export';
                    });
            });

        }
    }

}
