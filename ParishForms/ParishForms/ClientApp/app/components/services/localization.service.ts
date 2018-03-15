import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { Http, Response } from '@angular/http';
import { CacheService } from './cache.service';
import { EnvironmentSettings } from './client.settings.service';
import { Observable, Subscription } from 'rxjs';
import "rxjs/add/observable/of";
import 'rxjs/add/operator/map';

@Injectable()
export class LocalizationService {
    static culture: string;
    private static transSubs: any;
    private static initProm: Promise<any>;
    private static stateProm: Promise<any>;
    
    constructor(private readonly cache: CacheService, private http: Http,
        @Inject(PLATFORM_ID) private platformId: Object, private readonly settings: EnvironmentSettings) {
        LocalizationService.culture = "en-us";
        if (!LocalizationService.transSubs)
            LocalizationService.transSubs = { };
    }

    private initializeLocalization(): Observable<any> {
        //pre-cache cultures and translations
        const culturesKey = 'CulturesList';
        const cultsFromCache = this.cache.getCache(culturesKey);
        const cacheFunc = (cult: any) => this.cacheCulture(cult.value);
        const filter = (cult: any) => cult.value !== 'en-us';

        if (cultsFromCache)
            return Observable.of(cultsFromCache);

        //pre-cache states
        this.getStatesOptions();

        if (!LocalizationService.initProm) {
            LocalizationService.initProm = this.http.get(`${this.settings.getApiUrlBase()}/api/localization/list-cultures/`)
                .map(res => {
                    this.cache.setCache(culturesKey, res.json());
                    res.json().filter(filter).forEach(cacheFunc);
                    return res.json();
                }).toPromise();
        }

        return Observable.fromPromise(LocalizationService.initProm);
    }

    getCultures(): Observable<any> {
        const culturesKey = 'CulturesList';
        const cultsFromCache = this.cache.getCache(culturesKey);

        if(cultsFromCache)
            return Observable.of(cultsFromCache);

        return this.initializeLocalization();
    }

    translate(key: string): string {
        if (LocalizationService.culture === 'en-us')
            return key;

        const fromCache = this.cache.getCache(`${LocalizationService.culture}-translations`);
        if (fromCache && fromCache[key])
            return fromCache[key];
        if (fromCache && !fromCache[key])
            return key;

        if (!LocalizationService.transSubs[LocalizationService.culture]) {
            LocalizationService.transSubs[LocalizationService.culture] = this.http
                .get(`${this.settings.getApiUrlBase()}/api/localization/labels/${LocalizationService.culture}/`)
                .map((res: Response) => {
                    this.cache.setCache(`${LocalizationService.culture}-translations`, res.json());
                    return res.json();
                }).toPromise();
        }

        LocalizationService.transSubs[LocalizationService.culture].then((data: any) => {
            if (data && data[key])
                return data[key];
            else
                return key;
        });

        return key;
    }

    getStatesOptions(): Observable<any> {
        const key = `${LocalizationService.culture}-stateslist`;
        const fromCache = this.cache.getCache(key);
        if (fromCache) {
            return Observable.of(fromCache);
        } 

        if (this.settings.getApiUrlBase() !== undefined && !LocalizationService.stateProm) {
            LocalizationService.stateProm = this.http.get(`${this.settings.getApiUrlBase()}/api/localization/states`)
                .map((res: Response) => {
                    this.cache.setCache(key, res.json());
                    return res.json();
                }).toPromise();
        }

        return Observable.fromPromise(LocalizationService.stateProm);
    }

    private cacheCulture(culture: string) {
        const key = `${culture}-translations`;
        const fromCache = this.cache.getCache(key);

        if (!fromCache) {
            if (LocalizationService.transSubs[culture])
                return;

            LocalizationService.transSubs[culture] = this.http
                .get(`${this.settings.getApiUrlBase()}/api/localization/labels/${culture}/`)
                .map((res: Response) => {
                    this.cache.setCache(key, res.json());
                    return res.json();
                }).toPromise();
        }
    }
}
