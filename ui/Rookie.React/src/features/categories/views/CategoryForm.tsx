import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";

import { Box, Button, Grid, Typography } from "@mui/material";
import { FieldValues, FormProvider, useForm } from "react-hook-form";
import NormalTextField from "../../../ui/NormalTextField";
import {
  useAddCategoryMutation,
  useUpdateCategoryMutation,
} from "../../../services/categories/apiCategories";

import { toast } from "react-toastify";

interface Props {
  category?: Category;
}
const schema = yup.object().shape({
  categoryName: yup.string().required("Category name is required"),
  description: yup.string().required("Description is required"),
});

const CategoryForm = ({ category }: Props) => {
  const isEditing = Boolean(category?.id);

  const methods = useForm({
    mode: "onTouched",
    resolver: yupResolver(schema),
  });

  const [addCategory, { isLoading: isAdding }] = useAddCategoryMutation();
  const [updateCategory, { isLoading: isUpdating }] =
    useUpdateCategoryMutation();

  const isProcessing = isAdding || isUpdating;

  const submitForm = async (data: FieldValues) => {
    try {
      if (isEditing && category) {
        //prepare data
        const formData = new FormData();
        formData.append("Id", category?.id.toUpperCase());
        formData.append("CategoryName", data.categoryName);
        formData.append("Description", data.description);

        await updateCategory(formData)
          .unwrap()
          .then(() => {
            toast.success("Editing category successfully");
          })
          .catch((error) => {
            toast.error(error.data.error);
          });
      } else {
        //prepare data
        const formData = new FormData();
        formData.append("CategoryName", data.categoryName);
        formData.append("Description", data.description);

        await addCategory(formData)
          .unwrap()
          .then(() => {
            toast.success("Adding new category successfully");
          })
          .catch((error) => {
            toast.error(error.data.error);
          });
      }
    } catch (err) {
      console.log(err);
    }
  };

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
          {/* Category name */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">Category Name</Typography>
            </Grid>
            <Grid item xs={4}>
              <NormalTextField
                label="category name"
                name="categoryName"
                defaultValue={category?.name}
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
                defaultValue={category?.description}
              />
            </Grid>
          </Grid>

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

export default CategoryForm;
