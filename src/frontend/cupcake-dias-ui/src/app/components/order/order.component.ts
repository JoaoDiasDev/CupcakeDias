import { Component, OnInit } from '@angular/core';
import { Order } from '../../models/order.model';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { OrderService } from '../../services/order.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html', // Use external HTML template
  styleUrls: ['./order.component.css'],
  standalone: true,
  imports: [CommonModule, MatCardModule, RouterModule, MatButtonModule],
})
export class OrderComponent implements OnInit {
  orders: Order[] = [];

  constructor(
    private orderService: OrderService,
    private authService: AuthService
  ) {}

  /**
   * Lifecycle hook that fetches the user's orders when the component is initialized.
   */
  ngOnInit(): void {
    this.fetchOrders();
  }

  /**
   * Fetches the user's orders from the server.
   */
  fetchOrders(): void {
    const userId = this.authService.getUserIdFromToken();
    if (!userId) {
      return;
    }
    this.orderService.getOrders(userId).subscribe((data: Order[]) => {
      this.orders = data;
    });
  }
}
