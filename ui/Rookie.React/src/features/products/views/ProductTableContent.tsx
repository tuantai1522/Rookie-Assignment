import Box from "@mui/material/Box";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import ProductTableHeader, { Order } from "./ProductTableHeader";
import Product from "../models/product";
import ProductParams from "../models/productParams";
import ProductResponse from "../../../services/products/viewModels/listProductResponse";
import { Backdrop, CircularProgress } from "@mui/material";
import { useEffect, useState } from "react";

interface Props {
  data: ProductResponse | undefined;
  isFetching: boolean;

  params: ProductParams;
  setParams: React.Dispatch<React.SetStateAction<ProductParams>>;

  order: Order;
  setOrder: React.Dispatch<React.SetStateAction<Order>>;

  orderBy: string;
  setOrderBy: React.Dispatch<React.SetStateAction<string>>;

  page: number;
  setPage: React.Dispatch<React.SetStateAction<number>>;

  rowsPerPage: number;
  setRowsPerPage: React.Dispatch<React.SetStateAction<number>>;
}

const ProductTableContent = ({
  data,
  isFetching,
  params,
  setParams,
  order,
  setOrder,
  orderBy,
  setOrderBy,
  page,
  setPage,
  rowsPerPage,
  setRowsPerPage,
}: Props) => {
  const [dense, setDense] = useState(false);

  useEffect(() => {
    setParams({
      ...params,
      orderBy: orderBy + order,
      pageNumber: 1,
      pageSize: rowsPerPage,
    });

    setPage(0);
  }, [order, orderBy]);

  const handleRequestSort = (
    event: React.MouseEvent<unknown>,
    property: keyof Product
  ) => {
    setOrder(order === "desc" ? "asc" : "desc");
    setOrderBy(property);
  };

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);

    setParams({
      ...params,
      pageNumber: newPage + 1,
      orderBy: orderBy + order,
      pageSize: rowsPerPage,
    });
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value));
    setPage(0);

    setParams({
      ...params,
      pageSize: parseInt(event.target.value),
      pageNumber: 1,
      orderBy: orderBy + order,
    });
  };

  return (
    <Box sx={{ width: "100%", position: "relative" }}>
      <Paper sx={{ width: "100%", mb: 2, position: "relative" }}>
        {isFetching && (
          <Backdrop
            sx={{
              color: "#fff",
              zIndex: (theme) => theme.zIndex.drawer + 1,
              position: "absolute",
              top: 0,
              left: 0,
              right: 0,
              bottom: 0,
            }}
            open={isFetching}
          >
            <CircularProgress color="secondary" />
          </Backdrop>
        )}
        <TableContainer>
          <Table
            sx={{ minWidth: 750 }}
            aria-labelledby="tableTitle"
            size={dense ? "small" : "medium"}
          >
            <ProductTableHeader
              order={order}
              orderBy={orderBy}
              onRequestSort={handleRequestSort}
              params={params}
              setParams={setParams}
            />
            <TableBody>
              {data?.products.map((row, index) => {
                return (
                  <TableRow
                    hover
                    role="checkbox"
                    tabIndex={-1}
                    key={row.id}
                    sx={{ cursor: "pointer" }}
                  >
                    <TableCell component="th" scope="row" padding="none">
                      {row.productName}
                    </TableCell>
                    <TableCell align="right">{row.description}</TableCell>
                    <TableCell align="right">{row.price}</TableCell>
                    <TableCell align="right">{row.quantityInStock}</TableCell>
                    <TableCell align="right">{row.categoryName}</TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
        </TableContainer>
        <TablePagination
          rowsPerPageOptions={[5, 10, 25]}
          component="div"
          count={data?.pagination?.TotalCount ?? 0}
          rowsPerPage={rowsPerPage}
          page={page}
          onPageChange={handleChangePage}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      </Paper>
    </Box>
  );
};

export default ProductTableContent;
