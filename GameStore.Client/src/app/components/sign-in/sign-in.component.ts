import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {UserService} from "../../services/user.service";
import {SignUpComponent} from "../sign-up/sign-up.component";

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {
    form = new FormGroup({
       "userName" : new FormControl(null, [Validators.required]),
       "password" : new FormControl(null, [Validators.required])
    });

    constructor(private authService: AuthService,
                private userService: UserService,
                private signUpDialog: MatDialog,
                private dialogRef: MatDialogRef<SignInComponent>) {
    }

    ngOnInit(): void {
    }

    signIn() {
        this.authService.signIn(this.form.getRawValue()).subscribe({
            next: () => this.dialogRef.close()
        });
    }

    openSignUpDialog() {
        this.dialogRef.close();

        this.signUpDialog.open(SignUpComponent, {
            height: '750px',
            width: '500px',
            panelClass: 'sign-in-dialog',
            backdropClass: 'base-backdrop'
        });
    }
}
