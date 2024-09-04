import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ingredient } from '../models/ingredient.model';
import { environment } from '../../environments/environment'; // Make sure you have this set up

@Injectable({
  providedIn: 'root',
})
export class IngredientService {
  private apiUrl = `${environment.apiUrl}/ingredients`; // API endpoint for ingredients

  constructor(private http: HttpClient) {}

  // Fetch all ingredients
  getIngredients(): Observable<Ingredient[]> {
    return this.http.get<Ingredient[]>(this.apiUrl);
  }

  // Fetch a single ingredient by ID
  getIngredientById(ingredientId: string): Observable<Ingredient> {
    return this.http.get<Ingredient>(`${this.apiUrl}/${ingredientId}`);
  }

  // Create a new ingredient
  createIngredient(ingredient: Ingredient): Observable<Ingredient> {
    return this.http.post<Ingredient>(this.apiUrl, ingredient);
  }

  // Update an existing ingredient
  updateIngredient(
    ingredientId: string,
    ingredient: Ingredient
  ): Observable<Ingredient> {
    return this.http.put<Ingredient>(
      `${this.apiUrl}/${ingredientId}`,
      ingredient
    );
  }

  // Delete an ingredient by ID
  deleteIngredient(ingredientId: string): Observable<undefined> {
    return this.http.delete<undefined>(`${this.apiUrl}/${ingredientId}`);
  }
}
