import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ToastrService} from "ngx-toastr";
import {CartService} from "../../services/cart.service";
import {Order} from "../../models/order";
import {CartItem} from "../../models/cart-item";
import {OrderItem} from "../../models/order-item";

@Component({
    selector: 'app-order',
    templateUrl: './order.component.html',
    styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
    form: FormGroup = new FormGroup({
        "firstName": new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(20)]),
        "lastName": new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(20)]),
        "email": new FormControl(null, [Validators.required, Validators.email]),
        "phone": new FormControl(null, [Validators.maxLength(15)]),
        "comment": new FormControl(null, [Validators.maxLength(600)]),
        "paymentType": new FormControl(null, [Validators.required]),
    })


    constructor(private cartService: CartService,
                private toastr: ToastrService) {
    }

    ngOnInit(): void {
    }

    order() {
        let cart: OrderItem[];
        this.cartService.cart$.subscribe(c => cart = c);


        let order: Order = {
            firstName: this.form.get("firstName").value,
            lastName: this.form.get("lastName").value,
            email: this.form.get("email").value,
            phone: this.form.get("phone").value,
            comment: this.form.get("comment").value,
            paymentType: this.form.get("paymentType").value,
            items: cart.map(i => ({gameId: i.gameId, amount: i.amount}))
        }

        this.cartService.placeOrder(order).subscribe(r => {
            this.cartService.clearCart();
            this.toastr.success("Order created");
        });
    }

}
