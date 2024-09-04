import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cart } from '../models/cart.model';
import { CartItem } from '../models/cart-item.model';
import { environment } from '../../environments/environment';
import { Order } from '../models/order.model';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private apiUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient) {}

  // Fetch a cart by its ID
  getCartById(cartId: string): Observable<Cart> {
    return this.http.get<Cart>(`${this.apiUrl}/${cartId}`);
  }

  // Fetch cart items by Cart ID
  getCartItemsByCartId(cartId: string): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(`${this.apiUrl}/cartitems/cart/${cartId}`);
  }

  // Add a cart item
  addCartItem(cartItem: CartItem): Observable<CartItem> {
    return this.http.post<CartItem>(`${this.apiUrl}/cartitems`, cartItem);
  }

  // Update the cart
  updateCart(cartId: string, cart: Cart): Observable<undefined> {
    return this.http.put<undefined>(`${this.apiUrl}/carts/${cartId}`, cart);
  }

  // Remove an item from the cart
  removeCartItem(cartItemId: string): Observable<undefined> {
    return this.http.delete<undefined>(
      `${this.apiUrl}/cartitems/${cartItemId}`
    );
  }

  // Delete the entire cart
  deleteCart(cartId: string): Observable<undefined> {
    return this.http.delete<undefined>(`${this.apiUrl}/carts/${cartId}`);
  }

  // Update the cart status
  updateCartStatus(cartId: string, status: string): Observable<undefined> {
    const updateData = { status };
    return this.http.put<undefined>(
      `${this.apiUrl}/carts/${cartId}`,
      updateData
    );
  }

  // Create order and add order details
  createOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(`${this.apiUrl}/orders`, order);
  }

  // Get the user's active cart (newest cart with status 'Open')
  getActiveCart(userId: string): Observable<Cart> {
    return this.http.get<Cart>(
      `${this.apiUrl}/carts/user/${userId}?status=Open`
    );
  }
}
