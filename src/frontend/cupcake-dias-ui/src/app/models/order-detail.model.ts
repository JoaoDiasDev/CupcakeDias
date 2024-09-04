import { Cupcake } from "./cupcake.model";
import { Order } from "./order.model";

export interface OrderDetail {
  orderDetailId: string;   // Maps to OrderDetailId
  orderId: string;         // Maps to OrderId
  cupcakeId: string;       // Maps to CupcakeId
  quantity: number;        // Maps to Quantity
  price: number;           // Maps to Price
  order?: Order;           // Maps to the related Order
  cupcake?: Cupcake;       // Maps to the related Cupcake
}
