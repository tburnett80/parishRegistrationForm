import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import "rxjs/add/observable/of";

@Injectable()
export class LocalizationService {
    private static culture: string;
    private static english: any;
    private static spanish: any;

    constructor() {
        LocalizationService.culture = "en-us";
        LocalizationService.english = null;
        LocalizationService.spanish = null;
    }

    getFormText(): Observable<any> {
        if (LocalizationService.english && LocalizationService.culture === 'en-us') {
            return LocalizationService.english;
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

        LocalizationService.english = map;
        return Observable.of(map);
    }
}
