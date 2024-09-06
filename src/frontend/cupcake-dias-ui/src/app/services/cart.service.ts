import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cart } from '../models/cart.model';
import { environment } from '../environments/environment';
import { CartItem } from '../models/cart-item.model';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private apiUrl = `${environment.apiUrl}/carts`;

  constructor(private http: HttpClient) {}

  getCart(): Observable<Cart> {
    return this.http.get<Cart>(`${this.apiUrl}/user/current`);
  }

  addCartItem(cupcake: any, quantity: number): Observable<any> {
    const cartItem = { cupcakeId: cupcake.cupcakeId, quantity };
    return this.http.post(`${this.apiUrl}/items`, cartItem);
  }

  updateCartItem(cartItem: CartItem): Observable<any> {
    return this.http.put(
      `${this.apiUrl}/items/${cartItem.cartItemId}`,
      cartItem
    );
  }

  completeCart(cartId: string, status: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/${cartId}/status`, { status });
  }

  completeOrder(status: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/orders/complete`, { status });
  }
}
