import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service'; // Assume you have an OrderService to fetch/update orders
import { Order } from '../../models/order.model';
import { OrderStatus } from '../../enums/order-status.enum'; // Assume you have an enum for OrderStatus
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-order-management',
  templateUrl: './order-management.component.html',
  styleUrls: ['./order-management.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatSelectModule,
    FormsModule,
  ],
})
export class OrderManagementComponent implements OnInit {
  orders: Order[] = [];
  orderStatuses = Object.values(OrderStatus); // Enum values for the dropdown

  constructor(
    private orderService: OrderService,
    private authService: AuthService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.fetchOrders();
  }

  /**
   * Fetches all orders from the server.
   */
  fetchOrders(): void {
    this.orderService.getAllOrders().subscribe({
      next: (data) => {
        this.orders = data.filter(
          (order) =>
            order.status !== OrderStatus.Completed &&
            order.status !== OrderStatus.Cancelled
        );
      },
      error: (err) => {
        console.error('Error fetching orders', err);
      },
    });
  }

  /**
   * Updates the status of an order.
   */
  updateOrderStatus(orderId: string, status: OrderStatus): void {
    this.orderService.updateOrderStatus(orderId, status).subscribe({
      next: () => {
        this.snackBar.open('Order status updated successfully', 'Close', {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'center',
        });
        this.fetchOrders();
      },
      error: () => {
        this.snackBar.open('Failed to update order status', 'Close', {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'center',
        });
      },
    });
  }
}
