// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BACKEND_URL } from "../../utils/config";
import { getToken } from "../../utils/helper";

// Define a service using a base URL and expected endpoints
export const mainImageApi = createApi({
  reducerPath: "mainImages",
  tagTypes: ["Products"], // must be the same as products api
  baseQuery: fetchBaseQuery({ baseUrl: BACKEND_URL }),
  endpoints: (builder) => ({
    updateMainImage: builder.mutation<string, FormData>({
      query: (body) => ({
        url: `/api/main-image`,
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
export const { useUpdateMainImageMutation } = mainImageApi;
