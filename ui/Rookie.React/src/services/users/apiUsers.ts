// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BACKEND_URL } from "../../utils/config";
import LoginUserRequest from "./viewModels/LoginUserRequest";
import LoginUserResponse from "./viewModels/LoginUserResponse";
import User from "../../features/users/models/user";
import { getToken } from "../../utils/helper";

const baseQuery = fetchBaseQuery({
  baseUrl: BACKEND_URL,
  prepareHeaders: (headers) => {
    const token = getToken();
    if (token) {
      headers.set("Authorization", `Bearer ${token}`);
    }
    return headers;
  },
});

// Define a service using a base URL and expected endpoints
export const userApi = createApi({
  reducerPath: "users",
  baseQuery,
  endpoints: (builder) => ({
    loginUser: builder.mutation<LoginUserResponse, LoginUserRequest>({
      query: (credentials) => ({
        url: "/api/user/LoginUser",
        method: "POST",
        body: credentials,
      }),
    }),
    getCurrentUser: builder.query<User, void>({
      query: () => "/api/user/GetCurrentUser",
      transformResponse: (response: User): User => {
        return {
          id: response.id,
          userName: response.userName,
          firstName: response.firstName,
          lastName: response.lastName,
          email: response.email,
          token: response.token,
          userAddress: response.userAddress,
        };
      },
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useLoginUserMutation, useGetCurrentUserQuery } = userApi;
