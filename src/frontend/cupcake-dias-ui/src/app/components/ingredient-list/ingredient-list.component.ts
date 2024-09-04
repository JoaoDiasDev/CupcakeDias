import { Component, OnInit } from '@angular/core';
import { IngredientService } from '../../services/ingredient.service';
import { Ingredient } from '../../models/ingredient.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { MatCard, MatCardTitle } from '@angular/material/card';
import { MatList, MatListItem } from '@angular/material/list';

@Component({
  selector: 'app-ingredient-list',
  templateUrl: './ingredient-list.component.html',
  styleUrls: ['./ingredient-list.component.css'],
  standalone: true,
  imports: [MatCard, MatCardTitle, MatList, MatListItem],
})
export class IngredientListComponent implements OnInit {
  ingredients: Ingredient[] = [];

  constructor(
    private ingredientService: IngredientService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadIngredients();
  }

  // Fetch all ingredients
  loadIngredients(): void {
    this.ingredientService.getIngredients().subscribe({
      next: (ingredients) => (this.ingredients = ingredients),
      error: (err) => console.error('Error fetching ingredients:', err),
    });
  }

  // Delete an ingredient
  deleteIngredient(ingredientId: string): void {
    if (confirm('Are you sure you want to delete this ingredient?')) {
      this.ingredientService.deleteIngredient(ingredientId).subscribe({
        next: () => {
          this.snackBar.open('Ingredient deleted successfully', 'Close', {
            duration: 3000,
          });
          this.loadIngredients();
        },
        error: (err) => console.error('Error deleting ingredient:', err),
      });
    }
  }

  // Navigate to edit or add ingredient
  editIngredient(ingredientId: string): void {
    this.router.navigate(['/ingredients', ingredientId]);
  }

  addIngredient(): void {
    this.router.navigate(['/ingredients/new']);
  }
}
