import React, { FC, lazy, Suspense } from "react";

import { ThemeProvider as MUIThemeProvider } from "@mui/material";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";

import { theme } from "@common/theme/createTheme";
import { ProtectedRoute } from "@components/ProtectedRoute";
import LoadingPage from "@containers/LoadingPage";

import { TestpointRoutes, TESTPOINT_ROUTES } from "./api/pageRoutes";

const StartPage = lazy(() => import("./containers/StartPage"));
const HomePage = lazy(() => import("./containers/Home"));
const TestComponentPage = lazy(() => import("./containers/TestComponentPage"));
const TestsPage = lazy(() => import("./containers/TestsPage"));
const StatisticsPage = lazy(() => import("./containers/StatisticsPage"));
const ProfilePage = lazy(() => import("./containers/ProfilePage"));

export const routes: TestpointRoutes = {
  home: {
    ...TESTPOINT_ROUTES.home,
    component: <HomePage />,
  },
  tests: {
    ...TESTPOINT_ROUTES.tests,
    component: <TestsPage />,
  },
  testPage: {
    ...TESTPOINT_ROUTES.testPage,
    component: <TestComponentPage />,
  },
  statistics: {
    ...TESTPOINT_ROUTES.statistics,
    component: <StatisticsPage />,
  },
  profile: {
    ...TESTPOINT_ROUTES.profile,
    component: <ProfilePage />,
  },
};

export const MainApp: FC = () => {
  return (
    <MUIThemeProvider theme={theme}>
      <BrowserRouter>
        <Suspense fallback={<LoadingPage />}>
          <Routes>
            <Route path="/">
              <Route index element={<Navigate to="/home" />} />
              {Object.keys(routes).map((key) => (
                <Route
                  key={key}
                  path={routes[key].path}
                  element={<ProtectedRoute>{routes[key].component ?? <>no elements</>}</ProtectedRoute>}
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
