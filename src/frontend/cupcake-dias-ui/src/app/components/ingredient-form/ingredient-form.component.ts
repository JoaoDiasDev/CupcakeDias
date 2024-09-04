import { Component, OnInit } from '@angular/core';
import { IngredientService } from '../../services/ingredient.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Ingredient } from '../../models/ingredient.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatCard, MatCardTitle } from '@angular/material/card';
import { MatCheckbox } from '@angular/material/checkbox';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-ingredient-form',
  templateUrl: './ingredient-form.component.html',
  styleUrls: ['./ingredient-form.component.css'],
  standalone: true,
  imports: [
    MatFormField,
    MatCard,
    MatCardTitle,
    MatLabel,
    MatCheckbox,
    FormsModule
  ]
})
export class IngredientFormComponent implements OnInit {
  ingredient: Ingredient = { ingredientId: '', name: '', type: '', availability: false };
  isNewIngredient = true;

  constructor(
    private ingredientService: IngredientService,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isNewIngredient = false;
      this.loadIngredient(id);
    }
  }

  // Load ingredient details for editing
  loadIngredient(ingredientId: string): void {
    this.ingredientService.getIngredientById(ingredientId).subscribe({
      next: (ingredient) => (this.ingredient = ingredient),
      error: (err) => console.error('Error loading ingredient:', err),
    });
  }

  // Save ingredient (either create or update)
  saveIngredient(): void {
    if (this.isNewIngredient) {
      this.ingredientService.createIngredient(this.ingredient).subscribe({
        next: () => {
          this.snackBar.open('Ingredient added successfully', 'Close', { duration: 3000 });
          this.router.navigate(['/ingredients']);
        },
        error: (err) => console.error('Error adding ingredient:', err),
      });
    } else {
      this.ingredientService.updateIngredient(this.ingredient.ingredientId, this.ingredient).subscribe({
        next: () => {
          this.snackBar.open('Ingredient updated successfully', 'Close', { duration: 3000 });
          this.router.navigate(['/ingredients']);
        },
        error: (err) => console.error('Error updating ingredient:', err),
      });
    }
  }
}
