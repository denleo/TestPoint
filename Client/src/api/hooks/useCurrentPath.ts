import { useMemo } from "react";

import { useLocation } from "react-router-dom";

import { TESTPOINT_ROUTES } from "../pageRoutes";

export const useCurrentPath = () => {
  const location = useLocation();

  const currentRoute = useMemo(
    () => Object.keys(TESTPOINT_ROUTES).find((key) => TESTPOINT_ROUTES[key].path === location.pathname),
    [location.pathname]
  );

  return currentRoute && TESTPOINT_ROUTES[currentRoute];
};
