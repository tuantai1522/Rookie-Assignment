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

import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import ProductSearch from "./ProductSearch";
import ProductSort from "./ProductSort";
import ProductType from "./ProductType";
import { PAGE_NUMBER, PAGE_SIZE } from "../../../utils/config";
import ProductParams from "../models/productParams";
import { useState } from "react";
import { useGetAllProductsQuery } from "../../../services/apiProducts";

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
  const { data, isFetching } = useGetAllProductsQuery(params);

  if (isFetching) return <Typography>Loading</Typography>;

  return (
    <>
      <Typography variant={"h3"}>Products</Typography>
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
            <TableCell align="center">ProductName</TableCell>
            <TableCell align="center">Price</TableCell>
            <TableCell align="center">Category</TableCell>
            <TableCell align="center">Quantity</TableCell>
            <TableCell align="center">Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data &&
            data.products.map((product) => (
              <TableRow key={product.id}>
                <TableCell>
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
                          height: 250,
                          width: 250,
                          maxHeight: { xs: 233, md: 167 },
                          maxWidth: { xs: 350, md: 250 },
                        }}
                        alt="The house from the offer."
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
                <TableCell align="right">
                  <Grid
                    container
                    justifyContent="flex-end"
                    alignItems="center"
                    gap={1}
                  >
                    <Grid item>
                      <IconButton color="primary" aria-label="Edit">
                        <EditIcon />
                      </IconButton>
                    </Grid>
                    <Grid item>
                      <IconButton color="secondary" aria-label="Delete">
                        <DeleteIcon />
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
            justifyContent="flex-end"
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
