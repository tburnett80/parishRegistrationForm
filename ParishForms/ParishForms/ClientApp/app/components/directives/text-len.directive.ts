import { Directive, ElementRef, Renderer, Input } from '@angular/core';
import { FormConstraintsService } from '../services/form-constraints.service';
import { Observable, Subscription } from 'rxjs';

@Directive({
    selector: '[npt-constr]'
})

export class TextLengthDirective {
    constructor(private el: ElementRef, private renderer: Renderer, private readonly service: FormConstraintsService) { }

    ngOnInit() {
        if (!this.el.nativeElement.form.name) 
            return;

        this.service.getLimits(this.el.nativeElement.form.name.toLowerCase())
            .then((data) => {
                if (data && data[this.el.nativeElement.name.toLowerCase()]) {
                    this.el.nativeElement.maxLength = data[this.el.nativeElement.name.toLowerCase()];
                } 

                switch (this.el.nativeElement.type) {
                    case "tel":
                        if (data[this.el.nativeElement.type])
                            this.el.nativeElement.maxLength = data[this.el.nativeElement.type];
                        else
                            this.el.nativeElement.maxLength = 12;
                        this.el.nativeElement.pattern = "^[0-9]{3}(-)*[0-9]{3}(-)*[0-9]{4}$";
                    case "email":
                        if (data[this.el.nativeElement.type])
                            this.el.nativeElement.maxLength = data[this.el.nativeElement.type];
                        else
                            this.el.nativeElement.maxLength = 255;
                        this.el.nativeElement.pattern = "^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i";
                    case "number":
                        this.el.nativeElement.pattern = "^\d+$";
                }
            });
    }
}