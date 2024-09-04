import { Cupcake } from "./cupcake.model";
import { Ingredient } from "./ingredient.model";

export interface CupcakeIngredient {
  cupcakeIngredientId: string;   // Maps to CupcakeIngredientId
  cupcakeId: string;             // Maps to CupcakeId
  ingredientId: string;          // Maps to IngredientId
  cupcake?: Cupcake;             // Maps to the related Cupcake
  ingredient?: Ingredient;       // Maps to the related Ingredient
}
