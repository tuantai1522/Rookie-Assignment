interface Product {
  id: string;
  productName: string;
  description: string;
  price: number;
  quantityInStock: number;
  categoryName: string;

  mainImageUrl: string;
  imageUrls: Array<string>;
}

export default Product;
