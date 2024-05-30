import { Outlet, useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { useGetCurrentUserQuery } from "./services/users/apiUsers";

const AuthenticatedRoutes = () => {
  const { data, error, isLoading } = useGetCurrentUserQuery();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isLoading && (!data || error)) {
      navigate("/login");
    }
  }, [data, error, isLoading, navigate]);

  if (isLoading) {
    return <div>Loading...</div>; // Add a loading indicator if necessary
  }

  if (data) {
    return <Outlet />;
  }

  return null;
};

export default AuthenticatedRoutes;
