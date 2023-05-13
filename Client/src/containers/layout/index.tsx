/* eslint-disable @typescript-eslint/no-shadow */
import React, { FC, ReactNode, useEffect } from "react";

import { Box, ContainerProps, Divider, styled } from "@mui/material";

import { useBreakpoint } from "@/api/hooks/useBreakPoint";
import { theme } from "@/common/theme/createTheme";

import backImage from "../../shared/backgroundImage.svg";

import { DRAWER_WIDTH, HEADER_HEIGHT } from "./common";
import { Header } from "./Header";
import { SideBar } from "./SideBar";
import { useSidebarStore } from "./useLayoutStore";

interface Props extends ContainerProps {
  children?: ReactNode;
  footer?: NonNullable<ReactNode>;
}

const Background = styled(Box)(({ theme }) => ({
  minHeight: "100vh",
  "&::before": {
    content: "''",
    position: "absolute",
    top: 0,
    left: 0,
    width: "100%",
    height: "100%",
    background: "linear-gradient(180deg, #C1C1C1 0%, #B8F2FF 100%)",
    zIndex: -2,
  },
  "&::after": {
    content: "''",
    position: "absolute",
    top: 0,
    left: 0,
    width: "100%",
    height: "100%",
    backgroundImage: `url(${backImage})`,
    backgroundRepeat: "no-repeat",
    backgroundPosition: "bottom center",
    backgroundSize: "830px 75%",
    zIndex: -1,
  },
}));

const MainContainer = styled("main", {
  shouldForwardProp: (prop) => prop !== "isMinimized",
})<{
  isMinimized?: boolean;
}>(({ theme, isMinimized }) => ({
  flexGrow: 1,
  height: `calc(100vh - ${HEADER_HEIGHT + 1}px)`, // 1px Divider XD
  overflow: "auto",
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
      <Background>
        <SideBar />
        <Header />
        <Divider />
        {children && (
          <MainContainer isMinimized={isMinimized} {...props} sx={{ zIndex: 2 }}>
            {children}
          </MainContainer>
        )}
        {footer}
      </Background>
      <div id="footer" />
    </>
  );
};
