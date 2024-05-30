import { Box } from "@mui/material";

import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";

import CssBaseline from "@mui/material/CssBaseline";
import { Container } from "@mui/material";

import { useLocation, useNavigate } from "react-router-dom";

import { FieldValues, FormProvider, useForm } from "react-hook-form";

import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useLoginUserMutation } from "../../../services/users/apiUsers";
import PasswordTextField from "../../../ui/PasswordTextField";
import NormalTextField from "../../../ui/NormalTextField";
import { toast } from "react-toastify";
import { removeToken, setToken } from "../../../utils/helper";
import { useEffect } from "react";

// TODO remove, this demo shouldn't need to reset the theme.

const schema = yup.object().shape({
  userName: yup.string().required("User name is required"),
  password: yup.string().required("Password is required"),
});

function LoginForm() {
  const methods = useForm({
    mode: "onTouched",
    resolver: yupResolver(schema),
  });

  //if user moves back to login page after logging in, remove token
  useEffect(() => {
    removeToken();
  }, []);

  const [loginUser, { isLoading }] = useLoginUserMutation();

  const navigate = useNavigate();
  const location = useLocation();

  const submitForm = async (data: FieldValues) => {
    try {
      await loginUser({
        userName: data.userName,
        passWord: data.password,
      })
        .unwrap()
        .then((payload) => {
          toast.success("Login successfully");
          setToken(payload.token);

          navigate(location.state?.from || "/home");
        })
        .catch((error) => {
          toast.error(error.data.error);
        });
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Sign in
        </Typography>
        <FormProvider {...methods}>
          <Box
            component="form"
            onSubmit={methods.handleSubmit(submitForm)}
            noValidate
            sx={{ mt: 1 }}
          >
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <NormalTextField
                  label="User name"
                  name="userName"
                  defaultValue="user1"
                />
              </Grid>
              <Grid item xs={12}>
                <PasswordTextField
                  label="Password"
                  name="password"
                  defaultValue="P@ssw0rd"
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
              Sign In
            </Button>
          </Box>
        </FormProvider>
      </Box>
    </Container>
  );
}

export default LoginForm;
