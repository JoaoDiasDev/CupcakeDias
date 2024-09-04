import { Component, OnInit, Input } from '@angular/core';
import { OrderDetailService } from '../../services/order-detail.service';
import { OrderDetail } from '../../models/order-detail.model';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css'],
  imports: [CommonModule],
})
export class OrderDetailComponent implements OnInit {
  @Input() orderId = ''; // Input the order ID
  orderDetails: OrderDetail[] = [];

  constructor(private orderDetailService: OrderDetailService) {}

  ngOnInit(): void {
    this.loadOrderDetails();
  }

  // Load order details for the specified order
  loadOrderDetails(): void {
    this.orderDetailService.getOrderDetailsByOrderId(this.orderId).subscribe({
      next: (details) => {
        this.orderDetails = details;
      },
      error: (err) => {
        console.error('Error loading order details:', err);
      },
    });
  }
}
