import {Component, OnDestroy, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {SignInComponent} from "../sign-in/sign-in.component";
import {AuthService} from "../../services/auth.service";
import {User} from "../../models/user";

@Component({
    selector: 'app-nav-bar',
    templateUrl: './nav-bar.component.html',
    styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
    constructor(private authDialog: MatDialog,
                public authService: AuthService) {
    }

    ngOnInit(): void {
    }

    openAuthDialog() {
        this.authDialog.open(SignInComponent, {
            height: '500px',
            width: '500px',
            panelClass: 'sign-in-dialog',
            backdropClass: 'base-backdrop'
        });
    }

    logout() {
        this.authService.logout();
    }
}
