import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Adal4Service } from 'adal-angular4';
import { AuthHelperService } from '../../services/auth-helper.service';

@Component({
    selector: 'exporter',
    templateUrl: './export.component.html',
    styleUrls: ['./export.component.css']
})
export class ExportComponent {

    constructor(private readonly router: Router, private readonly adalService: Adal4Service,
        private readonly authHelper: AuthHelperService) {
    }

    ngOnInit() {
       
    }
}
