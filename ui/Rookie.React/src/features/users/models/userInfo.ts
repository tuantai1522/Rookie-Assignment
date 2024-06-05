import Order from "../../orders/models/order";

interface UserInfo {
  id: string;
  email: string;
  userName: string;
  firstName: string;
  lastName: string;
  orders: Array<Order>;
}

export default UserInfo;
