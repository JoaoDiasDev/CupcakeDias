import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-checkout-success',
  template: `
    <div class="success-message">
      <h2>Thank you for your order!</h2>
      <p>Your order has been placed successfully and is being processed.</p>
      <div class="action-buttons">
        <a routerLink="/home" mat-raised-button color="primary">Go to Home</a>
        <a routerLink="/order" mat-raised-button color="accent">View Orders</a>
      </div>
    </div>
  `,
  styleUrls: ['./checkout-success.component.css'],
  standalone: true,
  imports: [RouterModule, MatButtonModule],
})
export class CheckoutSuccessComponent {}
