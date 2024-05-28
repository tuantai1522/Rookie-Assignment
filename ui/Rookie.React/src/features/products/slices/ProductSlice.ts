import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import Product from "../models/product";
import { fetchAllProducts } from "../../../services/apiProducts";
import ProductParams from "../models/productParams";
import { AxiosResponse } from "axios";

interface ProductState {
  products: Array<Product> | null;
  loading: boolean;
  status: string | null;
}
const initialState: ProductState = {
  products: null,
  loading: true,
  status: null,
};

export const fetchProducts = createAsyncThunk<Product[], ProductParams>(
  "account/fetchProducts",
  async (productParams: ProductParams, thunkAPI) => {
    try {
      const response: Product[] = await fetchAllProducts(productParams);
      return response; // Extract the data property
    } catch (error: any) {
      return thunkAPI.rejectWithValue({
        error: error.response?.data || error.message,
      });
    }
  }
);

export const productSlice = createSlice({
  name: "products",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchProducts.pending, (state) => {
      state.status = "pendingFetchProducts";
    });

    builder.addCase(fetchProducts.fulfilled, (state, action) => {
      state.status = "fulfilled";
      state.products = action.payload;
      state.loading = false;
    });

    builder.addCase(fetchProducts.rejected, (state) => {
      state.status = "fullfilled";
    });
  },
});
