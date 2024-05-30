import { Outlet, useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { useGetCurrentUserQuery } from "./services/users/apiUsers";

const AuthenticatedRoutes = () => {
  const { data } = useGetCurrentUserQuery();

  const navigate = useNavigate();

  useEffect(
    function () {
      if (!data?.token) navigate("/login");
    },
    [data, navigate]
  );

  if (data?.token) return <Outlet />;
};

export default AuthenticatedRoutes;
