import { Injectable } from '@angular/core';
import { CacheService } from './cache.service';
import { Observable } from 'rxjs';
import "rxjs/add/observable/of";

@Injectable()
export class LocalizationService {
    private static culture: string;

    constructor(private readonly cache: CacheService) {
        LocalizationService.culture = "en-us";
    }

    getFormText(): Observable<any> {
        const fromCache = this.cache.getCache(`${LocalizationService.culture}-translations`);
        if (fromCache) {
            return Observable.of(fromCache);
        } 

        const map: any = {};
        map['header'] = 'Borromeo Parish Directory Signup 2018';
        map['description'] = 'This is the sign up form for the Parish Directory Update';
        map['family_name'] = 'Household Name';
        map['family_name_ph'] = 'i.e. Smith';
        map['street_address'] = 'Street Address';
        map['street_address_ph'] = 'i.e. 601 N 4th St.';
        map['city'] = 'City';
        map['city_ph'] = 'i.e. St Charles';
        map['zip'] = 'Zip';
        map['zip_ph'] = 'i.e. 63301';
        
        this.cache.setCache(`${LocalizationService.culture}-translations`, JSON.stringify(map));
        return Observable.of(map);
    }
}
