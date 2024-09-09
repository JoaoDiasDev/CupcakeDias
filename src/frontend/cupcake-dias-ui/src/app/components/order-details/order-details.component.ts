import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { OrderDetail } from '../../models/order-detail.model';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    RouterModule,
  ],
})
export class OrderDetailsComponent implements OnInit {
  orderDetails: OrderDetail[] = [];
  orderId: string | null = '';
  totalAmount = 0;

  constructor(
    private route: ActivatedRoute,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    this.orderId = this.route.snapshot.paramMap.get('orderId');
    if (this.orderId) {
      this.orderService
        .getOrderDetails(this.orderId)
        .subscribe((data: OrderDetail[]) => {
          this.orderDetails = data;
          this.calculateTotalAmount();
        });
    }
  }

  /**
   * Calculate the total amount for the order.
   */
  calculateTotalAmount(): void {
    this.totalAmount = this.orderDetails.reduce(
      (total, item) => total + item.price * item.quantity,
      0
    );
  }
}
