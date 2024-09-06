import { Component } from '@angular/core';

@Component({
  selector: 'app-checkout-success',
  template: `
    <h2>Thank you for your order!</h2>
    <p>Your order has been placed successfully and is being processed.</p>
    <a routerLink="/home" mat-raised-button color="primary">Go to Home</a>
  `,
  styleUrls: ['./checkout-success.component.css'],
  standalone: true,
})
export class CheckoutSuccessComponent {}
