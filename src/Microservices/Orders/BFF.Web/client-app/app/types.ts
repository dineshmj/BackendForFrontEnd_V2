export interface Claim {
  type: string;
  value: string;
}

export interface Order {
  orderId: number;
  dateOfOrder: string;
  totalAmount: number;
  paymentMethod: string;
  invoiceNumber: string;
  numberOfItems: number;
  customerName: string;
  dispatchStatus: string;
}

export interface OrdersResponse {
  message: string;
  orders: Order[];
}

export class HttpError extends Error {
  statusCode: number;
  constructor(message: string, statusCode: number) {
    super(message);
    this.name = 'HttpError';
    this.statusCode = statusCode;
  }
}