import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import AppLayout from "./ui/AppLayout";
import CategoryPage from "./pages/CategoryPage";
import Home from "./features/home/views/Home";
import ProductPage from "./pages/ProductPage";
import OrderPage from "./pages/OrderPage";
import ReportPage from "./pages/ReportPage";
import UserPage from "./pages/UserPage";

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route element={<AppLayout />}>
            <Route index element={<Navigate replace to="home" />} />
            <Route path="home" element={<Home />} />

            <Route path="category" element={<CategoryPage />} />
            <Route path="product" element={<ProductPage />} />
            <Route path="order" element={<OrderPage />} />
            <Route path="report" element={<ReportPage />} />
            <Route path="user" element={<UserPage />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
