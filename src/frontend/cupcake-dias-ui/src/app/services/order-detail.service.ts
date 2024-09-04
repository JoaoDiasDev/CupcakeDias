import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OrderDetail } from '../models/order-detail.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class OrderDetailService {
  private apiUrl = `${environment.apiUrl}/orderdetails`;

  constructor(private http: HttpClient) {}

  // Fetch order details for a specific order
  getOrderDetailsByOrderId(orderId: string): Observable<OrderDetail[]> {
    return this.http.get<OrderDetail[]>(`${this.apiUrl}/order/${orderId}`);
  }
}
