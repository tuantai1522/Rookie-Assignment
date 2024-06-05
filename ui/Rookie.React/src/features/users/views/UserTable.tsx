import {
  Grid,
  Pagination,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableFooter,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";

import { PAGE_NUMBER, PAGE_SIZE } from "../../../utils/config";
import { useState } from "react";
import UserType from "./UserType";
import UserParams from "../models/userParams";
import { useGetAllUsersQuery } from "../../../services/users/apiUsers";
import AddUserForm from "./AddUserForm";

const initialState: UserParams = {
  roleType: [],
  pageNumber: PAGE_NUMBER,
  pageSize: PAGE_SIZE,
};

const UserTable = () => {
  const [page, setPage] = useState(1);
  const [params, setParams] = useState<UserParams>(initialState);
  const { data, isFetching } = useGetAllUsersQuery(params);

  if (isFetching) return <Typography>Loading</Typography>;

  return (
    <>
      <Grid container alignItems="center" justifyContent="space-between">
        <Typography variant={"h3"}>Users</Typography>
        <AddUserForm />
      </Grid>

      <Grid
        container
        gap={2}
        sx={{ marginTop: "2rem" }}
        alignItems="center"
        justifyContent="center"
      >
        <Grid item xs={4}>
          <UserType
            checked={params.roleType}
            onChange={(checkedItems: string[]) => {
              setParams({
                ...params,
                roleType: checkedItems,
                pageNumber: 1,
              });
              setPage(1);
            }}
          />
        </Grid>
      </Grid>
      <Table size="small">
        <TableHead>
          <TableRow>
            <TableCell align="center">First Name</TableCell>
            <TableCell align="center">Last Name</TableCell>
            <TableCell align="center">User Name</TableCell>
            <TableCell align="center">Email</TableCell>
            <TableCell align="center">Order Quantity</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data &&
            data.users.map((user) => (
              <TableRow key={user.id}>
                <TableCell align="center">{user.firstName}</TableCell>
                <TableCell align="center">{user.lastName}</TableCell>
                <TableCell align="center">{user.userName}</TableCell>
                <TableCell align="center">{user.email}</TableCell>
                <TableCell align="center">{user.orders.length}</TableCell>
              </TableRow>
            ))}
        </TableBody>
        <TableFooter>
          <Grid
            container
            alignItems="center"
            justifyContent="flex-start"
            sx={{ marginTop: "2rem" }}
          >
            <Grid item>
              <Stack spacing={2}>
                <Pagination
                  onChange={(_, page) => {
                    setParams({ ...params, pageNumber: page });
                    setPage(page);
                  }}
                  count={data?.pagination.TotalPage || 0}
                  color="secondary"
                  page={page}
                />
              </Stack>
            </Grid>
          </Grid>
        </TableFooter>
      </Table>
    </>
  );
};

export default UserTable;
