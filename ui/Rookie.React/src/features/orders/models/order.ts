import Address from "../../common/models/address";
import OrderItem from "./orderItem";

interface Order {
  id: string;
  userName: string;
  shippingAddress: Address;
  orderDate: Date;
  subTotal: number;
  deliveryFee: number;
  orderItems: Array<OrderItem>;
}

export default Order;
