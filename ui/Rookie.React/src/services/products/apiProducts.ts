// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import Product from "../../features/products/models/product";
import ProductParams from "../../features/products/models/productParams";
import ProductResponse from "./viewModels/listProductResponse";
import { BACKEND_URL } from "../../utils/config";
import { getToken } from "../../utils/helper";

// Define a service using a base URL and expected endpoints
export const productApi = createApi({
  reducerPath: "products",
  tagTypes: ["Products"],
  baseQuery: fetchBaseQuery({ baseUrl: BACKEND_URL }),
  endpoints: (builder) => ({
    getAllProducts: builder.query<ProductResponse, ProductParams | void>({
      query: (args: ProductParams) => ({
        url: "/api/product/GetAllProducts",
        method: "GET",
        params: {
          OrderBy: args.orderBy,
          KeyWord: args.keyWord,
          CategoryType: args.categoryType,
          PageNumber: args.pageNumber,
          PageSize: args.pageSize,
        },
      }),
      //build data after receiving data from server
      transformResponse: (response: Product[], meta) => {
        const headers = meta?.response?.headers;

        const paginationHeader = headers?.get("pagination");

        return {
          products: response,
          pagination: paginationHeader ? JSON.parse(paginationHeader) : null,
        };
      },
      providesTags: (result) =>
        result
          ? [
              ...result.products.map(({ id }) => ({
                type: "Products" as const,
                id,
              })),
              { type: "Products", id: "LIST" },
            ]
          : [{ type: "Products", id: "LIST" }],
    }),

    getProduct: builder.query<Product, string>({
      query: (args) => ({
        url: `/api/product/GetCategoryById/?ProductId=${args}`,
        method: "GET",
      }),
      providesTags: (result, error, id) => [{ type: "Products", id }],
    }),

    addProduct: builder.mutation<string, FormData>({
      query: (body) => ({
        url: "/api/product/CreateProduct",
        method: "POST",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
        body,
      }),
      invalidatesTags: [{ type: "Products", id: "LIST" }],
    }),

    deleteProduct: builder.mutation<number, string>({
      query: (id) => ({
        url: `/api/product/DeleteProductById?ProductId=${id}`,
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
      }),
      invalidatesTags: [{ type: "Products", id: "LIST" }],
    }),

    updateProduct: builder.mutation<Product, FormData>({
      query: (body) => ({
        url: `/api/product/UpdateProductById`,
        method: "PUT",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
        body,
      }),
      invalidatesTags: [{ type: "Products", id: "LIST" }],
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const {
  useGetAllProductsQuery,
  useGetProductQuery,
  useAddProductMutation,
  useDeleteProductMutation,
  useUpdateProductMutation,
} = productApi;
