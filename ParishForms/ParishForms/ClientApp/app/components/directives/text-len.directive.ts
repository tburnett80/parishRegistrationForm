import { Directive, ElementRef, Renderer, Input } from '@angular/core';
import { FormConstraintsService } from '../services/form-constraints.service';

@Directive({
    selector: '[text-len]'
})

export class TextLengthDirective {
    @Input('text-len') form: string;

    constructor(el: ElementRef, renderer: Renderer, private readonly service: FormConstraintsService) {
        let elem = el.nativeElement;

        console.log('Form: ', this.form);
        console.log('Directive elementRef: ', el);
        console.log('Directive renderer: ', renderer);
    }
}

/* http://localhost:3020/api/directory/limits
{
	"AdultOneFirstName": 64,
	"AdultTwoFirstName": 64,
    "FamilyName": 64,
	"OtherFamily": 1024,
	"City": 64,
	"Street": 255,
	"Zip": 10,
	"Number": 15,
	"Email": 255
}
*/