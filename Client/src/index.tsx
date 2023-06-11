import React from "react";

import { GoogleOAuthProvider } from "@react-oauth/google";
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
const GOOGLE_CLIENT_ID = "98975823283-oaoo1dvhk00t49aaj37ilig5uc1doivs.apps.googleusercontent.com";

root.render(
  <React.StrictMode>
    <ErrorBoundary>
      <GoogleOAuthProvider clientId={GOOGLE_CLIENT_ID}>
        <ReduxStoreProvider store={store}>
          <PersistGate persistor={persistor}>
            <MainApp />
            <NotificationProvider />
          </PersistGate>
        </ReduxStoreProvider>
      </GoogleOAuthProvider>
    </ErrorBoundary>
  </React.StrictMode>
);
