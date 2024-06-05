import { configureStore } from "@reduxjs/toolkit";
import { setupListeners } from "@reduxjs/toolkit/query";
import { productApi } from "../services/products/apiProducts";
import { categoryApi } from "../services/categories/apiCategories";
import { userApi } from "../services/users/apiUsers";
import { imageApi } from "../services/images/apiImages";
import { mainImageApi } from "../services/mainImages/apiMainImages";
import { orderApi } from "../services/orders/apiOrders";

export const store = configureStore({
  reducer: {
    //redux toolkit query
    [productApi.reducerPath]: productApi.reducer,
    [categoryApi.reducerPath]: categoryApi.reducer,
    [userApi.reducerPath]: userApi.reducer,
    [imageApi.reducerPath]: imageApi.reducer,
    [mainImageApi.reducerPath]: mainImageApi.reducer,
    [orderApi.reducerPath]: orderApi.reducer,
  },
  // Adding the api middleware enables caching, invalidation, polling,
  // and other useful features of `rtk-query`.
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(
      imageApi.middleware,
      productApi.middleware,
      categoryApi.middleware,
      userApi.middleware,
      mainImageApi.middleware,
      orderApi.middleware
    ),
});

// optional, but required for refetchOnFocus/refetchOnReconnect behaviors
// see `setupListeners` docs - takes an optional callback as the 2nd arg for customization
setupListeners(store.dispatch);

export type RootState = ReturnType<typeof store.getState>;
