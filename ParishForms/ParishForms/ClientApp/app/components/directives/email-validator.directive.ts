import { Directive } from '@angular/core';
import { NG_VALIDATORS, AbstractControl, Validators } from '@angular/forms';

@Directive({
    selector: '[email2]',
    providers: [{ provide: NG_VALIDATORS, useExisting: EmailValidatorDirective, multi: true }]
})
export class EmailValidatorDirective {
    validate(c: AbstractControl): { [key: string]: any } | null {
        const emailError = Validators.email(c);
        if (c.value && emailError) {
            return { 'email': {} };
        }

        return null;
    }
}