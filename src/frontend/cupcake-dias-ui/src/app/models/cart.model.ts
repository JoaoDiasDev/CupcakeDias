import { CartItem } from "./cart-item.model";
import { User } from "./user.model";

export interface Cart {
  cartId: string;                // Maps to CartId
  userId: string;                // Maps to UserId
  user?: User;                   // Maps to the related User
  status: string;                // Maps to Status
  cartItems?: CartItem[];        // Maps to ICollection<CartItem>
}
