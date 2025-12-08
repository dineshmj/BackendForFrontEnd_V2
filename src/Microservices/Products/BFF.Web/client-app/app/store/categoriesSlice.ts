import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { Category, CategoriesResponse } from '../types';

export interface CategoriesState {
  data: Category[] | null;
  loading: boolean;
  error: string | null;
  hasLoaded: boolean;
}

const initialState: CategoriesState = {
  data: null,
  loading: false,
  error: null,
  hasLoaded: false,
};

// Async Thunk for API call
export const fetchCategories = createAsyncThunk<
  CategoriesResponse,
  void,
  { rejectValue: string }
>('categories/fetchCategories', async (_, { rejectWithValue }) => {
  try {
    const controller = new AbortController();
    const timeoutId = setTimeout(() => controller.abort(), 30000); // 30 second timeout

    const response = await fetch('/bff/categories', {
      credentials: 'include',
      headers: {
        'X-CSRF': '1',
      },
      signal: controller.signal,
    });
    
    clearTimeout(timeoutId);

    if (!response.ok) {
      return rejectWithValue(`Failed to load categories: Status ${response.status}`);
    }

    const data: CategoriesResponse = await response.json();
    return data;
  } catch (err) {
    if (err instanceof Error && err.name === 'AbortError') {
        return rejectWithValue("Categories fetch timed out");
    }
    console.error(err);
    return rejectWithValue("Network error fetching categories");
  }
});

const categoriesSlice = createSlice({
  name: 'categories',
  initialState,
  reducers: {
    clearCategoriesError: (state) => {
        state.error = null;
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchCategories.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCategories.fulfilled, (state, action: PayloadAction<CategoriesResponse>) => {
        state.loading = false;
        state.data = action.payload.categories;
        state.hasLoaded = true;
      })
      .addCase(fetchCategories.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload || "An unknown error occurred during fetch.";
        state.data = null;
      });
  },
});

export const { clearCategoriesError } = categoriesSlice.actions;
export default categoriesSlice.reducer;