import { Component, OnInit } from '@angular/core';
import { Cupcake } from '../../models/cupcake.model';
import { Ingredient } from '../../models/ingredient.model';
import { CupcakeService } from '../../services/cupcake.service';
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
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select'; // Import for multi-select
import { FormsModule, NgForm } from '@angular/forms';
import { CupcakeUpdateDto } from '../../models/cupcake-update-dto.model';
import { NgxCurrencyDirective } from 'ngx-currency';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-cupcake',
  templateUrl: './cupcake.component.html',
  styleUrls: ['./cupcake.component.css'],
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
    MatSelectModule,
    FormsModule,
    NgxCurrencyDirective,
    MatCardModule,
  ],
})
export class CupcakeComponent implements OnInit {
  cupcakes: Cupcake[] = [];
  ingredients: Ingredient[] = []; // List of all available ingredients
  cupcakeForm: Partial<Cupcake> = {}; // For creating/updating cupcakes
  selectedIngredients: string[] = []; // Holds selected ingredient IDs
  editingCupcake = false;
  cupcakeToEditId: string | undefined;
  errorMessage = '';
  successMessage = '';

  constructor(
    private cupcakeService: CupcakeService,
    private ingredientService: IngredientService // Inject ingredient service
  ) {}

  ngOnInit(): void {
    this.getCupcakes();
    this.getIngredients(); // Fetch available ingredients on load
  }

  /**
   * Fetches all cupcakes from the service.
   */
  getCupcakes(): void {
    this.cupcakeService.getCupcakes().subscribe({
      next: (cupcakes) => {
        this.cupcakes = cupcakes;
      },
      error: (error) => {
        this.errorMessage = `Failed to load cupcakes. Please try again. ${error}`;
      },
    });
  }

  /**
   * Fetches all ingredients for the multi-select dropdown.
   */
  getIngredients(): void {
    this.ingredientService.getIngredients().subscribe({
      next: (ingredients) => {
        this.ingredients = ingredients;
      },
      error: (error) => {
        this.errorMessage = `Failed to load ingredients. Please try again. ${error}`;
      },
    });
  }

  /**
   * Saves a new or updated cupcake and assigns selected ingredients.
   */
  saveCupcake(cupcakeForm: NgForm): void {
    // Check if the form is valid
    if (cupcakeForm.invalid) {
      this.errorMessage = 'Please fill all required fields correctly.';
      return;
    }

    const cupcakeUpdateDto: CupcakeUpdateDto = {
      cupcake: this.cupcakeForm as Cupcake, // Cupcake details
      ingredientIds: this.selectedIngredients, // Selected ingredient IDs
    };

    if (this.editingCupcake) {
      // Update existing cupcake
      if (this.cupcakeToEditId) {
        this.cupcakeService
          .updateCupcake(this.cupcakeToEditId, cupcakeUpdateDto)
          .subscribe({
            next: () => {
              this.successMessage = 'Cupcake updated successfully!';
              this.getCupcakes(); // Refresh the cupcake list
              this.cancelEdit(); // Reset form
            },
            error: (error) => {
              this.errorMessage = `Failed to update cupcake. Please try again. ${error}`;
            },
          });
      }
    } else {
      // Create a new cupcake
      this.cupcakeService.createCupcake(cupcakeUpdateDto).subscribe({
        next: (createdCupcake) => {
          this.successMessage = `Cupcake ${createdCupcake.name} created successfully!`;
          this.getCupcakes(); // Refresh the cupcake list
          this.cupcakeForm = {}; // Reset form
          this.selectedIngredients = [];
        },
        error: (error) => {
          this.errorMessage = `Failed to create cupcake. Please try again. ${error}`;
        },
      });
    }
  }

  /**
   * Sets the form data for editing a cupcake.
   * @param cupcake The cupcake to edit.
   */
  editCupcake(cupcake: Cupcake): void {
    this.editingCupcake = true;
    this.cupcakeToEditId = cupcake.cupcakeId;
    this.cupcakeForm = { ...cupcake };

    // Map and filter out any undefined ingredientId values
    this.selectedIngredients =
      cupcake.cupcakeIngredients
        ?.map((i) => i.ingredientId)
        .filter((id): id is string => !!id) || [];

    this.successMessage = '';
    this.errorMessage = '';
  }

  /**
   * Cancels the editing mode and resets the form.
   */
  cancelEdit(): void {
    this.editingCupcake = false;
    this.cupcakeForm = {};
    this.cupcakeToEditId = undefined;
    this.selectedIngredients = [];
  }

  /**
   * Deletes a cupcake by its ID.
   * @param cupcakeId The ID of the cupcake to delete.
   */
  deleteCupcake(cupcakeId: string | undefined): void {
    if (cupcakeId) {
      this.cupcakeService.deleteCupcake(cupcakeId).subscribe({
        next: () => {
          this.successMessage = 'Cupcake deleted successfully!';
          this.getCupcakes(); // Refresh the list
        },
        error: (error) => {
          this.errorMessage = `Failed to delete cupcake. Please try again. ${error}`;
        },
      });
    }
  }
}
