import { AppBar, Grid, Switch, Toolbar } from "@mui/material";
import MyNavLink from "./MyNavLink";
import { useGetCurrentUserQuery } from "../services/users/apiUsers";
import SignInMenu from "./SignInMenu";

const Header = () => {
  const { data, isFetching } = useGetCurrentUserQuery();

  if (isFetching) return;
  return (
    <>
      <AppBar position="static">
        <Toolbar>
          <Grid container alignItems="center" justifyContent="space-between">
            <Grid item>
              <Grid container alignItems="center">
                <MyNavLink children="Shop management" to="/" />
                <Switch />
              </Grid>
            </Grid>

            <Grid item>
              <Grid container alignItems="center">
                {data ? (
                  <>
                    <SignInMenu user={data} />
                  </>
                ) : (
                  <>
                    <MyNavLink children="Login" to="/login" />
                  </>
                )}
              </Grid>
            </Grid>
          </Grid>
        </Toolbar>
      </AppBar>
    </>
  );
};

export default Header;
