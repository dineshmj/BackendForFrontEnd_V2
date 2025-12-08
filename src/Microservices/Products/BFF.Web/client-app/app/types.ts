export interface Claim {
  type: string;
  value: string;
}

// Product Types based on app/v1/catalog/view-all/page.tsx
export interface Product {
  id: number;
  name: string;
  price: number;
  categoryName: string;
  stockQuantity: number;
}

export interface ProductsResponse {
  message: string;
  products: Product[];
}

// Category Types based on app/v1/categories/view-all/page.tsx
export interface Category {
  id: number;
  name: string;
}

export interface CategoriesResponse {
  message: string;
  categories: Category[];
}