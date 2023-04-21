import React from "react";

import ReactDOM from "react-dom/client";
import { Provider as ReduxStoreProvider } from "react-redux";

import ErrorBoundary from "@components/ErrorBoundary";
import { store } from "@redux/store";

import { MainApp } from "./MainApp";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <ErrorBoundary>
      <ReduxStoreProvider store={store}>
        <MainApp />
      </ReduxStoreProvider>
    </ErrorBoundary>
  </React.StrictMode>
);
