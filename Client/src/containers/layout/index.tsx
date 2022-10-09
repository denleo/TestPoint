/* eslint-disable @typescript-eslint/no-shadow */
import React, { FC, ReactNode, useState } from "react";

import { Box, ContainerProps, Divider, styled } from "@mui/material";

import { theme } from "@/common/theme/createTheme";

import { DRAWER_WIDTH, HEADER_HEIGHT } from "./common";
import { Header } from "./Header";
import { SideBar } from "./SideBar";

interface Props extends ContainerProps {
  children?: ReactNode;
  footer?: NonNullable<ReactNode>;
}

const MainContainer = styled("main", {
  shouldForwardProp: (prop) => prop !== "open",
})<{
  open?: boolean;
}>(({ theme, open }) => ({
  flexGrow: 1,
  height: `calc(100vh - ${HEADER_HEIGHT + 1}px)`, // 1px Divider XD
  padding: theme.spacing(3),
  transition: theme.transitions.create("margin", {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(open && {
    transition: theme.transitions.create("margin", {
      easing: theme.transitions.easing.easeOut,
      duration: theme.transitions.duration.enteringScreen,
    }),
    marginLeft: `${DRAWER_WIDTH}px`,
    [theme.breakpoints.down("md")]: {
      display: "none",
    },
  }),
}));

export const LayoutContainer: FC<Props> = ({ children, footer, ...props }) => {
  const [open, setOpen] = useState(false);
  return (
    <>
      <Box
        sx={{
          backgroundColor: theme.palette.background.default,
          minHeight: "100vh",
        }}
      >
        <SideBar open={open} setOpen={setOpen} />
        <Header open={open} setOpen={setOpen} />
        <Divider />
        {children && (
          <MainContainer open={open} {...props}>
            {children}
          </MainContainer>
        )}
        {footer}
      </Box>
      <div id="footer" />
    </>
  );
};
