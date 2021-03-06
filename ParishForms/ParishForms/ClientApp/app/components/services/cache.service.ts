﻿import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer } from '@angular/common';

@Injectable()
export class CacheService {
    constructor(@Inject(PLATFORM_ID) private platformId: Object) { }

    //default is 2 hours, or 7200000 milliseconds
    setCache(key: string, json: string, ttl: number = 7200000) {

        if (isPlatformServer(this.platformId)) {
            return;
        }

        if (!key) {
            console.log("cant cache when key null");
            return;
        }

        if (!json)
            return;

        const container: any = {};
        
        container["expires"] = new Date().getTime() + ttl;
        container["data"] = json;

        localStorage.setItem(key, JSON.stringify(container));
    }

    invalidateKey(key: string) {
        if (!key) {
            console.log("cant cache when key null");
            return;
        }

        localStorage.removeItem(key);
    }

    getCache(key: string): any | null {
        if (isPlatformServer(this.platformId)) {
            return null;
        }

        const data = localStorage.getItem(key);
        if (!data) {
            return null;
        }

        const container = JSON.parse(data);
        if (!container) {
            return null;
        }

        if (!container.data || !container.expires) {
            return null;
        }

        if (container.data instanceof Array && container.data.length === 0)
            return null;

        if (container["expires"] < new Date().getTime()) {
            console.log("Cache expired for key: ", key);
            this.invalidateKey(key);
            return null;
        }

        return container["data"];
    }
}
