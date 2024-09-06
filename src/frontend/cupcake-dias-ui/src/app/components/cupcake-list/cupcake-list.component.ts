import { Component, OnInit } from '@angular/core';
import { Cupcake } from '../../models/cupcake.model';
import { CupcakeService } from '../../services/cupcake.service';
import { CartService } from '../../services/cart.service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from '../../services/auth.service';
import { Cart } from '../../models/cart.model';
import { CartItem } from '../../models/cart-item.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-cupcake-list',
  templateUrl: './cupcake-list.component.html',
  styleUrls: ['./cupcake-list.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatFormFieldModule,
    MatButtonModule,
    FormsModule,
    MatInputModule,
  ],
})
export class CupcakeListComponent implements OnInit {
  cupcakes: Cupcake[] = [];
  userId = '';
  cartId: string | null;

  constructor(
    private cupcakeService: CupcakeService,
    private cartService: CartService,
    private authService: AuthService,
    private snackBar: MatSnackBar
  ) {
    this.cartId = this.getCartId();
  }

  ngOnInit(): void {
    this.fetchCupcakes();
    let userIdFromToken = this.authService.getUserIdFromToken();
    if (userIdFromToken) {
      this.userId = userIdFromToken;
    }
  }

  /**
   * Fetch the list of available cupcakes from the backend
   */
  fetchCupcakes(): void {
    this.cupcakeService.getCupcakes().subscribe((data: Cupcake[]) => {
      this.cupcakes = data;
    });
  }

  /**
   * Add selected cupcake and quantity to the cart
   * @param cupcake The selected cupcake
   * @param quantity The quantity to be added
   */
  addToCart(cupcake: Cupcake, quantity: number) {
    const cartItem: CartItem = {
      cartId: this.cartId ?? '',
      cupcakeId: cupcake.cupcakeId ?? '',
      quantity: quantity,
      price: cupcake.price,
    };

    this.cartService.addCartItem(cartItem).subscribe({
      next: (response) => {
        console.log('Item added to cart:', response);
        this.snackBar.open('Cupcake added to cart!', 'Close', {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'center',
        });
      },
      error: (error) => {
        console.error('Error adding item to cart:', error);
        this.snackBar.open('Failed to add cupcake to cart.', 'Close', {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'center',
        });
      },
      complete: () => {
        console.log('Add to cart process completed.');
      },
    });
  }

  /**
   * Get the cart ID for the current user (this should be implemented to fetch the active cart ID)
   * @returns the cart ID
   */
  getCartId(): string | null {
    return this.cartService.getCartIdLocalStorage();
  }
}
