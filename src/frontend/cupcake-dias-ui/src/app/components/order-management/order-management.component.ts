import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order.model';
import { OrderStatus } from '../../enums/order-status.enum';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../services/auth.service';
import { forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-order-management',
  templateUrl: './order-management.component.html',
  styleUrls: ['./order-management.component.css'],
  standalone: true,
  imports: [
    FormsModule,
    MatCardModule,
    MatInputModule,
    CommonModule,
    MatOptionModule,
    MatSelectModule,
  ],
})
export class OrderManagementComponent implements OnInit {
  orders: Order[] = [];
  filteredOrders: Order[] = [];
  orderStatuses = Object.values(OrderStatus);
  searchTerm = '';

  constructor(
    private orderService: OrderService,
    private authService: AuthService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.fetchOrders();
  }

  /**
   * Fetches all orders from the server and loads user details for each order.
   */
  fetchOrders(): void {
    this.orderService.getAllOrders().subscribe({
      next: (orders) => {
        // Filter out completed and cancelled orders
        this.orders = orders.filter(
          (order) =>
            order.status !== OrderStatus.Completed &&
            order.status !== OrderStatus.Cancelled
        );

        // Fetch user details for each order and store it in the order.user field
        const userRequests = this.orders.map((order) =>
          this.authService.getUserById(order.userId).pipe(
            map((user) => {
              order.user = user;
              return order;
            })
          )
        );

        // Wait for all user requests to complete
        forkJoin(userRequests).subscribe({
          next: (ordersWithUsers) => {
            this.orders = ordersWithUsers;
            this.filterOrders(); // Filter orders initially
          },
          error: (err) => {
            console.error('Error fetching users', err);
          },
        });
      },
      error: (err) => {
        console.error('Error fetching orders', err);
      },
    });
  }

  /**
   * Filters orders based on the search term.
   */
  filterOrders(): void {
    if (this.searchTerm.trim()) {
      this.filteredOrders = this.orders.filter((order) =>
        order.user?.name.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    } else {
      this.filteredOrders = [...this.orders];
    }
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
