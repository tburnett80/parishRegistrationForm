import { Directive, ElementRef, Renderer } from '@angular/core';
import { FormConstraintsService } from '../services/form-constraints.service';

@Directive({
    selector: '[npt-constr]'
})

export class TextLengthDirective {
    constructor(private el: ElementRef, private renderer: Renderer, private readonly service: FormConstraintsService) {
        if (!this.el.nativeElement.form.name)
            return;

        this.service.getLimits(this.el.nativeElement.form.name.toLowerCase())
            .then((data) => {
                if (data && data[this.el.nativeElement.name.toLowerCase()]) {
                    this.el.nativeElement.maxLength = data[this.el.nativeElement.name.toLowerCase()];
                } else {
                    if (this.el.nativeElement.type === 'tel')
                        this.el.nativeElement.maxLength = 12;
                }
            });
    }
}