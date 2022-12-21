import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {SignInComponent} from "../sign-in/sign-in.component";
import {AuthService} from "../../services/auth.service";
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";
import {EMPTY, mergeMap} from "rxjs";

@Component({
    selector: 'app-nav-bar',
    templateUrl: './nav-bar.component.html',
    styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
    @ViewChild('imageInput') imageInput;

    constructor(private authDialog: MatDialog,
                public authService: AuthService,
                private userService: UserService) {
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

    changeProfileImage() {
        let image = this.imageInput.nativeElement.files[0];

        this.userService.setProfileImage(image).subscribe(image => {
            let user: User;
            this.authService.currentUser$.subscribe(u =>
            {
                user = u;
                user.profileImage = image;
                this.authService.setCurrentUser(user);
            });


        });
    }
}
