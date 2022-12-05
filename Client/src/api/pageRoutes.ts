export enum TESTPOINT_ROUTE {
  HOME = "Home",
  TESTS = "Tests",
  TESTPAGE = "Test",
}

export interface TestpointRoutes {
  [key: string]: {
    name: TESTPOINT_ROUTE;
    path: string;
  };
}

export const TESTPOINT_ROUTE_DATA: TestpointRoutes = {
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
};
