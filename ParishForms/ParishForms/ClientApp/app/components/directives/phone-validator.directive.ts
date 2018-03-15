import { Directive } from '@angular/core';
import { NG_VALIDATORS, AbstractControl, Validators } from '@angular/forms';

@Directive({
    selector: '[phone]',
    providers: [{ provide: NG_VALIDATORS, useExisting: PhoneValidatorDirective, multi: true }]
})
export class PhoneValidatorDirective {
    static readonly pattern: string = "^[0-9]{3}(-)*[0-9]{3}(-)*[0-9]{4}$";

    validate(c: AbstractControl): { [key: string]: any } | null {
        const r = new RegExp(PhoneValidatorDirective.pattern);
        if (c.value && !r.test(c.value)) {
            return { 'phone': { }};
        }

        return null;
    }
}