import { Component, Input, Output, ViewChild, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'common-modal',
    templateUrl: './common-modal.component.html',
    styleUrls: ['./common-modal.component.css']
})
export class CommonModalComponent {
    @ViewChild('modalChild') childModal: ModalDirective;
    private _title: string = "";
    private _btn: string = "";

    @Input()
    get title(): string {
        return this._title;
    }

    set title(val: string) {
        this._title = val;
        this.showChange.emit(this._title);
    }

    @Input()
    get btn(): string {
        return this._btn;
    }

    set btn(val: string) {
        this._btn = val;
        this.showChange.emit(this._btn);
    }

    @Output() showChange = new EventEmitter();
    
    show() {
        this.childModal.show();
    }
    hide() {
        this.childModal.hide();
    }
}
