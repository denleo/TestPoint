import React from "react";

import ReactDOM from "react-dom/client";
import { Provider as ReduxStoreProvider } from "react-redux";
import { persistStore } from "redux-persist";
import { PersistGate } from "redux-persist/integration/react";

import ErrorBoundary from "@components/ErrorBoundary";
import { store } from "@redux/store";

import { NotificationProvider } from "./components/NotificationProvider";
import { MainApp } from "./MainApp";

const root = ReactDOM.createRoot(document.getElementById("root") as HTMLElement);
const persistor = persistStore(store);
root.render(
  <React.StrictMode>
    <ErrorBoundary>
      <ReduxStoreProvider store={store}>
        <PersistGate persistor={persistor}>
          <MainApp />
          <NotificationProvider />
        </PersistGate>
      </ReduxStoreProvider>
    </ErrorBoundary>
  </React.StrictMode>
);
