import Pagination from "../../../features/common/models/pagination";
import UserInfo from "../../../features/users/models/userInfo";

interface UserResponse {
  users: UserInfo[];
  pagination: Pagination;
}

export default UserResponse;
