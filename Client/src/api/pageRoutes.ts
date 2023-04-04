export enum TESTPOINT_ROUTE {
  HOME = "Home",
  TESTS = "Tests",
  TESTPAGE = "Test",
  STATISTICS = "Statistics",
  PROFILE = "Profile",
}

export interface TestpointRoutes {
  [key: string]: {
    name: TESTPOINT_ROUTE;
    path: string;
    component?: JSX.Element;
  };
}

export const TESTPOINT_ROUTES: TestpointRoutes = {
  home: {
    name: TESTPOINT_ROUTE.HOME,
    path: "/home",
  },
  tests: {
    name: TESTPOINT_ROUTE.TESTS,
    path: "/tests",
  },
  testPage: {
    name: TESTPOINT_ROUTE.TESTPAGE,
    path: "/test",
  },
  statistics: {
    name: TESTPOINT_ROUTE.STATISTICS,
    path: "/statistics",
  },
  profile: {
    name: TESTPOINT_ROUTE.PROFILE,
    path: "/profile",
  },
};
