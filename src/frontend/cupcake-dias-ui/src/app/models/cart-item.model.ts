import { Cart } from './cart.model';
import { Cupcake } from './cupcake.model';

export interface CartItem {
  cartItemId: string; // Maps to CartItemId
  cartId: string; // Maps to CartId
  cart?: Cart; // Maps to the related Cart
  cupcakeId?: string; // Maps to CupcakeId (nullable)
  cupcake?: Cupcake; // Maps to the related Cupcake
  quantity: number; // Maps to Quantity
  price: number; // Maps to Price
}
