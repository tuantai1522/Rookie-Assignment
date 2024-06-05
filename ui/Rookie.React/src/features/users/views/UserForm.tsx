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
import { toast } from "react-toastify";
import { useAddUserMutation } from "../../../services/users/apiUsers";
import PasswordTextField from "../../../ui/PasswordTextField";

const schema = yup.object().shape({
  firstName: yup.string().required("First name is required"),
  lastName: yup.string().required("Last name is required"),
  userName: yup.string().required("User name is required"),
  email: yup.string().required("Email is required"),
  password: yup.string().required("Password is required"),
  role: yup.string().required("Role is required"),
});

const UserForm = () => {
  const methods = useForm({
    mode: "onTouched",
    resolver: yupResolver(schema),
  });

  const [addUser, { isLoading: isAdding }] = useAddUserMutation();

  const submitForm = async (data: FieldValues) => {
    try {
      //prepare data
      const formData = new FormData();
      formData.append("FirstName", data.firstName);
      formData.append("LastName", data.lastName);
      formData.append("UserName", data.userName);
      formData.append("Email", data.email);
      formData.append("Password", data.password);
      formData.append("Role", data.role);

      await addUser(formData)
        .unwrap()
        .then(() => {
          toast.success("Adding new user successfully");
        })
        .catch((error) => {
          toast.error(error.data.error);
        });
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
          {/* First name */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">First Name</Typography>
            </Grid>
            <Grid item xs={4}>
              <NormalTextField label="First name" name="firstName" />
            </Grid>
          </Grid>
          {/* Last name */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">Last Name</Typography>
            </Grid>
            <Grid item xs={4}>
              <NormalTextField label="Last Name" name="lastName" />
            </Grid>
          </Grid>
          {/* User Name */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">User Name</Typography>
            </Grid>
            <Grid item xs={4}>
              <NormalTextField label="User Name" name="userName" />
            </Grid>
          </Grid>
          {/* Email */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">Email</Typography>
            </Grid>
            <Grid item xs={4}>
              <NormalTextField label="Email" name="email" />
            </Grid>
          </Grid>
          {/* Password */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">Password</Typography>
            </Grid>
            <Grid item xs={4}>
              <PasswordTextField label="Password" name="password" />
            </Grid>
          </Grid>

          {/* User role */}
          <Grid
            container
            justifyContent="center"
            alignItems="center"
            gap="1rem"
          >
            <Grid item xs={3}>
              <Typography variant="h5">User role</Typography>
            </Grid>
            <Grid item xs={4}>
              <TextField
                select
                label="User role"
                {...methods.register("role")}
                fullWidth
              >
                <MenuItem key={1} value={"Customer"}>
                  Customer
                </MenuItem>
                <MenuItem key={2} value={"Admin"}>
                  Admin
                </MenuItem>
              </TextField>
            </Grid>
          </Grid>

          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
            disabled={!methods.formState.isValid || isAdding}
          >
            Add
          </Button>
        </Box>
      </FormProvider>
    </>
  );
};

export default UserForm;
