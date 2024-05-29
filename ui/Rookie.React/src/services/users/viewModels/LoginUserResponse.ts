import UserAddress from "../../../features/users/models/userAddress";

interface LoginUserResponse {
  id: string;
  email: string;
  userName: string;
  firstName: string;
  lastName: string;
  userAddress: Array<UserAddress>;
  token: string;
}

export default LoginUserResponse;
