import React, { FC, lazy, Suspense } from "react";

import { ThemeProvider } from "@mui/material";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import { theme } from "@common/theme/createTheme";

import ErrorBoundary from "./components/ErrorBoundary";

const HomePage = lazy(() => import("./containers/Home"));

const MainApp: FC = () => {
  return (
    <ThemeProvider theme={theme}>
      <BrowserRouter>
        <Suspense>
          <Routes>
            <Route index element={<HomePage />} />
            <Route path=":login" element={<HomePage />} />
          </Routes>
        </Suspense>
      </BrowserRouter>
    </ThemeProvider>
  );
};

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <ErrorBoundary>
      <MainApp />
    </ErrorBoundary>
  </React.StrictMode>
);
