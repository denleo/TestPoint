import React, { FC, lazy, Suspense } from "react";

import { ThemeProvider as MUIThemeProvider } from "@mui/material";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";

import { theme } from "@common/theme/createTheme";
import { ProtectedRoute } from "@components/ProtectedRoute";

import { TestpointRoutes, TESTPOINT_ROUTE_DATA } from "./api/pageRoutes";

const StartPage = lazy(() => import("./containers/StartPage"));
const HomePage = lazy(() => import("./containers/Home"));
const TestComponentPage = lazy(() => import("./containers/TestComponentPage"));
const TestsPage = lazy(() => import("./containers/TestsPage"));

export const TESTPOINT_ROUTES: TestpointRoutes & {
  [key: string]: { component: JSX.Element };
} = {
  home: {
    ...TESTPOINT_ROUTE_DATA.home,
    component: <HomePage />,
  },
  tests: {
    ...TESTPOINT_ROUTE_DATA.tests,
    component: <TestsPage />,
  },
  testPage: {
    ...TESTPOINT_ROUTE_DATA.testPage,
    component: <TestComponentPage />,
  },
};

export const MainApp: FC = () => {
  return (
    <MUIThemeProvider theme={theme}>
      <BrowserRouter>
        <Suspense>
          <Routes>
            <Route path="/">
              <Route index element={<Navigate to="/home" />} />
              {Object.keys(TESTPOINT_ROUTES).map((key) => (
                <Route
                  key={key}
                  path={TESTPOINT_ROUTES[key].path}
                  element={
                    <ProtectedRoute>
                      {TESTPOINT_ROUTES[key].component}
                    </ProtectedRoute>
                  }
                />
              ))}
              <Route path="/login" element={<StartPage />} />
              <Route path="*" element={<Navigate to="/home" />} />
            </Route>
          </Routes>
        </Suspense>
      </BrowserRouter>
    </MUIThemeProvider>
  );
};
