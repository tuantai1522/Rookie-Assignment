import { Grid, Typography } from "@mui/material";

import ProductSearch from "./ProductSearch";
import ProductType from "./ProductType";
import { PAGE_NUMBER, PAGE_SIZE } from "../../../utils/config";
import ProductParams from "../models/productParams";
import { useState } from "react";
import { useGetAllProductsQuery } from "../../../services/products/apiProducts";
import AddProductForm from "./AddProductForm";
import ProductTableContent from "./ProductTableContent";
import { Order } from "./ProductTableHeader";

const initialState: ProductParams = {
  orderBy: "productNameasc",
  keyWord: "",
  categoryType: [],
  pageNumber: PAGE_NUMBER,
  pageSize: PAGE_SIZE,
};

const ProductView = () => {
  const [params, setParams] = useState<ProductParams>(initialState);
  const [order, setOrder] = useState<Order>("asc");
  const [orderBy, setOrderBy] = useState<string>("productName");
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  const { data, isFetching } = useGetAllProductsQuery(params);

  return (
    <>
      <Grid container alignItems="center" justifyContent="space-between">
        <Typography variant={"h3"}>Products</Typography>
        <AddProductForm />
      </Grid>

      <Grid
        container
        gap={2}
        sx={{ marginTop: "2rem" }}
        alignItems="center"
        justifyContent="center"
      >
        <Grid item xs={4}>
          <ProductSearch
            textValue={params.keyWord}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setParams({
                ...params,
                keyWord: event.target.value,
                pageNumber: 1,
                pageSize: rowsPerPage,
              });

              setPage(0);
            }}
          />
        </Grid>
        <Grid item xs={4}>
          <ProductType
            checked={params.categoryType}
            onChange={(checkedItems: string[]) => {
              setParams({
                ...params,
                categoryType: checkedItems,
                pageNumber: 1,
                pageSize: rowsPerPage,
              });

              setPage(0);
            }}
          />
        </Grid>
      </Grid>

      <ProductTableContent
        data={data}
        isFetching={isFetching}
        params={params}
        setParams={setParams}
        order={order}
        setOrder={setOrder}
        orderBy={orderBy}
        setOrderBy={setOrderBy}
        page={page}
        setPage={setPage}
        rowsPerPage={rowsPerPage}
        setRowsPerPage={setRowsPerPage}
      />
    </>
  );
};

export default ProductView;
