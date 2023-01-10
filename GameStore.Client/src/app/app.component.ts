import {Component, OnInit} from '@angular/core';
import {ProgressSpinnerService} from "./services/progress-spinner.service";
import {AuthService} from "./services/auth.service";
import {UserService} from "./services/user.service";
import {CartService} from "./services/cart.service";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
    title = 'GameStore';

    constructor(public progressSpinnerService: ProgressSpinnerService,
                private authService: AuthService,
                private userService: UserService,
                private cartService: CartService) {
    }

    ngOnInit(): void {
        let token = localStorage.getItem('authToken');

        if(token) {
            this.userService.getCurrentUser().subscribe(result => this.authService.setCurrentUser(result));
        }

        let cart = localStorage.getItem('cart');

        if(cart) {
            this.cartService.setCart(JSON.parse(cart));
        }
    }
}
