import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ingredient } from '../models/ingredient.model';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class IngredientService {
  private apiUrl = `${environment.apiUrl}/ingredients`;

  constructor(private http: HttpClient) {}

  /**
   * Fetches the list of available ingredients from the backend.
   * @returns Observable of the ingredient list.
   */
  getIngredients(): Observable<Ingredient[]> {
    return this.http.get<Ingredient[]>(this.apiUrl);
  }

  /**
   * Creates a new ingredient.
   * @param ingredientData Data for the new ingredient.
   * @returns Observable indicating the success of the creation.
   */
  createIngredient(ingredientData: Ingredient): Observable<Ingredient> {
    return this.http.post<Ingredient>(this.apiUrl, ingredientData);
  }

  /**
   * Updates an existing ingredient by its ID.
   * @param ingredientId The ID of the ingredient to update.
   * @param ingredientData The updated ingredient data.
   * @returns Observable indicating the success of the update.
   */
  updateIngredient(
    ingredientId: string,
    ingredientData: Ingredient
  ): Observable<Ingredient> {
    return this.http.put<Ingredient>(
      `${this.apiUrl}/${ingredientId}`,
      ingredientData
    );
  }

  /**
   * Deletes an ingredient by its ID.
   * @param ingredientId The ID of the ingredient to delete.
   * @returns Observable indicating the success of the deletion.
   */
  deleteIngredient(ingredientId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${ingredientId}`);
  }
}
