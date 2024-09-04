import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Cupcake } from '../models/cupcake.model';
import { CartItem } from '../models/cart-item.model';

@Injectable({
  providedIn: 'root',
})
export class CupcakeService {
  private readonly apiUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient) {}

  /**
   * Fetches a list of cupcakes from the backend.
   * @returns Observable containing cupcake data.
   */
  getAllCupcakes(): Observable<Cupcake[]> {
    return this.http.get<Cupcake[]>(`${this.apiUrl}/cupcakes`);
  }

  // Add Cupcake to Cart as CartItem
  addToCart(cartItem: CartItem): Observable<CartItem> {
    return this.http.post<CartItem>(`${environment.apiUrl}/cartitems`, cartItem);
  }
}
