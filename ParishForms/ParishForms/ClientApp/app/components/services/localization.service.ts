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
    private getCultureListSub: Subscription;
    private transSubs: any;

    constructor(private readonly cache: CacheService, private http: Http,
        @Inject(PLATFORM_ID) private platformId: Object, private readonly settings: EnvironmentSettings) {
        LocalizationService.culture = "en-us";
        this.transSubs = {};
    }

    initializeLocalization() {
        //pre-cache states
        this.getStatesOptions();

        //pre-cache cultures and translations
        const culturesKey = 'CulturesList';
        const cultsFromCache = this.cache.getCache(culturesKey);
        const cacheFunc = (cult: any) => this.cacheCulture(cult.value);
        if (!cultsFromCache) {
            this.getCultureListSub = this.http.get(`${this.settings.getApiUrlBase()}/api/localization/list-cultures/`)
                .map(res => {
                    this.cache.setCache(culturesKey, res.json());
                    return res.json();
                })
                .subscribe(cultures => {
                    cultures.forEach(cacheFunc);
                    this.getCultureListSub.unsubscribe();
                });
        } else {
            cultsFromCache.forEach(cacheFunc);
        }
    }

    getCultures() {
        const culturesKey = 'CulturesList';
        const cultsFromCache = this.cache.getCache(culturesKey);
        if (!cultsFromCache) {
            this.getCultureListSub = this.http.get(`${this.settings.getApiUrlBase()}/api/localization/list-cultures/`)
                .map(res => {
                    this.cache.setCache(culturesKey, res.json());
                    return res.json();
                })
                .subscribe(cultures => {
                    this.getCultureListSub.unsubscribe();
                    return cultures;
                });
        } else {
            return cultsFromCache;
        }
    }

    translate(key: string): string {
        if (LocalizationService.culture === 'en-us')
            return key;

        let fromCache: any = this.cache.getCache(`${LocalizationService.culture}-translations`);
        if (!fromCache) {
            console.log("translation culture not in cache, getting data...");
            this.transSubs[LocalizationService.culture] = this.http.get(`${this.settings.getApiUrlBase()}/api/localization/labels/${LocalizationService.culture}/`)
                .map((res: Response) => {
                    this.cache.setCache(`${LocalizationService.culture}-translations`, res.json());
                    return res.json();
                }).subscribe(data => {
                    fromCache = data;
                    this.transSubs[LocalizationService.culture].unsubscribe();
                });
        }

        if (fromCache && fromCache[key])
            return fromCache[key];
        
        return key;
    }

    isCultureLoaded(culture: string): boolean {
        return this.cache.getCache(`${culture}-translations`);
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
                    this.cache.setCache(key, res.json());
                    return res.json();
                });
        }

        return Observable.of(null);
    }

    private cacheCulture(culture: string) {
        const key = `${culture}-translations`;
        const fromCache = this.cache.getCache(key);

        if (!fromCache) {
            this.transSubs[culture] = this.http
                .get(`${this.settings.getApiUrlBase()}/api/localization/labels/${culture}/`)
                .map((res: Response) => {
                    this.cache.setCache(key, res.json());
                    return res.json();
                }).subscribe(() => this.transSubs[culture].unsubscribe());
        }
    }
}
