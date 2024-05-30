// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import LoginUserRequest from "./viewModels/LoginUserRequest";
import LoginUserResponse from "./viewModels/LoginUserResponse";
import User from "../../features/users/models/user";
import { BACKEND_URL } from "../../utils/config";
import { getToken } from "../../utils/helper";

// Define a service using a base URL and expected endpoints
export const userApi = createApi({
  reducerPath: "users",
  tagTypes: ["Users"],
  baseQuery: fetchBaseQuery({ baseUrl: BACKEND_URL }),
  endpoints: (builder) => ({
    loginUser: builder.mutation<LoginUserResponse, LoginUserRequest>({
      query: (credentials) => ({
        url: "/api/user/LoginUser",
        method: "POST",

        body: credentials,
      }),
    }),
    getCurrentUser: builder.query<User, void>({
      query: () => ({
        url: "/api/user/GetCurrentUser",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
      }),
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useLoginUserMutation, useGetCurrentUserQuery } = userApi;
