import {
  Grid,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";

import AddCategoryForm from "./AddCategoryForm";
import { useGetAllCategoriesQuery } from "../../../services/categories/apiCategories";
import EditCategoryForm from "./EditCategoryForm";
import DeleteCategoryForm from "./DeleteCategoryForm";

const CategoryTable = () => {
  const { data, isFetching } = useGetAllCategoriesQuery();

  if (isFetching) return <Typography>Loading</Typography>;

  return (
    <>
      <Grid container alignItems="center" justifyContent="space-between">
        <Typography variant={"h3"}>Categories</Typography>
        <AddCategoryForm />
      </Grid>

      <Table size="small">
        <TableHead>
          <TableRow>
            <TableCell align="center">Category Name</TableCell>
            <TableCell align="center">Description</TableCell>
            <TableCell sx={{ width: "25%" }} align="center">
              Actions
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data &&
            data.map((category) => (
              <TableRow key={category.id}>
                <TableCell align="center">
                  <Grid>{category.name}</Grid>
                </TableCell>

                <TableCell align="center">{category.description}</TableCell>
                <TableCell sx={{ width: "25%" }} align="center">
                  <Grid container alignItems="center" justifyContent="center">
                    <Grid item>
                      <EditCategoryForm category={category} />
                    </Grid>
                    <Grid item>
                      <DeleteCategoryForm
                        id={category.id}
                        name={category.name}
                      />
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

export default CategoryTable;
