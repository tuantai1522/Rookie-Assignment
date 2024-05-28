import Product from "../features/products/models/product";
import ProductParams from "../features/products/models/productParams";
import axios from "../setup/axios";
import { BACKEND_URL, PAGE_NUMBER, PAGE_SIZE } from "../utils/config";

const fetchAllProducts = async (productParams: ProductParams) => {
  try {
    const category = productParams.categoryType.join("%2C");
    const response = await axios.get<Product[]>(`${BACKEND_URL}/api/product`, {
      params: {
        OrderBy: productParams.orderBy,
        KeyWord: productParams.keyWord,
        CategoryType: category,
        PageNumber: PAGE_NUMBER,
        PageSize: PAGE_SIZE,
      },
    });

    console.log(response);

    return response;
  } catch (error) {
    console.error("Error fetching products:", error);
    throw error;
  }
};

export { fetchAllProducts };
