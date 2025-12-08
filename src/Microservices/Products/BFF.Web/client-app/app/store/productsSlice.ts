import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { Product, ProductsResponse } from '../types';

export interface ProductsState {
  data: Product[] | null;
  loading: boolean;
  error: string | null;
  hasLoaded: boolean;
}

const initialState: ProductsState = {
  data: null,
  loading: false,
  error: null,
  hasLoaded: false,
};

// Async Thunk for API call
export const fetchProducts = createAsyncThunk<
  ProductsResponse,
  void,
  { rejectValue: string }
>('products/fetchProducts', async (_, { rejectWithValue }) => {
  try {
    const controller = new AbortController();
    const timeoutId = setTimeout(() => controller.abort(), 30000); // 30 second timeout

    const response = await fetch('/bff/products', {
      credentials: 'include',
      headers: {
        'X-CSRF': '1',
      },
      signal: controller.signal,
    });
    
    clearTimeout(timeoutId);

    if (!response.ok) {
      return rejectWithValue(`Failed to load products: Status ${response.status}`);
    }

    const data: ProductsResponse = await response.json();
    return data;
  } catch (err) {
    if (err instanceof Error && err.name === 'AbortError') {
        return rejectWithValue("Products fetch timed out");
    }
    console.error(err);
    return rejectWithValue("Network error fetching products");
  }
});

const productsSlice = createSlice({
  name: 'products',
  initialState,
  reducers: {
    // Reducer for explicit 'Fetch Records Again' if needed, though async thunk handles update flow well.
    // We will rely on dispatching fetchProducts for reloads.
    clearProductsError: (state) => {
        state.error = null;
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchProducts.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchProducts.fulfilled, (state, action: PayloadAction<ProductsResponse>) => {
        state.loading = false;
        state.data = action.payload.products;
        state.hasLoaded = true;
      })
      .addCase(fetchProducts.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload || "An unknown error occurred during fetch.";
        state.data = null; // Clear data on failure? Let's keep existing data if it exists and hasLoaded is true, but the requirement implies fetching updates state entirely. Clearing seems safer for reloads unless specified otherwise.
      });
  },
});

export const { clearProductsError } = productsSlice.actions;
export default productsSlice.reducer;