import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order.model';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css'],
  imports: [CommonModule],
})
export class OrderListComponent implements OnInit {
  orders: Order[] = [];
  userId = '1'; // Replace with actual logic to get the logged-in user ID

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  // Load orders for the user
  loadOrders(): void {
    this.orderService.getOrdersByUserId(this.userId).subscribe({
      next: (orders) => {
        this.orders = orders;
      },
      error: (err) => {
        console.error('Error loading orders:', err);
      },
    });
  }

  // View order details
  viewOrder(orderId: string): void {
    // Navigate to the order details view (implement routing accordingly)
  }
}
