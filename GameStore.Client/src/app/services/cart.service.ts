import {Inject, Injectable} from '@angular/core';
import {ReplaySubject} from "rxjs";
import {API_BASE_URL} from "../extensions/injection-token";
import {HttpClient} from "@angular/common/http";
import {OrderItem} from "../models/order-item";
import {Order} from "../models/order";

@Injectable({
    providedIn: 'root'
})
export class CartService {
    private cartSource = new ReplaySubject<OrderItem[] | null>(1);
    public cart$ = this.cartSource.asObservable();

    constructor(@Inject(API_BASE_URL) private apiUrl: string,
                private http: HttpClient) {
    }

    addToCart(gameId: number, gameKey: string, amount: number) {
        let cartJson = localStorage.getItem("cart");
        let cart: OrderItem[] = [];

        if(cartJson) {
            let t = JSON.parse(cartJson)

            cart.push(...t);
        }

        let item = cart.find(i => i.gameId == gameId);

        if(item) {
            item.amount += amount;
        }

        else {
            cart.push({gameId, gameKey, amount});
        }

        this.updateCart(cart);
    }

    increaseAmount(gameId: number) {
        let cart: OrderItem[] = [];
        this.cart$.subscribe(c => cart = c);

        let item = cart.find(i => i.gameId == gameId);
        item.amount++;

        this.updateCart(cart);
    }

    decreaseAmount(gameId: number) {
        let cart: OrderItem[] = [];
        this.cart$.subscribe(c => cart = c);

        let item = cart.find(i => i.gameId == gameId);

        if(item.amount == 1) {
            let itemIndex = cart.findIndex(i => i.gameId == gameId);
            cart.splice(itemIndex, 1);
        }

        else {
            item.amount--;
        }

        this.updateCart(cart);
    }

    removeFromCart(gameId: number) {
        let cart: OrderItem[] = [];
        this.cart$.subscribe(c => cart = c);

        let itemIndex = cart.findIndex(i => i.gameId == gameId);
        cart.splice(itemIndex, 1);

        this.updateCart(cart);
    }

    placeOrder(order: Order) {
        return this.http.post(this.apiUrl + 'orders/add', order);
    }

    updateCart(cart: OrderItem[]) {
        localStorage.removeItem("cart");
        localStorage.setItem("cart", JSON.stringify(cart));

        this.setCart(cart);
    }

    setCart(cart: OrderItem[]) {
        this.cartSource.next(cart);
    }

    clearCart() {
        localStorage.removeItem("cart");

        this.setCart(null);
    }
}
