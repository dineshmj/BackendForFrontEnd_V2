// store.ts
import { configureStore } from '@reduxjs/toolkit';
import { combineReducers } from 'redux';
import {
  persistStore,
  persistReducer,
  FLUSH,
  REHYDRATE,
  PAUSE,
  PERSIST,
  PURGE,
  REGISTER,
  Persistor,
} from 'redux-persist';
import storage from 'redux-persist/lib/storage';

import productsReducer from './productsSlice';
import categoriesReducer from './categoriesSlice';

// Combine reducers
const rootReducer = combineReducers({
  products: productsReducer,
  categories: categoriesReducer,
});

const persistConfig = {
  key: 'root',
  storage,
  whitelist: ['products', 'categories'],
};

const persistedReducer = persistReducer(persistConfig, rootReducer);

// Create store
export const store = configureStore({
  reducer: persistedReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    }),
});

// Create persistor only in browser
export const persistor: Persistor | undefined =
  typeof window !== 'undefined' ? persistStore(store) : undefined;

// Types
export type RootState = ReturnType<typeof rootReducer>;
export type AppDispatch = typeof store.dispatch;