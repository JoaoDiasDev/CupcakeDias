import { Cart } from './cart.model';
import { Cupcake } from './cupcake.model';

export interface CartItem {
  cartItemId?: string; // Maps to CartItemId
  cartId: string; // Maps to CartId
  cupcakeId: string; 
  quantity: number; // Maps to Quantity
  price: number; // Maps to Price
  cupcake?: Cupcake; // Optional related cupcake
  cart?: Cart; // Optional related cart
}
