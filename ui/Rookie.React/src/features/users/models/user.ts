import UserAddress from "./userAddress";

interface User {
  id: string;
  email: string;
  userName: string;
  firstName: string;
  lastName: string;
  userAddress: Array<UserAddress>;
  token: string;
}

export default User;
