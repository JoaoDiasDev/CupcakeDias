import { CartItem } from './cart-item.model';
import { CupcakeIngredient } from './cupcake-ingredient.model';
import { OrderDetail } from './order-detail.model';

export interface Cupcake {
  cupcakeId: string; // Maps to CupcakeId
  name: string; // Maps to Name
  baseFlavor: string; // Maps to BaseFlavor
  price: number; // Maps to Price
  description: string; // Maps to Description
  imageUrl: string; // Maps to ImageUrl
  orderDetails?: OrderDetail[]; // Maps to ICollection<OrderDetail>
  cupcakeIngredients?: CupcakeIngredient[]; // Maps to ICollection<CupcakeIngredient>
  cartItems?: CartItem[]; // Maps to ICollection<CartItem>
}
