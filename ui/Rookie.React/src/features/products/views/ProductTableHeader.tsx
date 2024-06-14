import {
  Box,
  TableCell,
  TableHead,
  TableRow,
  TableSortLabel,
} from "@mui/material";

import { visuallyHidden } from "@mui/utils";
import Product from "../models/product";
import ProductParams from "../models/productParams";

export type Order = "asc" | "desc";

interface HeadCell {
  id: keyof Product;
  label: string;
  numeric: boolean;
}

const headCells: readonly HeadCell[] = [
  {
    id: "productName",
    numeric: false,
    label: "Product name",
  },
  {
    id: "description",
    numeric: true,
    label: "Description",
  },
  {
    id: "price",
    numeric: true,
    label: "Price",
  },
  {
    id: "quantityInStock",
    numeric: true,
    label: "Quantity",
  },
  {
    id: "categoryName",
    numeric: true,
    label: "Category name",
  },
];

interface Props {
  onRequestSort: (
    event: React.MouseEvent<unknown>,
    property: keyof Product
  ) => void;
  order: Order;
  orderBy: string;

  params: ProductParams;
  setParams: React.Dispatch<React.SetStateAction<ProductParams>>;
}

const ProductTableHeader = (props: Props) => {
  const { order, orderBy, onRequestSort, params, setParams } = props;
  const createSortHandler =
    (property: keyof Product) => (event: React.MouseEvent<unknown>) => {
      const query = orderBy + order;

      setParams({
        ...params,
        orderBy: query,
        pageNumber: 1,
      });
      onRequestSort(event, property);
    };

  return (
    <TableHead>
      <TableRow>
        {headCells.map((headCell) => (
          <TableCell
            key={headCell.id}
            align={headCell.numeric ? "right" : "left"}
            sortDirection={orderBy === headCell.id ? order : false}
          >
            <TableSortLabel
              active={orderBy === headCell.id}
              direction={orderBy === headCell.id ? order : "asc"}
              onClick={createSortHandler(headCell.id)}
            >
              {headCell.label}
              {orderBy === headCell.id ? (
                <Box component="span" sx={visuallyHidden}>
                  {order === "desc" ? "sorted descending" : "sorted ascending"}
                </Box>
              ) : null}
            </TableSortLabel>
          </TableCell>
        ))}
      </TableRow>
    </TableHead>
  );
};

export default ProductTableHeader;
