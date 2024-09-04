import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { Cart } from '../../models/cart.model';
import { CartItem } from '../../models/cart-item.model';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { Order } from '../../models/order.model';
import { OrderStatus } from '../../consts/order-status';
import { CartStatus } from '../../consts/cart-status';
import { v4 as uuidv4 } from 'uuid';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
  standalone: true,
  imports: [MatTableModule, MatButtonModule, CommonModule],
})
export class CartComponent implements OnInit {
  cartId = uuidv4(); // Replace with logic to get the user's active cart
  cart: Cart | undefined;
  cartItems: CartItem[] = [];
  userId = uuidv4(); // TODO Replace with logic to get user active

  constructor(private cartService: CartService, private router: Router) {}

  /**
   * Initializes the component and loads the cart.
   */
  ngOnInit(): void {
    this.loadActiveCart();
  }

  // Load the active cart for the user
  loadActiveCart(): void {
    this.cartService.getActiveCart(this.userId).subscribe({
      next: (cart) => {
        this.cart = cart;
        this.cartId = cart.cartId;
        this.cartItems = cart.cartItems || [];
      },
      error: (err) => {
        console.error('Error loading cart:', err);
      },
    });
  }

  /**
   * Remove item from the cart
   */
  removeItem(cartItemId: string): void {
    this.cartService.removeCartItem(cartItemId).subscribe(() => {
      this.cartItems = this.cartItems.filter(
        (item) => item.cartItemId !== cartItemId
      );
    });
  }

  /**
   * Proceed to checkout
   */
  proceedToCheckout(): void {
    if (!this.cart || !this.cart.user) return;

    const orderGuid = uuidv4();
    // Step 1: Prepare order object
    const newOrder: Order = {
      orderId: orderGuid,
      userId: this.cart.userId,
      orderDate: new Date(),
      status: OrderStatus.Processing,
      orderDetails: this.cartItems.map((item) => ({
        orderDetailId: uuidv4(),
        orderId: orderGuid,
        cupcakeId: item.cupcakeId ?? '',
        quantity: item.quantity,
        price: item.price,
      })),
    };

    // Step 2: Create order and handle responses
    this.cartService.createOrder(newOrder).subscribe({
      next: () => {
        // Step 3: Once the order is created, update cart status to 'Completed'
        this.cartService
          .updateCartStatus(this.cartId, CartStatus.Completed)
          .subscribe({
            next: () => {
              // Step 4: Navigate to the success page after checkout
              this.router.navigate(['/checkout-success']);
            },
            error: (err) => {
              console.error('Error updating cart status:', err);
            },
          });
      },
      error: (err) => {
        console.error('Error creating order:', err);
        // Handle error, show message to the user if necessary
      },
    });
  }

  /**
   * Calculate total price
   */
  getTotalPrice(): number {
    return this.cartItems.reduce(
      (total, item) => total + item.price * item.quantity,
      0
    );
  }
}
