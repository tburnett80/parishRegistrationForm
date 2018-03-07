import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { Http, Response } from '@angular/http';
import { CacheService } from './cache.service';
import { EnvironmentSettings } from './client.settings.service';
import { Observable } from 'rxjs';
import "rxjs/add/observable/of";
import 'rxjs/add/operator/map';

@Injectable()
export class LocalizationService {
    static culture: string;

    constructor(private readonly cache: CacheService, private http: Http,
        @Inject(PLATFORM_ID) private platformId: Object, private readonly settings: EnvironmentSettings) {
        LocalizationService.culture = "en-us";
    }

    getFormText(): Observable<any> {
        const key = `${LocalizationService.culture}-translations`;
        console.log("Getting: ", key);
        const fromCache = this.cache.getCache(key);
        if (fromCache) {
            return Observable.of(fromCache);
        } 

        if (this.settings.getApiUrlBase() && LocalizationService.culture !== 'en-us') {
            return this.http.get(`${this.settings.getApiUrlBase()}/api/localization/labels/${LocalizationService.culture}/`)
                .map((res: Response) => {
                    this.cache.setCache(key, res.json(), 900000);
                    return res.json();
                });
        }

        return Observable.of(null);
    }

    getStatesOptions(): Observable<any> {
        const key = `${LocalizationService.culture}-stateslist`;
        const fromCache = this.cache.getCache(key);
        if (fromCache) {
            return Observable.of(fromCache);
        } 

        if (this.settings.getApiUrlBase() !== undefined) {
            return this.http.get(`${this.settings.getApiUrlBase()}/api/localization/states`)
                .map((res: Response) => {
                    this.cache.setCache(key, res.json(), 3600000);
                    return res.json();
                });
        }

        return Observable.of(null);
    }
}
