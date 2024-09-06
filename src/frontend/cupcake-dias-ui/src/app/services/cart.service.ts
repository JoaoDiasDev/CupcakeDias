import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cart } from '../models/cart.model';
import { CartItem } from '../models/cart-item.model';
import { environment } from '../environments/environment';
import { CartStatus } from '../enums/cart-status.enum';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private apiUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient) {}

  /**
   * Fetch the cart for the current logged-in user
   * @returns Observable of Cart
   */
  getCart(userId: string): Observable<Cart> {
    return this.http.get<Cart>(`${this.apiUrl}/carts/user/${userId}`);
  }

  /**
   * Add an item to the cart
   * @param cartItem The CartItem object to add
   * @returns Observable for the HTTP request
   */
  addCartItem(cartItem: CartItem): Observable<any> {
    return this.http.post(`${this.apiUrl}/cartItems`, cartItem);
  }

  /**
   * Update an existing cart item
   * @param cartItem The updated cart item
   * @returns Observable for the HTTP request
   */
  updateCartItem(cartItem: CartItem): Observable<any> {
    return this.http.put(`${this.apiUrl}/cartItems`, cartItem);
  }

  /**
   * Remove an item from the cart
   * @param cartItemId The ID of the cart item to remove
   * @returns Observable for the HTTP request
   */
  removeCartItem(cartItemId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/cartItems/${cartItemId}`);
  }

  /**
   * Complete the cart by changing its status
   * @param cartId The cart ID to complete
   * @param status The status to update to (Completed, Canceled, etc.)
   * @returns Observable for the HTTP request
   */
  completeCart(cartId: string, status: CartStatus): Observable<any> {
    return this.http.put(`${this.apiUrl}/carts/${cartId}/status`, { status });
  }

  setCartIdLocalStorage(cartId: string) {
    localStorage.setItem('cartId', cartId);
  }

  getCartIdLocalStorage() {
    return localStorage.getItem('cartId');
  }

  removeCartIdLocalStorage() {
    localStorage.removeItem('cartId');
  }
}
