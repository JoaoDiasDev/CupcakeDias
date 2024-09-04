import { Component, OnInit } from '@angular/core';
import { CupcakeService } from '../../services/cupcake.service';
import { Cupcake } from '../../models/cupcake.model';
import { CartItem } from '../../models/cart-item.model';
import { MatSnackBar } from '@angular/material/snack-bar'; // Optional for notification
import { v4 as uuidv4 } from 'uuid'; // Use UUID to generate cartItemId for the cart item
import { CommonModule } from '@angular/common';
import {
  MatCard,
  MatCardActions,
  MatCardContent,
  MatCardHeader,
  MatCardSubtitle,
  MatCardTitle,
} from '@angular/material/card';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { CartService } from '../../services/cart.service';
import { Cart } from '../../models/cart.model';
import { AuthService } from '../../services/auth.service';

@Component({
  standalone: true,
  selector: 'app-cupcake-list',
  templateUrl: './cupcake-list.component.html',
  styleUrls: ['./cupcake-list.component.css'],
  imports: [
    MatCard,
    MatCardHeader,
    MatCardTitle,
    MatCardSubtitle,
    MatCardContent,
    MatCardActions,
    MatFormField,
    MatLabel,
    CommonModule,
  ],
})
export class CupcakeListComponent implements OnInit {
  cupcakes: Cupcake[] = [];
  activeCart: Cart = {} as Cart;
  userId = '';

  constructor(
    private cupcakeService: CupcakeService,
    private snackBar: MatSnackBar,
    private cartService: CartService,
    private authService: AuthService
  ) {
    this.userId = this.authService.getUserId() ?? '';

    this.cartService.getActiveCart(this.userId).subscribe({
      next: (cart) => {
        this.activeCart = cart;
      },
      error: (err) => {
        console.error('Error getting active cart:', err);
      },
    });
  }

  ngOnInit(): void {
    this.loadCupcakes();
  }

  // Load all cupcakes from the service
  loadCupcakes(): void {
    this.cupcakeService.getAllCupcakes().subscribe({
      next: (cupcakes) => {
        this.cupcakes = cupcakes;
      },
      error: (err) => {
        console.error('Error loading cupcakes:', err);
      },
    });
  }

  // Add cupcake to the cart
  addToCart(cupcake: Cupcake, quantity: number): void {
    const cartItem: CartItem = {
      cartItemId: uuidv4(), // Generate unique ID
      cartId: this.activeCart?.cartId, //TODO Replace with logic to get the active cart ID
      cupcakeId: cupcake.cupcakeId,
      quantity,
      price: cupcake.price,
    };

    this.cupcakeService.addToCart(cartItem).subscribe({
      next: () => {
        this.snackBar.open(`${cupcake.name} added to cart!`, 'Close', {
          duration: 3000,
        });
      },
      error: (err) => {
        console.error('Error adding to cart:', err);
        this.snackBar.open(`Failed to add ${cupcake.name} to cart.`, 'Close', {
          duration: 3000,
        });
      },
    });
  }
}
