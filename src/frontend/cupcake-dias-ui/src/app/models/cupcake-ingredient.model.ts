import { Cupcake } from './cupcake.model';
import { Ingredient } from './ingredient.model';

export interface CupcakeIngredient {
  cupcakeIngredientId: string; // Maps to CupcakeIngredientId
  cupcakeId: string; // Maps to CupcakeId
  ingredientId: string; // Maps to IngredientId
  cupcake?: Cupcake; // Optional related cupcake
  ingredient?: Ingredient; // Optional related ingredient
}
