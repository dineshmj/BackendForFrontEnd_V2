'use client';

import React from 'react';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';
import { store, persistor } from '../store/store';

interface ReduxProviderWrapperProps {
  children: React.ReactNode;
}

export default function ReduxProviderWrapper({ children }: ReduxProviderWrapperProps) {
  // If persistor is not available (server / static export), render Provider without PersistGate.
  if (!persistor) {
    return <Provider store={store}>{children}</Provider>;
  }

  // Otherwise render PersistGate (client)
  return (
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        {children}
      </PersistGate>
    </Provider>
  );
}