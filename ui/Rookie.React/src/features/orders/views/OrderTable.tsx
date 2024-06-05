import {
  Grid,
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

import { formatCurrency, formatDate } from "../../../utils/helper";

import { PAGE_NUMBER, PAGE_SIZE } from "../../../utils/config";
import { useState } from "react";
import OrderParams from "../models/orderParams";
import { useGetAllOrdersQuery } from "../../../services/orders/apiOrders";
import OrderSort from "./OrderSort";

const initialState: OrderParams = {
  orderBy: "priceAsc",
  pageNumber: PAGE_NUMBER,
  pageSize: PAGE_SIZE,
  minTotal: 1,
  maxTotal: 20000,
};

const OrderTable = () => {
  const [page, setPage] = useState(1);
  const [params, setParams] = useState<OrderParams>(initialState);
  const { data, isFetching } = useGetAllOrdersQuery(params);

  if (isFetching) return <Typography>Loading</Typography>;

  return (
    <>
      <Grid container alignItems="center" justifyContent="space-between">
        <Typography variant={"h3"}>Orders</Typography>
      </Grid>

      <Grid
        container
        gap={2}
        sx={{ marginTop: "2rem" }}
        alignItems="center"
        justifyContent="center"
      >
        <Grid item xs={3}>
          <OrderSort
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
        {/* <Grid item xs={4}>
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
        </Grid> */}
      </Grid>
      <Table size="small">
        <TableHead>
          <TableRow>
            <TableCell align="center">Order Date</TableCell>
            <TableCell align="center">User Name</TableCell>
            <TableCell align="center">Shipping Address</TableCell>
            <TableCell align="center">Total</TableCell>
            <TableCell align="center">Product Quantity</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data &&
            data.orders.map((order) => (
              <TableRow key={order.id}>
                <TableCell align="center">
                  <Grid item>{formatDate(order.orderDate.toString())}</Grid>
                </TableCell>
                <TableCell align="center">{order.userName}</TableCell>
                <TableCell align="center">
                  {order.shippingAddress.value +
                    ", " +
                    order.shippingAddress.city +
                    ", " +
                    order.shippingAddress.country +
                    ", " +
                    order.shippingAddress.zipCode}
                </TableCell>
                <TableCell align="center">
                  {formatCurrency(order.deliveryFee + order.subTotal)}
                </TableCell>
                <TableCell align="center">{order.orderItems.length}</TableCell>
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

export default OrderTable;
