interface CreateProductRequest {
  productName: string;
  description: string;
  quantityInStock: number;
  fileImage: File;
  price: number;
  categoryId: string;
}

export default CreateProductRequest;
