import {
  Box,
  Grid,
  IconButton,
  Pagination,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableFooter,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";

import { formatCurrency } from "../../../utils/helper";

import InfoIcon from "@mui/icons-material/Info";

import ProductSearch from "./ProductSearch";
import ProductSort from "./ProductSort";
import ProductType from "./ProductType";
import { PAGE_NUMBER, PAGE_SIZE } from "../../../utils/config";
import ProductParams from "../models/productParams";
import { useEffect, useState } from "react";
import { useGetAllProductsQuery } from "../../../services/products/apiProducts";
import EditProductForm from "./EditProductForm";
import AddProductForm from "./AddProductForm";
import DeleteProductForm from "./DeleteProductForm";
import { useNavigate } from "react-router-dom";

const initialState: ProductParams = {
  orderBy: "name",
  keyWord: "",
  categoryType: [],
  pageNumber: PAGE_NUMBER,
  pageSize: PAGE_SIZE,
};

const ProductTable = () => {
  const [page, setPage] = useState(1);
  const [params, setParams] = useState<ProductParams>(initialState);
  const { data, isFetching, refetch } = useGetAllProductsQuery(params);

  const navigate = useNavigate();

  useEffect(function () {
    refetch();
  }, []);

  if (isFetching) return <Typography>Loading</Typography>;

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
              });
              setPage(1);
            }}
          />
        </Grid>
        <Grid item xs={3}>
          <ProductSort
            item={params.orderBy}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setParams({
                ...params,
                orderBy: event.target.value,
                pageNumber: 1,
              });
              setPage(1);
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
              });
              setPage(1);
            }}
          />
        </Grid>
      </Grid>
      <Table size="small">
        <TableHead>
          <TableRow>
            <TableCell sx={{ width: "60%" }} align="center">
              ProductName
            </TableCell>
            <TableCell align="center">Price</TableCell>
            <TableCell align="center">Category</TableCell>
            <TableCell align="center">Quantity</TableCell>
            <TableCell sx={{ width: "25%" }} align="center">
              Actions
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data &&
            data.products.map((product) => (
              <TableRow key={product.id}>
                <TableCell sx={{ width: "60%" }} align="center">
                  <Grid
                    container
                    justifyContent="flex-start"
                    alignItems="center"
                    gap={2}
                  >
                    <Grid item>
                      <Box
                        component="img"
                        sx={{
                          height: 80,
                          width: 100,
                          maxHeight: { xs: 80, md: 80 },
                          maxWidth: { xs: 100, md: 100 },
                        }}
                        alt={product.productName}
                        src={product.mainImageUrl}
                      />
                    </Grid>
                    <Grid item>{product.productName}</Grid>
                  </Grid>
                </TableCell>
                <TableCell align="center">
                  {formatCurrency(product.price)}
                </TableCell>
                <TableCell align="center">{product.categoryName}</TableCell>
                <TableCell align="center">{product.quantityInStock}</TableCell>
                <TableCell sx={{ width: "25%" }} align="center">
                  <Grid container alignItems="center" justifyContent="center">
                    <Grid item>
                      <EditProductForm product={product} />
                    </Grid>
                    <Grid item>
                      <DeleteProductForm
                        id={product.id}
                        name={product.productName}
                      />
                    </Grid>
                    <Grid item>
                      <IconButton
                        onClick={() => navigate(`/product/${product.id}`)}
                        color="success"
                        aria-label="Edit"
                      >
                        <InfoIcon />
                      </IconButton>
                    </Grid>
                  </Grid>
                </TableCell>
              </TableRow>
            ))}
        </TableBody>
        <TableFooter>
          <Grid
            container
            alignItems="center"
            justifyContent="flex-start"
            sx={{ marginTop: "2rem" }}
          >
            <Grid item>
              <Stack spacing={2}>
                <Pagination
                  onChange={(_, page) => {
                    setParams({ ...params, pageNumber: page });
                    setPage(page);
                  }}
                  count={data?.pagination.TotalPage || 0}
                  color="secondary"
                  page={page}
                />
              </Stack>
            </Grid>
          </Grid>
        </TableFooter>
      </Table>
    </>
  );
};

export default ProductTable;
