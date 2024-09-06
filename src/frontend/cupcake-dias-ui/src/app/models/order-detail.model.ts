import { Cupcake } from './cupcake.model';
import { Order } from './order.model';

export interface OrderDetail {
  orderDetailId?: string; // Maps to OrderDetailId (Guid)
  orderId: string; // Maps to OrderId
  cupcakeId: string; // Maps to CupcakeId
  quantity: number; // Maps to Quantity
  price: number; // Maps to Price
  cupcake?: Cupcake; // Maps to related Cupcake (optional for now)
  order?: Order; // Maps to related Order (optional for now)
}
