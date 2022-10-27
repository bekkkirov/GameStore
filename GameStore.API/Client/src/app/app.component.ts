import {Component} from '@angular/core';
import {ProgressSpinnerService} from "./services/progress-spinner.service";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    title = 'GameStore';

    constructor(public progressSpinnerService: ProgressSpinnerService) {
    }
}
