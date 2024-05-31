import { ThemeProvider } from "@mui/material/styles";
import createTheme from "@mui/material/styles/createTheme";

import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import AppLayout from "./ui/AppLayout";
import CategoryPage from "./pages/CategoryPage";
import Home from "./features/home/views/Home";
import ProductPage from "./pages/ProductPage";
import OrderPage from "./pages/OrderPage";
import ReportPage from "./pages/ReportPage";
import UserPage from "./pages/UserPage";
import LoginPage from "./pages/LoginPage";

import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import AuthenticatedRoutes from "./AuthenticatedRoutes";
import ProductDetailsPage from "./pages/ProductDetailsPage";

const defaultTheme = createTheme();

function App() {
  return (
    <>
      <ThemeProvider theme={defaultTheme}>
        <BrowserRouter>
          <Routes>
            <Route element={<AppLayout />}>
              {/* Authenticated route */}
              <Route element={<AuthenticatedRoutes />}>
                <Route path="category" element={<CategoryPage />} />

                <Route path="product" element={<ProductPage />} />
                <Route path="product/:id" element={<ProductDetailsPage />} />

                <Route path="order" element={<OrderPage />} />
                <Route path="report" element={<ReportPage />} />
                <Route path="user" element={<UserPage />} />
              </Route>

              {/* Not authenticated route */}
              <Route index element={<Navigate replace to="home" />} />
              <Route path="home" element={<Home />} />
            </Route>
            <Route path="login" element={<LoginPage />} />
          </Routes>
        </BrowserRouter>
        <ToastContainer
          position="top-right"
          autoClose={5000}
          hideProgressBar={false}
          newestOnTop={false}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
          theme="light"
        />
      </ThemeProvider>
    </>
  );
}

export default App;
