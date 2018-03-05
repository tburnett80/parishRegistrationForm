import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { Http, Response } from '@angular/http';
import { CacheService } from './cache.service';
import { Observable } from 'rxjs';
import "rxjs/add/observable/of";
import 'rxjs/add/operator/map';

@Injectable()
export class LocalizationService {
    static culture: string;

    constructor(private readonly cache: CacheService, private readonly http: Http,
        @Inject(PLATFORM_ID) private platformId: Object) {
        LocalizationService.culture = "en-us";
    }

    getFormText(): Observable<any> {
        const key = `${LocalizationService.culture}-translations`;
        const fromCache = this.cache.getCache(key);
        if (fromCache) {
            return Observable.of(JSON.parse(fromCache));
        } 

        const map: any = {};
        map['header'] = 'Borromeo Parish Directory Signup 2018';
        map['description'] = 'This is the sign up form for the Parish Directory Update';
        map['family_name'] = 'Household Name';
        map['family_name_ph'] = 'i.e. Smith';
        map['home_phone'] = 'Home Phone';
        map['home_phone_ph'] = 'i.e. 636-946-1893';
        map['home_phone_pub_label'] = 'Publish phone number in directory';
        map['street_address'] = 'Street Address';
        map['street_address_ph'] = 'i.e. 601 N 4th St.';
        map['city'] = 'City';
        map['city_ph'] = 'i.e. St Charles';
        map['zip'] = 'Zip';
        map['zip_ph'] = 'i.e. 63301';
        map['state'] = 'State';
        
        this.cache.setCache(key, JSON.stringify(map));
        return Observable.of(map);
    }

    getStatesOptions(): Observable<any> {
        const key = `${LocalizationService.culture}-stateslist`;
        const fromCache = this.cache.getCache(key);
        if (fromCache) {
            return Observable.of(fromCache);
        } 

        if (isPlatformServer(this.platformId)) {
            const map: any = {};
            map['text'] = 'Select a state';
            map['value'] = '';

            return Observable.of([map]);
        }

        return this.http.get(`http://localhost:60588/api/localization/states`)
            .map((res: Response) => {
                this.cache.setCache(key, res.json(), 3600000);
                return res.json();
            });
    }
}
