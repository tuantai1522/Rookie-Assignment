import Pagination from "../../features/common/models/pagination";
import Product from "../../features/products/models/product";

interface ProductResponse {
  products: Product[];
  pagination: Pagination;
}

export default ProductResponse;
