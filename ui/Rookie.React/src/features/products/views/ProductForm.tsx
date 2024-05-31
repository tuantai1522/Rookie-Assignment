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
import { ChangeEvent, useEffect, useState } from "react";
import {
  useAddProductMutation,
  useUpdateProductMutation,
} from "../../../services/products/apiProducts";
import { toast } from "react-toastify";
import Product from "../models/product";

interface Props {
  product?: Product;
}
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
});

const ProductForm = ({ product }: Props) => {
  const [file, setFile] = useState<File | null>(null);

  const isEditing = Boolean(product?.id);

  const methods = useForm({
    mode: "onTouched",
    resolver: yupResolver(schema),
  });

  const { data, isFetching } = useGetAllCategoriesQuery();
  const [addProduct, { isLoading: isAdding }] = useAddProductMutation();
  const [updateProduct, { isLoading: isUpdating }] = useUpdateProductMutation();

  const isProcessing = isAdding || isUpdating;

  const [defaultCategoryId, setDefaultCategoryId] = useState("");

  useEffect(() => {
    if (product?.categoryName && data) {
      const category = data.find((cat) => cat.name === product.categoryName);
      if (category) {
        setDefaultCategoryId(category.id);
        methods.setValue("categoryId", category.id);
      }
    }
  }, [product, data, methods.setValue]);

  const submitForm = async (data: FieldValues) => {
    try {
      if (isEditing && product) {
        //prepare data
        const formData = new FormData();
        formData.append("Id", product?.id.toUpperCase());
        formData.append("ProductName", data.productName);
        formData.append("Description", data.description);
        formData.append("QuantityInStock", data.quantityInStock);
        formData.append("Price", data.price);
        formData.append("CategoryId", data.categoryId.toUpperCase());

        await updateProduct(formData)
          .unwrap()
          .then(() => {
            toast.success("Editing product successfully");
          })
          .catch((error) => {
            toast.error(error.data.error);
          });
      } else {
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
      }
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
                defaultValue={product?.productName}
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
                defaultValue={product?.description}
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
              <NormalTextField
                label="Price"
                name="price"
                defaultValue={product?.price.toString()}
              />
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
                defaultValue={product?.quantityInStock.toString()}
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
                value={defaultCategoryId}
                onChange={(e) => {
                  setDefaultCategoryId(e.target.value); // Update the local state
                  methods.setValue("categoryId", e.target.value);
                }}
              >
                {data?.map((category) => (
                  <MenuItem key={category.id} value={category.id}>
                    {category.name}
                  </MenuItem>
                ))}
              </TextField>
            </Grid>
          </Grid>

          {/* Don't allow to update main image */}
          {/* Product image */}
          {!isEditing && (
            <>
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
                    fullWidth
                    onChange={handleImage}
                  />
                </Grid>
              </Grid>
            </>
          )}

          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
            disabled={!methods.formState.isValid || isProcessing}
          >
            {isEditing ? "Edit" : `Add`}
          </Button>
        </Box>
      </FormProvider>
    </>
  );
};

export default ProductForm;
