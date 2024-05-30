import Button from "@mui/material/Button";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import { MouseEvent, useState } from "react";
import { Typography } from "@mui/material";
import { NavLink, useNavigate } from "react-router-dom";
import { removeToken } from "../utils/helper";
import User from "../features/users/models/user";
import PersonIcon from "@mui/icons-material/Person";

interface Props {
  user: User;
}

const style: React.CSSProperties = {
  textTransform: "uppercase",
  color: "inherit",
  textDecoration: "none",
};

export default function SignInMenu({ user }: Props) {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const navigate = useNavigate();

  const open = Boolean(anchorEl);
  const handleClick = (event: MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleLogout = () => {
    removeToken();
    navigate("/login");
  };

  return (
    <>
      <Button style={style} onClick={handleClick}>
        <PersonIcon />
        {user.userName}
      </Button>
      <Menu anchorEl={anchorEl} open={open} onClose={handleClose}>
        <MenuItem onClick={handleClose}>
          <NavLink
            style={{ textDecoration: "none", color: " inherit" }}
            to="/profile"
          >
            <Typography variant="body2">Profile</Typography>
          </NavLink>
        </MenuItem>
        <MenuItem onClick={handleLogout}>
          <Typography variant="body2">Log out</Typography>
        </MenuItem>
      </Menu>
    </>
  );
}
