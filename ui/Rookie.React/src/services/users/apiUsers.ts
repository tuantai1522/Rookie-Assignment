// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import LoginUserRequest from "./viewModels/LoginUserRequest";
import LoginUserResponse from "./viewModels/LoginUserResponse";
import User from "../../features/users/models/user";
import { BACKEND_URL } from "../../utils/config";
import { getToken } from "../../utils/helper";
import UserResponse from "./viewModels/listUserReponse";
import UserParams from "../../features/users/models/userParams";
import UserInfo from "../../features/users/models/userInfo";

// Define a service using a base URL and expected endpoints
export const userApi = createApi({
  reducerPath: "users",
  tagTypes: ["Users"],
  baseQuery: fetchBaseQuery({ baseUrl: BACKEND_URL }),
  endpoints: (builder) => ({
    getAllUsers: builder.query<UserResponse, UserParams | void>({
      query: (args: UserParams) => ({
        url: "/api/user/GetAllUsers",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
        method: "GET",
        params: {
          Role: args.roleType,
          PageNumber: args.pageNumber,
          PageSize: args.pageSize,
        },
      }),
      //build data after receiving data from server
      transformResponse: (response: UserInfo[], meta) => {
        const headers = meta?.response?.headers;

        const paginationHeader = headers?.get("pagination");

        return {
          users: response,
          pagination: paginationHeader ? JSON.parse(paginationHeader) : null,
        };
      },
      providesTags: (result) =>
        result
          ? [
              ...result.users.map(({ id }) => ({
                type: "Users" as const,
                id,
              })),
              { type: "Users", id: "LIST" },
            ]
          : [{ type: "Users", id: "LIST" }],
    }),
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
    addUser: builder.mutation<string, FormData>({
      query: (body) => ({
        url: "/api/user/RegisterUser",
        method: "POST",
        body,
      }),
      invalidatesTags: [{ type: "Users", id: "LIST" }],
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const {
  useGetAllUsersQuery,
  useLoginUserMutation,
  useGetCurrentUserQuery,
  useAddUserMutation,
} = userApi;
