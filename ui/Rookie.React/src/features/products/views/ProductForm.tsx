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
import { useAddProductMutation } from "../../../services/products/apiProducts";
import { toast } from "react-toastify";

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
  categoryId: yup.string().required("Category name is required"),
  productImage: yup.mixed().required("File is required"),
});

const ProductForm = () => {
  const [file, setFile] = useState<File | null>(null);

  const { data, isFetching } = useGetAllCategoriesQuery();

  const [addProduct, { isLoading }] = useAddProductMutation();

  const methods = useForm({
    mode: "onTouched",
    resolver: yupResolver(schema),
  });

  const submitForm = async (data: FieldValues) => {
    try {
      if (!file) {
        toast.error("Please choose one image.");
        return;
      }

      //prepare data
      const formData = new FormData();
      formData.append("ProductName", data.productName);
      formData.append("Description", data.description);
      formData.append("QuantityInStock", data.quantityInStock);
      formData.append("FileImage", file);
      formData.append("Price", data.price);
      formData.append("CategoryId", data.categoryId.toUpperCase());

      await addProduct(formData)
        .unwrap()
        .then(() => {
          toast.success("Adding new product successfully");
        })
        .catch((error) => {
          toast.error(error.data.error);
        });
    } catch (err) {
      console.log(err);
    }
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
                {...methods.register("categoryId")}
                fullWidth
              >
                {data?.map((category) => (
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
            disabled={!methods.formState.isValid || isLoading}
          >
            Create
          </Button>
        </Box>
      </FormProvider>
    </>
  );
};

export default ProductForm;
