import { Cupcake } from './cupcake.model';

export interface CupcakeUpdateDto {
  cupcake: Cupcake;
  ingredientIds: string[];
}
