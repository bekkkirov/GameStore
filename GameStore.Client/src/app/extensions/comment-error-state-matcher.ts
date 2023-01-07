import {FormControl, FormGroupDirective, NgForm} from "@angular/forms";
import {ErrorStateMatcher} from "@angular/material/core";

export class CommentErrorStateMatcher implements ErrorStateMatcher {
    isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
        return false;
    }
}
