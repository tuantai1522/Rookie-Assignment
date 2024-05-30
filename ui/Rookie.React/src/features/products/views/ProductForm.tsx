import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";

import {
  Box,
  Button,
  Grid,
  MenuItem,
  TextField,
  Typography,
} from "@mui/material";
import { FieldValues, FormProvider, useForm } from "react-hook-form";
import NormalTextField from "../../../ui/NormalTextField";
import { useGetAllCategoriesQuery } from "../../../services/categories/apiCategories";
import { ChangeEvent, useState } from "react";
import CreateProductRequest from "../../../services/products/viewModels/createProductRequest";

const schema = yup.object().shape({
  productName: yup.string().required("Product name is required"),
  description: yup.string().required("Description is required"),
  price: yup
    .number()
    .typeError("Price must be a number")
    .moreThan(0, "Price must be greater than 0")
    .required("Price is required"),
  quantityInStock: yup
    .number()
    .typeError("Quantity must be a number")
    .moreThan(-1, "Quantity must be greater than -1")
    .required("Quantity is required"),
  categoryName: yup.string().required("Category name is required"),
  productImage: yup.string().required("Product image is required"),
});

const ProductForm = () => {
  const [file, setFile] = useState<File | null>(null);

  const { data, isFetching } = useGetAllCategoriesQuery();

  const methods = useForm({
    mode: "onTouched",
    resolver: yupResolver(schema),
  });

  const submitForm = async (data: FieldValues) => {
    if (!file) {
      console.error("File is not selected.");
      return;
    }

    const createProductRequest: CreateProductRequest = {
      productName: data.productName,
      description: data.description,
      quantityInStock: parseInt(data.quantityInStock),
      fileImage: file,
      price: parseFloat(data.price),
      categoryId: data.categoryId,
    };

    console.log(createProductRequest);
  };

  const handleImage = (e: ChangeEvent<HTMLInputElement>) => {
    const selectedFile = e.target.files?.[0]; // Get the selected file
    setFile(selectedFile || null); // Update the file state
  };

  if (isFetching) return;

  return (
    <>
      <FormProvider {...methods}>
        <Box
          component="form"
          onSubmit={methods.handleSubmit(submitForm)}
          noValidate
          sx={{
            display: "flex",
            gap: "2rem",
            alignItems: "center",
            flexDirection: "column",
          }}
        >
          {/* Product name */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">Product Name</Typography>
            </Grid>
            <Grid item xs={4}>
              <NormalTextField
                label="Product name"
                name="productName"
                defaultValue=""
              />
            </Grid>
          </Grid>
          {/* Description */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">Description</Typography>
            </Grid>
            <Grid item xs={4}>
              <NormalTextField
                label="Description"
                name="description"
                defaultValue=""
              />
            </Grid>
          </Grid>
          {/* Price */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">Price</Typography>
            </Grid>
            <Grid item xs={4}>
              <NormalTextField label="Price" name="price" defaultValue="" />
            </Grid>
          </Grid>
          {/* Quantity */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">Quantity</Typography>
            </Grid>
            <Grid item xs={4}>
              <NormalTextField
                label="Quantity"
                name="quantityInStock"
                defaultValue=""
              />
            </Grid>
          </Grid>
          {/* Category name */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">Category name</Typography>
            </Grid>
            <Grid item xs={4}>
              <TextField
                select
                label="Category name"
                {...methods.register("categoryName")}
                fullWidth
              >
                {data?.categories.map((category) => (
                  <MenuItem key={category.id} value={category.id}>
                    {category.name}
                  </MenuItem>
                ))}
              </TextField>
            </Grid>
          </Grid>

          {/* Product image */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">Product image</Typography>
            </Grid>
            <Grid item xs={4}>
              <TextField
                type="file"
                label="Product image"
                {...methods.register("productImage")}
                fullWidth
                onChange={handleImage}
              />
            </Grid>
          </Grid>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
            disabled={!methods.formState.isValid}
          >
            Create
          </Button>
        </Box>
      </FormProvider>
    </>
  );
};

export default ProductForm;
