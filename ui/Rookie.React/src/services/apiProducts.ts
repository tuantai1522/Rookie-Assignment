// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BACKEND_URL } from "../utils/config";
import Product from "../features/products/models/product";
import ProductParams from "../features/products/models/productParams";
import Pagination from "../features/common/models/pagination";

interface ProductResponse {
  products: Product[];
  pagination: Pagination;
}

// Define a service using a base URL and expected endpoints
export const productApi = createApi({
  reducerPath: "products",
  baseQuery: fetchBaseQuery({ baseUrl: BACKEND_URL }),
  endpoints: (builder) => ({
    getAllProducts: builder.query<ProductResponse, ProductParams | void>({
      query: (args) => {
        if (!args) return "/api/product";

        const { orderBy, keyWord, categoryType, pageNumber, pageSize } = args;
        const type = categoryType.join("%2C");

        return `/api/product?OrderBy=${orderBy}&KeyWord=${keyWord}&CategoryType=${type}&PageNumber=${pageNumber}&PageSize=${pageSize}`;
      },
      transformResponse: (response: Product[], meta) => {
        const headers = meta?.response?.headers;

        const paginationHeader = headers?.get("pagination");

        return {
          products: response,
          pagination: paginationHeader ? JSON.parse(paginationHeader) : null,
        };
      },
    }),
    getProduct: builder.query<Product, string>({
      query: (args) => `/api/Product/${args}`,
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useGetAllProductsQuery, useGetProductQuery } = productApi;
