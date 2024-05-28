import { ListItemButton, ListItemIcon, ListItemText } from "@mui/material";
import AddHomeIcon from "@mui/icons-material/AddHome";
import CategoryIcon from "@mui/icons-material/Category";
import ManageAccountsIcon from "@mui/icons-material/ManageAccounts";
import ProductionQuantityLimitsIcon from "@mui/icons-material/ProductionQuantityLimits";
import Inventory2Icon from "@mui/icons-material/Inventory2";
import AutoGraphIcon from "@mui/icons-material/AutoGraph";
import MyNavLink from "./MyNavLink";

const MainListItem = () => {
  return (
    <>
      <ListItemButton>
        <ListItemIcon>
          <AddHomeIcon />
        </ListItemIcon>
        <ListItemText>
          <MyNavLink to="/" children="Home" />
        </ListItemText>
      </ListItemButton>
      <ListItemButton>
        <ListItemIcon>
          <CategoryIcon />
        </ListItemIcon>
        <ListItemText>
          <MyNavLink to="/category" children="Category" />
        </ListItemText>
      </ListItemButton>
      <ListItemButton>
        <ListItemIcon>
          <Inventory2Icon />
        </ListItemIcon>
        <ListItemText>
          <MyNavLink to="/product" children="Product" />
        </ListItemText>
      </ListItemButton>
      <ListItemButton>
        <ListItemIcon>
          <ManageAccountsIcon />
        </ListItemIcon>
        <ListItemText>
          <MyNavLink to="/user" children="User" />
        </ListItemText>
      </ListItemButton>
      <ListItemButton>
        <ListItemIcon>
          <ProductionQuantityLimitsIcon />
        </ListItemIcon>
        <ListItemText>
          <MyNavLink to="/order" children="Order" />
        </ListItemText>
      </ListItemButton>
      <ListItemButton>
        <ListItemIcon>
          <AutoGraphIcon />
        </ListItemIcon>
        <ListItemText>
          <MyNavLink to="/report" children="Report" />
        </ListItemText>
      </ListItemButton>
    </>
  );
};

export default MainListItem;
