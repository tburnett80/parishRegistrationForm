﻿import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
        private readonly cache: CacheService, private http: HttpClient) { }

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
        const fromCacheStr = this.cache.getCache(key);

        if (fromCacheStr) {
            const fromCache = JSON.parse(fromCacheStr);
            if (fromCache && fromCache["redirectUrl"])
            return Observable.of(fromCache["redirectUrl"]);
        }
            
        this.spinner.show(EnvironmentSettings.spinnerName);
        return this.http.get(`${this.getApiUrlBase()}/api/settings`)
            .map((r: any) => {
                this.cache.setCache(key, JSON.stringify(r));
                return r["redirectUrl"];
            })
            .do(() => this.spinner.hide(EnvironmentSettings.spinnerName));
    }
}
