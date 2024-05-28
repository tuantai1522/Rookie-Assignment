import React from "react";
import { NavLink } from "react-router-dom";
import Typography from "@mui/material/Typography";

interface Props {
  to: string;
  children: React.ReactNode;
  customStyles?: React.CSSProperties;
  isHeader?: boolean;
}

const styleNavLink: React.CSSProperties = {
  textTransform: "uppercase",
  color: "inherit",
  textDecoration: "none",
};

const MyNavLink = ({ to, children, customStyles, isHeader = true }: Props) => {
  return (
    <NavLink to={to} style={{ ...styleNavLink, ...customStyles }}>
      <Typography
        variant={isHeader ? "h6" : "inherit"}
        sx={{
          "&:hover": {
            color: "grey.500",
          },
          "&:actice": { color: "text.secondary" },
        }}
      >
        {children}
      </Typography>
    </NavLink>
  );
};

export default MyNavLink;
