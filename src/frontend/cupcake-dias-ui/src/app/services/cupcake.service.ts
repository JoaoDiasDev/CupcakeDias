import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cupcake } from '../models/cupcake.model';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CupcakeService {
  private apiUrl = `${environment.apiUrl}/cupcakes`;

  constructor(private http: HttpClient) {}

  /**
   * Fetches the list of available cupcakes from the backend.
   * @returns Observable of the cupcake list.
   */
  getCupcakes(): Observable<Cupcake[]> {
    return this.http.get<Cupcake[]>(this.apiUrl);
  }

  /**
   * Adds a selected cupcake to the cart.
   * @param cupcake The cupcake to add.
   * @param cartId The cart ID to associate the item with.
   * @param quantity The quantity to add to the cart.
   * @returns Observable indicating the success of the request.
   */
  addCupcakeToCart(
    cartId: string,
    cupcake: Cupcake,
    quantity: number
  ): Observable<any> {
    const cartItem = {
      cartId,
      cupcakeId: cupcake.cupcakeId,
      quantity,
      price: cupcake.price,
    };
    return this.http.post(`${environment.apiUrl}/cartItems`, cartItem);
  }
}
