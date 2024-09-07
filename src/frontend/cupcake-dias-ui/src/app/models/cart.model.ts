import { CartItem } from './cart-item.model';
import { User } from './user.model';

export interface Cart {
  cartId?: string; // Maps to CartId
  userId: string; // Maps to UserId
  status: string; // Maps to Status (e.g., Open, Completed)
  createdAt?: Date; // Maps to CreatedAt
  cartItems?: CartItem[]; // Array of CartItems (optional)
  user?: User; // Related user (optional)
}
