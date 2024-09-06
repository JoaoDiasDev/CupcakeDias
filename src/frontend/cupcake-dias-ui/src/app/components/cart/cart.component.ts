import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CartItem } from '../../models/cart-item.model';
import { Cart } from '../../models/cart.model';
import { environment } from '../../environments/environment';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { JwtToken } from '../../models/jwt-token.model';
import { Router } from '@angular/router';
import { CartStatus } from '../../enums/cart-status.enum';

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
  ],
})
export class CartComponent implements OnInit {
  cart: Cart | undefined;
  cartItems: CartItem[] = [];
  userId: string | null = null;

  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {}

  /**
   * OnInit lifecycle hook to fetch the user's cart
   */
  ngOnInit(): void {
    this.userId = this.getUserIdFromToken();

    if (this.userId) {
      console.log('User logged in with ID:', this.userId);
      this.fetchCart();
    } else {
      console.error('User not logged in or invalid token');
    }
  }

  /**
   * Get the logged-in user's ID from the JWT token stored in localStorage
   * @returns The user ID (unique_name) or null if the token is invalid
   */
  getUserIdFromToken(): string | null {
    const token = this.authService.getToken();
    if (!token) {
      console.error('No token found in localStorage');
      return null;
    }

    const decodedToken = this.decodeToken(token);
    if (!decodedToken) {
      console.error('Failed to decode token or token is invalid');
      return null;
    }

    return decodedToken.unique_name || null;
  }

  /**
   * Decode JWT token using the JwtToken model
   * @param token The JWT token
   * @returns Decoded JwtToken object or null if decoding fails
   */
  decodeToken(token: string): JwtToken | null {
    try {
      const parts = token.split('.');
      if (parts.length !== 3) {
        throw new Error('Invalid token format');
      }

      const decodedPayload = atob(parts[1]);
      const jwtToken: JwtToken = JSON.parse(decodedPayload) as JwtToken;

      if (!jwtToken.unique_name || !jwtToken.role) {
        throw new Error('Token is missing required fields');
      }

      return jwtToken;
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }

  /**
   * Fetch the user's cart based on the logged-in user's ID
   * @returns void
   */
  fetchCart(): void {
    if (!this.userId) {
      console.error('User ID not available');
      return;
    }

    const apiUrl = `${environment.apiUrl}/carts/user/${this.userId}`;
    this.http.get<Cart>(apiUrl).subscribe({
      next: (data: Cart) => {
        this.cart = data;
        this.cartItems = data.cartItems || [];
      },
      error: (error) => {
        console.error('Failed to fetch cart', error);
      },
    });
  }

  /**
   * Remove a cart item by ID
   * @param cartItemId The ID of the cart item to remove
   */
  removeItem(cartItemId: string): void {
    const apiUrl = `${environment.apiUrl}/cartItems/${cartItemId}`;
    this.http.delete(apiUrl).subscribe({
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
   * @param cartItem The cart item to update
   * @param quantity The new quantity value
   */
  updateQuantity(cartItem: CartItem, quantity: number): void {
    cartItem.quantity = quantity;
    const apiUrl = `${environment.apiUrl}/cartItems`; // Update cart item quantity
    this.http.put(apiUrl, cartItem).subscribe({
      next: () => {
        console.log('Quantity updated successfully');
      },
      error: (error) => {
        console.error('Failed to update quantity', error);
      },
    });
  }

  /**
   * Proceed to checkout, submitting the cart to the backend and changing its status to Completed
   */
  proceedToCheckout(): void {
    const apiUrl = `${environment.apiUrl}/carts/${this.cart?.cartId}/status`;
    const statusPayload = { status: CartStatus.Completed };

    this.http.put(apiUrl, statusPayload).subscribe({
      next: () => {
        console.log('Checkout completed, cart status updated to Completed');
        this.router.navigate(['/checkout-success']); // Redirect after successful checkout
      },
      error: (error) => {
        console.error('Failed to proceed to checkout', error);
      },
    });
  }
}
