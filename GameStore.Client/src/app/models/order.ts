import {OrderItem} from "./order-item";

export interface Order {
    firstName: string,
    lastName: string,
    email: string,
    phone: string,
    comment?: string,
    paymentType: number,
    items: {gameId: number, amount: number}[]
}
