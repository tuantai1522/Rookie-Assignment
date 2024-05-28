import { Divider, List } from "@mui/material";

import styled from "styled-components";
import MainListItem from "./MainListItem";

const StyledSideBar = styled.div`
  background-color: #ce7045;
  height: 100vh;
`;

const SideBar = () => {
  return (
    <>
      <StyledSideBar>
        <List component="nav">
          <MainListItem />
        </List>
        <Divider />
      </StyledSideBar>
    </>
  );
};

export default SideBar;
