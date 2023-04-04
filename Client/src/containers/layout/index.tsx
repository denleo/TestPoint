/* eslint-disable @typescript-eslint/no-shadow */
import React, { FC, ReactNode, useEffect } from "react";

import { Box, ContainerProps, Divider, styled } from "@mui/material";

import { useBreakpoint } from "@/api/hooks/useBreakPoint";
import { theme } from "@/common/theme/createTheme";

import { DRAWER_WIDTH, HEADER_HEIGHT } from "./common";
import { Header } from "./Header";
import { SideBar } from "./SideBar";
import { useSidebarStore } from "./useLayoutStore";

interface Props extends ContainerProps {
  children?: ReactNode;
  footer?: NonNullable<ReactNode>;
}

const MainContainer = styled("main", {
  shouldForwardProp: (prop) => prop !== "isMinimized",
})<{
  isMinimized?: boolean;
}>(({ theme, isMinimized }) => ({
  flexGrow: 1,
  height: `calc(100vh - ${HEADER_HEIGHT + 1}px)`, // 1px Divider XD
  padding: theme.spacing(3),
  transition: theme.transitions.create("margin", {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(isMinimized && {
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
  const lgUp = useBreakpoint("lg");
  const isMinimized = useSidebarStore((store) => store.isMinimized);
  const toggleIsMinimized = useSidebarStore((store) => store.toggleIsMinimized);

  useEffect(() => {
    if (lgUp) {
      toggleIsMinimized(true);
    }
  }, []);

  return (
    <>
      <Box
        sx={{
          backgroundColor: theme.palette.background.default,
          minHeight: "100vh",
        }}
      >
        <SideBar />
        <Header />
        <Divider />
        {children && (
          <MainContainer isMinimized={isMinimized} {...props}>
            {children}
          </MainContainer>
        )}
        {footer}
      </Box>
      <div id="footer" />
    </>
  );
};
