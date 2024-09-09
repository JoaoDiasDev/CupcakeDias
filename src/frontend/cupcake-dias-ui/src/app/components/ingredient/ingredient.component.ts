import { Component, OnInit } from '@angular/core';
import { Ingredient } from '../../models/ingredient.model';
import { IngredientService } from '../../services/ingredient.service';
import { CommonModule } from '@angular/common';
import {
  MatButton,
  MatIconAnchor,
  MatIconButton,
} from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { FormsModule, NgForm } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-ingredient',
  templateUrl: './ingredient.component.html',
  styleUrls: ['./ingredient.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatToolbarModule,
    MatIconModule,
    MatButton,
    MatIconAnchor,
    MatIconButton,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    MatLabel,
    MatCardModule,
  ],
})
export class IngredientComponent implements OnInit {
  ingredients: Ingredient[] = [];
  ingredientForm: Partial<Ingredient> = {};
  editingIngredient = false;
  ingredientToEditId: string | undefined;
  errorMessage = '';
  successMessage = '';

  constructor(private ingredientService: IngredientService) {}

  ngOnInit(): void {
    this.getIngredients();
  }

  /**
   * Fetches all ingredients from the service.
   */
  getIngredients(): void {
    this.ingredientService.getIngredients().subscribe({
      next: (ingredients) => {
        this.ingredients = ingredients;
      },
      error: (error) => {
        this.errorMessage = 'Failed to load ingredients. Please try again.';
      },
    });
  }

  /**
   * Saves a new or updated ingredient.
   */
  saveIngredient(ingredientForm: NgForm): void {
    if (ingredientForm.invalid) {
      this.errorMessage = 'Please fill all required fields correctly.';
      return;
    }

    if (this.editingIngredient) {
      // Update existing ingredient
      if (this.ingredientToEditId) {
        this.ingredientService
          .updateIngredient(
            this.ingredientToEditId,
            this.ingredientForm as Ingredient
          )
          .subscribe({
            next: () => {
              this.successMessage = 'Ingredient updated successfully!';
              this.getIngredients();
              this.cancelEdit();
            },
            error: () => {
              this.errorMessage =
                'Failed to update ingredient. Please try again.';
            },
          });
      }
    } else {
      // Create a new ingredient
      this.ingredientService
        .createIngredient(this.ingredientForm as Ingredient)
        .subscribe({
          next: () => {
            this.successMessage = 'Ingredient created successfully!';
            this.getIngredients();
            this.ingredientForm = {};
          },
          error: () => {
            this.errorMessage = `Failed to create ingredient. Please try again`;
          },
        });
    }
  }

  /**
   * Sets the form data for editing an ingredient.
   * @param ingredient The ingredient to edit.
   */
  editIngredient(ingredient: Ingredient): void {
    this.editingIngredient = true;
    this.ingredientToEditId = ingredient.ingredientId;
    this.ingredientForm = { ...ingredient };
    this.successMessage = '';
    this.errorMessage = '';
  }

  /**
   * Cancels the editing mode and resets the form.
   */
  cancelEdit(): void {
    this.editingIngredient = false;
    this.ingredientForm = {};
    this.ingredientToEditId = undefined;
  }

  /**
   * Deletes an ingredient by its ID.
   * @param ingredientId The ID of the ingredient to delete.
   */
  deleteIngredient(ingredientId: string | undefined): void {
    if (ingredientId) {
      this.ingredientService.deleteIngredient(ingredientId).subscribe({
        next: () => {
          this.successMessage = 'Ingredient deleted successfully!';
          this.getIngredients();
        },
        error: () => {
          this.errorMessage = 'Failed to delete ingredient. Please try again.';
        },
      });
    }
  }
}
