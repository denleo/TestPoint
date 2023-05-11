export enum TESTPOINT_ROUTE {
  HOME = "Home",
  TESTS = "Tests",
  TESTPAGE = "Test",
  STATISTICS = "Statistics",
  PROFILE = "Profile",
  TEST_BUILDER = "Test Builder",
  USERS = "Users",
  HISTORY = "History",
  RESULTS = "Results",
}

export interface TestpointRoutes {
  [key: string]: {
    name: TESTPOINT_ROUTE;
    path: string;
    component?: JSX.Element;
    showAdmin: boolean | "both";
  };
}

export const TESTPOINT_ROUTES: TestpointRoutes = {
  home: {
    name: TESTPOINT_ROUTE.HOME,
    path: "/home",
    showAdmin: false,
  },
  tests: {
    name: TESTPOINT_ROUTE.TESTS,
    path: "/tests",
    showAdmin: "both",
  },
  testPage: {
    name: TESTPOINT_ROUTE.TESTPAGE,
    path: "/test",
    showAdmin: false,
  },
  statistics: {
    name: TESTPOINT_ROUTE.STATISTICS,
    path: "/statistics",
    showAdmin: "both",
  },
  profile: {
    name: TESTPOINT_ROUTE.PROFILE,
    path: "/profile",
    showAdmin: false,
  },
  testBuilder: {
    name: TESTPOINT_ROUTE.TEST_BUILDER,
    path: "/constructor",
    showAdmin: true,
  },
  users: {
    name: TESTPOINT_ROUTE.USERS,
    path: "/users",
    showAdmin: true,
  },
  history: {
    name: TESTPOINT_ROUTE.HISTORY,
    path: "/history",
    showAdmin: false,
  },
  results: {
    name: TESTPOINT_ROUTE.RESULTS,
    path: "/results",
    showAdmin: "both",
  },
};
