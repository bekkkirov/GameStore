import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {MatDialogRef} from "@angular/material/dialog";

@Component({
    selector: 'app-sign-up',
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {
    form = new FormGroup({
       "userName": new FormControl(null,
           [Validators.required, Validators.minLength(5), Validators.maxLength(20)]),
       "email": new FormControl(null, [Validators.required, Validators.email, Validators.maxLength(50)]),
       "firstName": new FormControl(null,
           [Validators.required, Validators.minLength(2), Validators.maxLength(20)]),
       "lastName": new FormControl(null,
           [Validators.required, Validators.minLength(2), Validators.maxLength(20)]),
       "password": new FormControl(null,
           [Validators.required, Validators.minLength(5), Validators.maxLength(20)])
    });


    constructor(private authService: AuthService,
                private dialogRef: MatDialogRef<SignUpComponent>) {
    }

    ngOnInit(): void {
    }

    signUp() {
        this.authService.signUp(this.form.getRawValue()).subscribe({
            next: () => this.dialogRef.close()
        });
    }
}
