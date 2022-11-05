import {Injectable} from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import {finalize, Observable} from 'rxjs';
import {ProgressSpinnerService} from "../services/progress-spinner.service";

@Injectable()
export class SpinnerInterceptor implements HttpInterceptor {
    requests: number = 0;

    constructor(private progressSpinnerService: ProgressSpinnerService) {
    }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        setTimeout(() => {
            this.progressSpinnerService.isLoading.next(true)
        });

        this.requests++;


        return next.handle(request).pipe(
            finalize(() => {
                if (--this.requests <= 0) {
                    this.requests = 0;
                    this.progressSpinnerService.isLoading.next(false)
                }
            })
        );
    }
}
