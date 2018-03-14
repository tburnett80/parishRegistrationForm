import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { Http, Response } from '@angular/http';
import { CacheService } from './cache.service';
import { EnvironmentSettings } from './client.settings.service';
import { Observable, Subscription } from 'rxjs';
import "rxjs/add/observable/of";
import 'rxjs/add/operator/map';

@Injectable()
export class FormConstraintsService {

    constructor(private readonly cache: CacheService, private http: Http,
        @Inject(PLATFORM_ID) private platformId: Object, private readonly settings: EnvironmentSettings) {
    }

    getDirectoryLimits(): Observable<any> {
        return this.getFormConstraints("directory", "/api/directory/limits");
    }

    private getFormConstraints(formName: string, urlPath: string): Observable<any> {
        if (isPlatformServer(this.platformId)) {
            return Observable.of(null);
        }

        const frmKey = `${formName}::formLimits`;
        const fromCache = this.cache.getCache(frmKey);

        if (fromCache)
            return Observable.of(fromCache);

        return this.http.get(`${this.settings.getApiUrlBase()}${urlPath}`)
            .map(res => {
                this.cache.setCache(frmKey, res.json());
                return res.json();
            });
    }
}