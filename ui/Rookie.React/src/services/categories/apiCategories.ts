// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BACKEND_URL } from "../../utils/config";

// Define a service using a base URL and expected endpoints
export const categoryApi = createApi({
  reducerPath: "categories",
  baseQuery: fetchBaseQuery({ baseUrl: BACKEND_URL }),
  endpoints: (builder) => ({
    getAllCategories: builder.query<Category[], void>({
      query: () => "/api/category/GetAllCategories",
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useGetAllCategoriesQuery } = categoryApi;
