interface OrderParams {
  pageNumber: number;
  pageSize: number;
  orderBy: string;
  minTotal: number;
  maxTotal: number;
}

export default OrderParams;
