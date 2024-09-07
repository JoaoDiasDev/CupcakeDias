import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../models/order.model';
import { OrderDetail } from '../models/order-detail.model';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private apiUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient) {}

  /**
   * Fetches the orders for the given user ID
   * @param userId The user ID to fetch orders for
   * @returns An Observable of Order[]
   */
  getOrders(userId: string): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/orders/${userId}`);
  }

  /**
   * Fetches the order details for the given order ID
   * @param orderId The order ID to fetch order details for
   * @returns An Observable of OrderDetail[]
   */
  getOrderDetails(orderId: string): Observable<OrderDetail[]> {
    return this.http.get<OrderDetail[]>(
      `${this.apiUrl}/orderdetails/order/${orderId}`
    );
  }

  /**
   * Fetch all orders
   */
  getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/orders`);
  }

  /**
   * Update order status
   */
  updateOrderStatus(orderId: string, status: string): Observable<undefined> {
    return this.http.put<undefined>(`${this.apiUrl}/orders/${orderId}/status`, {
      status,
    });
  }
}
