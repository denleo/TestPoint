import React, { FC } from "react";

import { Box, styled } from "@mui/material";

import { START_PAGE_STEPS } from "./common";
import { Login } from "./Login";
import { SignUp } from "./SignUp";
import { useStartPageStore } from "./useStartPageStore";

const Layout = styled(Box)(({ theme }) => ({
  width: "100vw",
  minHeight: "100vh",
  display: "flex",
  flexDirection: "column",
  alignItems: "center",
  justifyContent: "center",
  backgroundColor: theme.palette.background.default,
}));

const StartPage: FC = () => {
  const pageStep = useStartPageStore((state) => state.pageStep);

  return (
    <Layout>
      {pageStep === START_PAGE_STEPS.LOGIN && <Login />}
      {pageStep === START_PAGE_STEPS.SIGN_UP && <SignUp />}
    </Layout>
  );
};

export default StartPage;
