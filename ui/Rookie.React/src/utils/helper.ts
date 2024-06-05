import { getCookie, setCookie, removeCookie } from "typescript-cookie";

const formatCurrency = (value: number) =>
  new Intl.NumberFormat("en", { style: "currency", currency: "USD" }).format(
    value
  );

const formatDate = (isoDate: string): string => {
  const date = new Date(isoDate);
  const day = String(date.getDate()).padStart(2, "0");
  const month = String(date.getMonth() + 1).padStart(2, "0"); // Months are 0-based
  const year = date.getFullYear();

  return `${day}/${month}/${year}`;
};

// Set JWT token in cookie and expires 7 days from now
const setToken = (token: string) =>
  setCookie("Jwt", token, { expires: 7, path: "" });

// Get JWT token from cookie
const getToken = () => getCookie("Jwt");

// Remove JWT token from cookie
const removeToken = () => removeCookie("Jwt");

export { formatCurrency, formatDate, getToken, setToken, removeToken };
