import React, { FC, lazy, Suspense, useEffect } from "react";

import { ThemeProvider as MUIThemeProvider } from "@mui/material";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";

import { theme } from "@common/theme/createTheme";
import { ProtectedRoute } from "@components/ProtectedRoute";
import LoadingPage from "@containers/LoadingPage";

import { useStoreStates } from "./api/hooks/useStoreStates";
import { TestpointRoutes, TESTPOINT_ROUTES } from "./api/pageRoutes";
import { TEST_DATA_1 } from "./containers/TestsPage/data";
import { useDispatch, useSelector } from "./redux/hooks";
import { isAdminSelector } from "./redux/selectors";
import { AccountActions } from "./redux/userAccount/actions";
import { clearUserData } from "./redux/userAccount/reducer";

const StartPage = lazy(() => import("./containers/StartPage"));
const HomePage = lazy(() => import("./containers/Home"));
const TestComponentPage = lazy(() => import("./containers/TestComponentPage"));
const TestsPage = lazy(() => import("./containers/TestsPage"));
const StatisticsPage = lazy(() => import("./containers/StatisticsPage"));
const ProfilePage = lazy(() => import("./containers/ProfilePage"));
const TestBuilderPage = lazy(() => import("./containers/TestBuilderPage"));
const UsersPage = lazy(() => import("./containers/UsersPage"));

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
  testBuilder: {
    ...TESTPOINT_ROUTES.testBuilder,
    component: <TestBuilderPage />,
  },
  users: {
    ...TESTPOINT_ROUTES.users,
    component: <UsersPage />,
  },
};

export const MainApp: FC = () => {
  const isAdmin = useSelector(isAdminSelector);
  const dispatch = useDispatch();
  useStoreStates();

  useEffect(() => {
    async function fetchData() {
      if (isAdmin) {
        await dispatch(AccountActions.getAdminData());
      } else {
        await dispatch(AccountActions.getUserData());
      }
    }

    try {
      fetchData();
    } catch {
      dispatch(clearUserData);
    }
  }, [isAdmin, dispatch]);

  return (
    <MUIThemeProvider theme={theme}>
      <BrowserRouter>
        <Suspense fallback={<LoadingPage />}>
          <Routes>
            <Route path="/">
              <Route index element={<Navigate to={!isAdmin ? "/home" : "/tests"} />} />
              {Object.keys(routes)
                .filter((key) => routes[key].showAdmin !== !isAdmin)
                .map((key) => (
                  <Route
                    key={key}
                    path={routes[key].path}
                    element={<ProtectedRoute>{routes[key].component ?? <>no elements</>}</ProtectedRoute>}
                  />
                ))}
              <Route path="/login" element={<StartPage />} />
              <Route path="*" element={<Navigate to={!isAdmin ? "/home" : "/tests"} />} />
            </Route>
          </Routes>
        </Suspense>
      </BrowserRouter>
    </MUIThemeProvider>
  );
};
