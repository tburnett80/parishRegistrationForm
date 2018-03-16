import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { Http, Response } from '@angular/http';
import { isPlatformServer } from '@angular/common';
import { CacheService } from './cache.service';
import { SpinnerService } from './spinner.service';
import { Observable } from 'rxjs';
import "rxjs/add/observable/of";
import 'rxjs/add/operator/map';

@Injectable()
export class EnvironmentSettings {
    static readonly spinnerName: string = "getSettings";

    constructor( @Inject(PLATFORM_ID) private platformId: Object, private readonly spinner: SpinnerService,
        private readonly cache: CacheService, private http: Http) { }

    getApiUrlBase(): string | undefined {

        if (isPlatformServer(this.platformId)) {
            return undefined;
        }

        if (!window || !window.location || !window.location.protocol && !window.location.hostname) {
            return undefined;
        }

        if (window.location.port && window.location.port !== '80' && window.location.port !== '443') {
            const port = `:${window.location.port}`;
            return `${window.location.protocol}//${window.location.hostname}${port}`;
        }

        return `${window.location.protocol}//${window.location.hostname}`;
    }

    getRedirectUrl(): Observable<any> {
        const key = 'RedirectUrl';
        const fromCache = this.cache.getCache(key);

        if (fromCache && fromCache["redirectUrl"])
            return Observable.of(fromCache["redirectUrl"]);

        this.spinner.show(EnvironmentSettings.spinnerName);
        return this.http.get(`${this.getApiUrlBase()}/api/settings`)
            .map((res: Response) => {
                this.cache.setCache(key, res.json());
                return res.json()["redirectUrl"];
            }).do(() => this.spinner.hide(EnvironmentSettings.spinnerName));
    }
}
