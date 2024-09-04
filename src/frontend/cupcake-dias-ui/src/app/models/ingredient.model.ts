import { CupcakeIngredient } from "./cupcake-ingredient.model";

export interface Ingredient {
  ingredientId: string; // Maps to IngredientId
  name: string; // Maps to Name
  type: string; // Maps to Type
  availability: boolean; // Maps to Availability
  cupcakeIngredients?: CupcakeIngredient[]; // Maps to ICollection<CupcakeIngredient>
}
