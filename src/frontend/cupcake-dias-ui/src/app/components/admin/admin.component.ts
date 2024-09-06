import { Component } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  standalone: true,
  imports: [MatIcon],
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
}
