import {
  Box,
  Button,
  Grid,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../../store/store";
import { useEffect, useState } from "react";
import { fetchProducts } from "../slices/ProductSlice";
import ProductParams from "../models/productParams";
import { PAGE_NUMBER, PAGE_SIZE } from "../../../utils/config";
import { formatCurrency } from "../../../utils/helper";

import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";

const initialState: ProductParams = {
  orderBy: "name",
  keyWord: "",
  categoryType: [],
  pageNumber: PAGE_NUMBER,
  pageSize: PAGE_SIZE,
};

const ProductTable = () => {
  const [params] = useState<ProductParams>(initialState);

  const { products, loading } = useAppSelector((state) => state.product);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(fetchProducts(params));
  }, [dispatch]);

  if (loading) return <Typography>Loading</Typography>;

  console.log(products);

  return (
    <>
      <Typography variant={"h3"}>Products</Typography>
      <Table size="small">
        <TableHead>
          <TableRow>
            <TableCell align="center">ProductName</TableCell>
            <TableCell>Price</TableCell>
            <TableCell>Category</TableCell>
            <TableCell>Quantity</TableCell>
            <TableCell align="center">Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {products &&
            products.map((product) => (
              <TableRow key={product.id}>
                <TableCell>
                  <Grid
                    container
                    justifyContent="flex-start"
                    alignItems="center"
                    gap={2}
                  >
                    <Grid item>
                      {" "}
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
                <TableCell>{formatCurrency(product.price)}</TableCell>
                <TableCell>{product.categoryName}</TableCell>
                <TableCell>{product.quantityInStock}</TableCell>
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
      </Table>
    </>
  );
};

export default ProductTable;
