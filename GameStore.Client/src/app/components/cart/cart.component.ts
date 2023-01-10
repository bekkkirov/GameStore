import {Component, Inject, Input, OnInit} from '@angular/core';
import {CartService} from "../../services/cart.service";
import {CartItem} from "../../models/cart-item";
import {GameService} from "../../services/game.service";
import {OrderItem} from "../../models/order-item";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {SignUpComponent} from "../sign-up/sign-up.component";
import {OrderComponent} from "../order/order.component";

@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
    cart: CartItem[] = [];

    constructor(public cartService: CartService,
                private gameService: GameService,
                private dialogRef: MatDialogRef<CartComponent>,
                private matDialog: MatDialog) {
    }

    ngOnInit(): void {
        let orderItems: OrderItem[];
        this.cartService.cart$.subscribe(r => {
            orderItems = r;
            this.cart.length = 0;
            orderItems.map((item) => {
                this.gameService.getGame(item.gameKey).subscribe(r => this.cart.push({game: r, amount: item.amount}));
            });
        });
    }

    openOrderDialog() {
        this.dialogRef.close();

        this.matDialog.open(OrderComponent, {
            height: '720px',
            width: '500px',
            panelClass: 'sign-in-dialog',
            backdropClass: 'base-backdrop'
        });
    }

    getTotalPrice() {
        let totalPrice: number = 0;

        this.cart.map(i => totalPrice += i.amount * i.game.price);

        return totalPrice;
    }
}
