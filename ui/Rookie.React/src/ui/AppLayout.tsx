import { Grid } from "@mui/material";
import { Outlet } from "react-router-dom";
import Header from "./Header";
import SideBar from "./SideBar";

const AppLayout = () => {
  return (
    <>
      <Grid container spacing={2}>
        <Grid item xs={3}>
          <SideBar />
        </Grid>
        <Grid item xs={9}>
          <Grid container>
            <Grid item xs={12}>
              <Header />
            </Grid>
            <Grid item xs={12} sx={{ marginTop: "2rem", padding: "1rem" }}>
              <Outlet />
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </>
  );
};

export default AppLayout;
