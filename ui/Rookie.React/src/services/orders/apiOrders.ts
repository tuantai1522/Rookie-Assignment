// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BACKEND_URL } from "../../utils/config";
import OrderResponse from "./viewModels/listOrderResponse";
import OrderParams from "../../features/orders/models/orderParams";
import Order from "../../features/orders/models/order";
import { getToken } from "../../utils/helper";

// Define a service using a base URL and expected endpoints
export const orderApi = createApi({
  reducerPath: "orders",
  tagTypes: ["Orders"],
  baseQuery: fetchBaseQuery({ baseUrl: BACKEND_URL }),
  endpoints: (builder) => ({
    getAllOrders: builder.query<OrderResponse, OrderParams | void>({
      query: (args: OrderParams) => ({
        url: "/api/order/GetAllOrders",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
        method: "GET",
        params: {
          OrderBy: args.orderBy,
          PageNumber: args.pageNumber,
          PageSize: args.pageSize,
          MinTotal: args.minTotal,
          MaxTotal: args.maxTotal,
        },
      }),

      //build data after receiving data from server
      transformResponse: (response: Order[], meta) => {
        const headers = meta?.response?.headers;

        const paginationHeader = headers?.get("pagination");

        return {
          orders: response,
          pagination: paginationHeader ? JSON.parse(paginationHeader) : null,
        };
      },
      providesTags: (result) =>
        result
          ? [
              ...result.orders.map(({ id }) => ({
                type: "Orders" as const,
                id,
              })),
              { type: "Orders", id: "LIST" },
            ]
          : [{ type: "Orders", id: "LIST" }],
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useGetAllOrdersQuery } = orderApi;
