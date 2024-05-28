import { Divider, List, ListItem, ListItemIcon } from "@mui/material";
import MyNavLink from "./MyNavLink";
import AddHomeIcon from "@mui/icons-material/AddHome";
import CategoryIcon from "@mui/icons-material/Category";
import ManageAccountsIcon from "@mui/icons-material/ManageAccounts";
import ProductionQuantityLimitsIcon from "@mui/icons-material/ProductionQuantityLimits";
import Inventory2Icon from "@mui/icons-material/Inventory2";
import AutoGraphIcon from "@mui/icons-material/AutoGraph";

const SideBar = () => {
  return (
    <>
      <div>
        <List component="nav">
          <ListItem>
            <ListItemIcon>
              <AddHomeIcon />
            </ListItemIcon>
            <MyNavLink to="/" children="Home" />
          </ListItem>
          <ListItem>
            <ListItemIcon>
              <CategoryIcon />
            </ListItemIcon>
            <MyNavLink to="/category" children="Category" />
          </ListItem>
          <ListItem>
            <ListItemIcon>
              <Inventory2Icon />
            </ListItemIcon>
            <MyNavLink to="/product" children="Product" />
          </ListItem>
          <ListItem>
            <ListItemIcon>
              <ManageAccountsIcon />
            </ListItemIcon>
            <MyNavLink to="/user" children="User" />
          </ListItem>
          <ListItem>
            <ListItemIcon>
              <ProductionQuantityLimitsIcon />
            </ListItemIcon>
            <MyNavLink to="/Order" children="Order" />
          </ListItem>
          <ListItem>
            <ListItemIcon>
              <AutoGraphIcon />
            </ListItemIcon>
            <MyNavLink to="/report" children="Report" />
          </ListItem>
        </List>
        <Divider />
      </div>
    </>
  );
};

export default SideBar;
