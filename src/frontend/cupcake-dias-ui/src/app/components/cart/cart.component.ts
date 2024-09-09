import { Component, OnInit } from '@angular/core';
import { CartItem } from '../../models/cart-item.model';
import { Cart } from '../../models/cart.model';
import { AuthService } from '../../services/auth.service';
import { CartService } from '../../services/cart.service';
import { JwtToken } from '../../models/jwt-token.model';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { CartStatus } from '../../enums/cart-status.enum';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';

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
  totalItems = 0;
  totalPrice = 0;
  private routerSubscription: Subscription | undefined;

  constructor(
    private cartService: CartService,
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  /**
   * Lifecycle hook that fetches the user's cart when the component is initialized,
   * given that the user is logged in.
   */
  ngOnInit(): void {
    this.userId = this.authService.getUserIdFromToken();
    if (this.userId) {
      this.fetchCart();
    }

    this.routerSubscription = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.fetchCart(); // Trigger cart fetch on every navigation
      }
    });
  }

  ngOnDestroy(): void {
    // Clean up the router event subscription
    if (this.routerSubscription) {
      this.routerSubscription.unsubscribe();
    }
  }

  /**
   * Decode a JWT token into a JSON object. If the token is invalid,
   * this function will return null.
   *
   * @param token The JWT token to decode.
   *
   * @returns The decoded JSON object, or null if the token was invalid.
   */
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
        this.calculateCartSummary();
      },
      error: (error) => {
        console.error('Failed to fetch cart', error);
      },
    });
  }

  /**
   * Calculate the total number of items and the total price.
   */
  calculateCartSummary(): void {
    this.totalItems = this.cartItems.reduce(
      (total, item) => total + item.quantity,
      0
    );
    this.totalPrice = this.cartItems.reduce(
      (total, item) => total + item.price * item.quantity,
      0
    );
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
        this.calculateCartSummary();
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
        this.snackBar.open('Quantity updated successfully', 'Close', {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'center',
        });
        this.calculateCartSummary();
      },
      error: () => {
        this.snackBar.open('Failed to update quantity', 'Close', {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'center',
        });
      },
    });
  }

  /**
   * Proceed to checkout and complete the cart
   */
  proceedToCheckout(): void {
    if (this.cart) {
      this.cartService.checkout(this.cart).subscribe({
        next: () => {
          this.snackBar.open(
            'Order placed successfully! Check your email.',
            'Close',
            {
              duration: 3000,
              verticalPosition: 'top',
              horizontalPosition: 'center',
            }
          );
          this.cartService.removeCartIdLocalStorage();
          this.router.navigate(['/checkout-success']);
        },
        error: () => {
          this.snackBar.open(
            'Failed to place the order. Please try again.',
            'Close',
            {
              duration: 3000,
              verticalPosition: 'top',
              horizontalPosition: 'center',
            }
          );
        },
      });
    }
  }
}
