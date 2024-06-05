// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BACKEND_URL } from "../../utils/config";
import { getToken } from "../../utils/helper";

// Define a service using a base URL and expected endpoints
export const categoryApi = createApi({
  reducerPath: "categories",
  tagTypes: ["Categories"],
  baseQuery: fetchBaseQuery({ baseUrl: BACKEND_URL }),
  endpoints: (builder) => ({
    getAllCategories: builder.query<Category[], void>({
      query: () => "/api/category/GetAllCategories",
      providesTags: (result) =>
        result
          ? [
              ...result.map(({ id }) => ({
                type: "Categories" as const,
                id,
              })),
              { type: "Categories", id: "LIST" },
            ]
          : [{ type: "Categories", id: "LIST" }],
    }),

    addCategory: builder.mutation<string, FormData>({
      query: (body) => ({
        url: "/api/category/CreateCategory",
        method: "POST",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
        body,
      }),
      invalidatesTags: [{ type: "Categories", id: "LIST" }],
    }),

    deleteCategory: builder.mutation<number, string>({
      query: (id) => ({
        url: `/api/category/DeleteCategoryById?CategoryId=${id}`,
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
      }),
      invalidatesTags: [{ type: "Categories", id: "LIST" }],
    }),

    updateCategory: builder.mutation<Category, FormData>({
      query: (body) => ({
        url: `/api/category/UpdateCategoryById`,
        method: "PUT",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
        body,
      }),
      invalidatesTags: [{ type: "Categories", id: "LIST" }],
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const {
  useGetAllCategoriesQuery,
  useAddCategoryMutation,
  useDeleteCategoryMutation,
  useUpdateCategoryMutation,
} = categoryApi;
