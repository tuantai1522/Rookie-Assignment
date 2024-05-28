import { AppBar, Grid, Switch, Toolbar, Typography } from "@mui/material";
import MyNavLink from "./MyNavLink";

const Header = () => {
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
                <Typography>User info</Typography>
              </Grid>
            </Grid>
          </Grid>
        </Toolbar>
      </AppBar>
    </>
  );
};

export default Header;
