import axios from "axios";
import { toast } from "react-toastify";
import { BACKEND_URL } from "../utils/config";

// Set config defaults when creating the instance
const instance = axios.create({
  baseURL: BACKEND_URL,
});

instance.defaults.withCredentials = true;

// Add a response interceptor
instance.interceptors.request.use((config) => {
  // const token = store.getState().account.user?.token;
  // if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

// Add a request interceptor
instance.interceptors.request.use(
  function (config) {
    // Do something before request is sent
    return config;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  }
);
const sleep = () => new Promise((resolve) => setTimeout(resolve, 500));
// Add a response interceptor
instance.interceptors.response.use(
  async function (response) {
    await sleep();

    return response.data;
  },
  function (error) {
    // Any status codes that falls outside the range of 2xx cause this function to trigger
    // Do something with response error
    console.log(">>>>>>>>>>Check error", error);
    const status = error.response?.status || 500;
    switch (status) {
      case 401: {
        toast.error("Unauthorized. Please login");

        return error.response.data;
      }
      case 403: {
        toast.error("You don't have permission to access this resource");
        return Promise.reject(error);
      }
    }
  }
);
export default instance;
