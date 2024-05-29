// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BACKEND_URL } from "../../utils/config";
import LoginUserRequest from "./viewModels/LoginUserRequest";
import LoginUserResponse from "./viewModels/LoginUserResponse";

// Define a service using a base URL and expected endpoints
export const userApi = createApi({
  reducerPath: "users",
  baseQuery: fetchBaseQuery({ baseUrl: BACKEND_URL }),
  endpoints: (builder) => ({
    loginUser: builder.mutation<LoginUserResponse, LoginUserRequest>({
      query: (credentials) => ({
        url: "/api/user/LoginUser",
        method: "POST",
        body: credentials,
      }),
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useLoginUserMutation } = userApi;
