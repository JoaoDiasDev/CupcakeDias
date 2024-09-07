import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  standalone: true,
  imports: [MatIconModule, MatButtonModule, CommonModule, MatCardModule],
})
export class AdminComponent {
  constructor(private router: Router) {}

  /**
   * Navigate to the cupcake management page.
   */
  manageCupcakes(): void {
    this.router.navigate(['/admin/cupcakes']);
  }

  /**
   * Navigate to the ingredient management page.
   */
  manageIngredients(): void {
    this.router.navigate(['/admin/ingredients']);
  }

   /**
   * Navigate to the order management page.
   */
   manageOrders(): void {
    this.router.navigate(['/admin/orders']);
  }
}
