// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BACKEND_URL } from "../utils/config";

interface CategoryResponse {
  categories: Category[];
}

// Define a service using a base URL and expected endpoints
export const categoryApi = createApi({
  reducerPath: "categories",
  baseQuery: fetchBaseQuery({ baseUrl: BACKEND_URL }),
  endpoints: (builder) => ({
    getAllCategories: builder.query<CategoryResponse, void>({
      query: () => "/api/category",
      transformResponse: (response: Category[]): CategoryResponse => {
        return { categories: response };
      },
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useGetAllCategoriesQuery } = categoryApi;
