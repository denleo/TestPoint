import React, { FC } from "react";

import { Navigate, useLocation } from "react-router-dom";

import { LayoutContainer } from "@/containers/layout";
import { useSelector } from "@/redux/hooks";
import { userAccountNameSelector, adminNameSelector } from "@/redux/selectors";

interface Props {
  children: JSX.Element;
}

export const ProtectedRoute: FC<Props> = ({ children }) => {
  const userName = useSelector(userAccountNameSelector);
  const adminUserName = useSelector(adminNameSelector);
  const isAuth = !!userName || !!adminUserName;
  const location = useLocation();

  if (!isAuth) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return <LayoutContainer>{children}</LayoutContainer>;
};
