import Pagination from "../../../features/common/models/pagination";
import Order from "../../../features/orders/models/order";

interface OrderResponse {
  orders: Order[];
  pagination: Pagination;
}

export default OrderResponse;
