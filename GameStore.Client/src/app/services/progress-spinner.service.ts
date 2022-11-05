import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class ProgressSpinnerService {
    isLoading: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    constructor() {
    }
}
