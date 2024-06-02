// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BACKEND_URL } from "../../utils/config";
import { getToken } from "../../utils/helper";

// Define a service using a base URL and expected endpoints
export const imageApi = createApi({
  reducerPath: "images",
  tagTypes: ["Products"], // must be the same as products api
  baseQuery: fetchBaseQuery({ baseUrl: BACKEND_URL }),
  endpoints: (builder) => ({
    addImage: builder.mutation<string, FormData>({
      query: (body) => ({
        url: `/api/image/CreateImage`,
        method: "POST",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
        body,
      }),
      invalidatesTags: [{ type: "Products", id: "LIST" }],
    }),

    deleteImage: builder.mutation<number, string>({
      query: (id) => ({
        url: `/api/image/DeleteImage/?ImageId=${id}`,
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
      }),
      invalidatesTags: [{ type: "Products", id: "LIST" }],
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useAddImageMutation, useDeleteImageMutation } = imageApi;
