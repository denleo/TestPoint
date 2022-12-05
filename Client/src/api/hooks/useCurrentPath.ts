import { useMemo } from "react";

import { useLocation } from "react-router-dom";

import { TESTPOINT_ROUTE_DATA } from "../pageRoutes";

export const useCurrentPath = () => {
  const location = useLocation();

  const currentRoute = useMemo(
    () => Object.keys(TESTPOINT_ROUTE_DATA).find((key) => TESTPOINT_ROUTE_DATA[key].path === location.pathname),
    [location.pathname]
  );

  return currentRoute && TESTPOINT_ROUTE_DATA[currentRoute];
};
