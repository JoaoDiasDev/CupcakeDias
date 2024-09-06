import { Component, OnInit } from '@angular/core';
import { CartItem } from '../../models/cart-item.model';
import { Cart } from '../../models/cart.model';
import { AuthService } from '../../services/auth.service';
import { CartService } from '../../services/cart.service';
import { JwtToken } from '../../models/jwt-token.model';
import { Router, RouterModule } from '@angular/router';
import { CartStatus } from '../../enums/cart-status.enum';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatIconModule,
    RouterModule,
  ],
})
export class CartComponent implements OnInit {
  cart: Cart | undefined;
  cartItems: CartItem[] = [];
  userId: string | null = null;

  constructor(
    private cartService: CartService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userId = this.authService.getUserIdFromToken();
    if (this.userId) {
      this.fetchCart();
    }
  }

  decodeToken(token: string): JwtToken | null {
    try {
      const parts = token.split('.');
      const decodedPayload = atob(parts[1]);
      return JSON.parse(decodedPayload) as JwtToken;
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }

  /**
   * Fetch the user's cart
   */
  fetchCart(): void {
    if (!this.userId) return;

    this.cartService.getCart(this.userId).subscribe({
      next: (data: Cart) => {
        this.cart = data;
        this.cartItems = data.cartItems || [];
        this.cartService.setCartIdLocalStorage(this.cart?.cartId ?? '');
      },
      error: (error) => {
        console.error('Failed to fetch cart', error);
      },
    });
  }

  /**
   * Remove a cart item by ID
   */
  removeItem(cartItemId: string): void {
    this.cartService.removeCartItem(cartItemId).subscribe({
      next: () => {
        this.cartItems = this.cartItems.filter(
          (item) => item.cartItemId !== cartItemId
        );
      },
      error: (error) => {
        console.error('Failed to remove item', error);
      },
    });
  }

  /**
   * Update the quantity of a cart item
   */
  updateQuantity(cartItem: CartItem, quantity: number): void {
    cartItem.quantity = quantity;
    if (!cartItem.quantity || cartItem.quantity < 1) {
      cartItem.quantity = 1;
    }
    this.cartService.updateCartItem(cartItem).subscribe({
      next: () => {
        console.log('Quantity updated successfully');
      },
      error: (error) => {
        console.error('Failed to update quantity', error);
      },
    });
  }

  /**
   * Proceed to checkout and complete the cart
   */
  proceedToCheckout(): void {
    if (!this.cart?.cartId) return;

    this.cartService
      .completeCart(this.cart.cartId, CartStatus.Completed)
      .subscribe({
        next: () => {
          console.log('Checkout completed, cart status updated to Completed');
          this.router.navigate(['/checkout-success']);
        },
        error: (error) => {
          console.error('Failed to proceed to checkout', error);
        },
      });
  }
}
