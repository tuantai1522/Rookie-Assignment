import { getCookie, setCookie, removeCookie } from "typescript-cookie";

const formatCurrency = (value: number) =>
  new Intl.NumberFormat("en", { style: "currency", currency: "USD" }).format(
    value
  );

// Set JWT token in cookie and expires 7 days from now
const setToken = (token: string) =>
  setCookie("Jwt", token, { expires: 7, path: "" });

// Get JWT token from cookie
const getToken = () => getCookie("Jwt");

// Remove JWT token from cookie
const removeToken = () => removeCookie("Jwt");

export { formatCurrency, getToken, setToken, removeToken };
