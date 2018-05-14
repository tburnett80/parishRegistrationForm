import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CacheService } from './cache.service';
import { EnvironmentSettings } from './client.settings.service';
import { SpinnerService } from './spinner.service';
import { Observable } from 'rxjs';
import "rxjs/add/observable/of";
import 'rxjs/add/operator/map';

@Injectable()
export class LocalizationService {
    static culture: string;
    static readonly spinnerName: string = "locSpinner";
    static readonly stateSpinnerName: string = "stateSpinner";
    private static transSubs: any;
    private static initProm: Promise<any>;
    private static stateProm: Promise<any>;
    
    constructor(private readonly cache: CacheService, private http: HttpClient, private readonly spinner: SpinnerService,
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
            return Observable.of(JSON.parse(cultsFromCache));

        //pre-cache states
        this.getStatesOptions();

        //trigger cache for settings
        this.settings.getRedirectUrl();

        if (!LocalizationService.initProm) {
            this.spinner.show(LocalizationService.spinnerName);
            LocalizationService.initProm = this.http.get(`${this.settings.getApiUrlBase()}/api/localization/list-cultures/`)
                .map((res: any) => {
                    this.cache.setCache(culturesKey, JSON.stringify(res));
                    res.filter(filter).forEach(cacheFunc);
                    return res;
                })
                .do(() => this.spinner.hide(LocalizationService.spinnerName))
                .toPromise();
        }

        return Observable.fromPromise(LocalizationService.initProm);
    }

    getCultures(): Observable<any> {
        const culturesKey = 'CulturesList';
        const cultsFromCache = this.cache.getCache(culturesKey);

        if(cultsFromCache)
            return Observable.of(JSON.parse(cultsFromCache));

        return this.initializeLocalization();
    }

    translate(key: string): string {
        if (LocalizationService.culture === 'en-us')
            return key;

        const fromCachestr = this.cache.getCache(`${LocalizationService.culture}-translations`);
        if (fromCachestr) {
            const fromCache = JSON.parse(fromCachestr);
            if (fromCache && fromCache[key])
                return fromCache[key];
            if (fromCache && !fromCache[key])
                return key;
        }

        if (!LocalizationService.transSubs[LocalizationService.culture]) {
            this.spinner.show(LocalizationService.spinnerName);
            LocalizationService.transSubs[LocalizationService.culture] = this.http
                .get(`${this.settings.getApiUrlBase()}/api/localization/labels/${LocalizationService.culture}/`)
                .map((res: any) => {
                    this.cache.setCache(`${LocalizationService.culture}-translations`, JSON.stringify(res));
                    return res;
                })
                .do(() => this.spinner.hide(LocalizationService.spinnerName))
                .toPromise();
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
            return Observable.of(JSON.parse(fromCache));
        } 

        if (this.settings.getApiUrlBase() !== undefined && !LocalizationService.stateProm) {
            this.spinner.show(LocalizationService.stateSpinnerName);
            LocalizationService.stateProm = this.http.get(`${this.settings.getApiUrlBase()}/api/localization/states`)
                .map((res: any) => {
                    console.log("Get States Options: ", res);
                    this.cache.setCache(key, JSON.stringify(res));
                    return res;
                })
                .do(() => this.spinner.hide(LocalizationService.stateSpinnerName))
                .toPromise();
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
                .map((res: any) => {
                    this.cache.setCache(key, JSON.stringify(res));
                    return res;
                }).toPromise();
        }
    }
}
