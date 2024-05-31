import Image from "../../images/models/image";

interface Product {
  id: string;
  productName: string;
  description: string;
  price: number;
  quantityInStock: number;
  categoryName: string;

  mainImageUrl: string;
  imageUrls: Image[];
}

export default Product;
