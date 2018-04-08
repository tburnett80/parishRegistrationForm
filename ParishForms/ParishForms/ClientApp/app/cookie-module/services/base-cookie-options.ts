/**
 * angular2-cookie - Implementation of Angular 1.x $cookies service to Angular 2
 * @version v1.2.4
 * @link https://github.com/salemdar/angular2-cookie#readme
 * @license MIT
 */
import { APP_BASE_HREF } from '@angular/common';
import { Inject, Injectable, Injector, Optional } from '@angular/core';

/** @private */
export class CookieOptions {
    path?: string;
    domain?: string;
    expires?: string | Date;
    secure: boolean;

    constructor({ path, domain, expires, secure }: ICookieOptionsArgs = {}) {
        this.path = this.isPresent(path) ? path : undefined;
        this.domain = this.isPresent(domain) ? domain : undefined;
        this.expires = this.isPresent(expires) ? expires : undefined;
        this.secure = this.isPresent(secure) ? secure : false;
    }

    merge(options?: ICookieOptionsArgs): CookieOptions {
        return new CookieOptions(<ICookieOptionsArgs> {
            path: options && options.path ? options.path : this.path,
            domain: options && options.domain ? options.domain : this.domain,
            expires: options && options.expires ? options.expires : this.expires,
            secure: options && options.secure ? options.secure : this.secure
        });
    }

    private isPresent(obj: any): boolean {
        return obj !== undefined && obj !== null;
    }
}

/** @private */
@Injectable()
export class BaseCookieOptions extends CookieOptions {
    constructor( @Optional() @Inject(APP_BASE_HREF) private baseHref: string) {
        super({ path: baseHref || '/' });
    }
}