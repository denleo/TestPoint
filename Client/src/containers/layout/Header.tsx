/* eslint-disable @typescript-eslint/no-shadow */
import React, { FC } from "react";

import { AccountCircle } from "@mui/icons-material";
import MenuIcon from "@mui/icons-material/Menu";
import { Breadcrumbs, IconButton, Link, Menu, MenuItem, styled, Toolbar, Typography, useTheme } from "@mui/material";
import MuiAppBar, { AppBarProps } from "@mui/material/AppBar";

import { useCurrentPath } from "@/api/hooks/useCurrentPath";

import { DRAWER_WIDTH, HEADER_HEIGHT } from "./common";
import { useSidebarStore } from "./useLayoutStore";

const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== "isMinimized",
})<AppBarProps & { isMinimized?: boolean }>(({ theme, isMinimized }) => ({
  height: HEADER_HEIGHT,
  transition: theme.transitions.create(["margin", "width"], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(isMinimized && {
    width: `calc(100% - ${DRAWER_WIDTH}px)`,
    marginLeft: `${DRAWER_WIDTH}px`,
    transition: theme.transitions.create(["margin", "width"], {
      easing: theme.transitions.easing.easeOut,
      duration: theme.transitions.duration.enteringScreen,
    }),
    [theme.breakpoints.down("md")]: {
      display: "none",
    },
  }),
}));

export const Header: FC = () => {
  const theme = useTheme();
  const currentPath = useCurrentPath();
  const isMinimized = useSidebarStore((store) => store.isMinimized);
  const toggleIsMinimized = useSidebarStore((store) => store.toggleIsMinimized);

  return (
    <AppBar
      isMinimized={isMinimized}
      position="static"
      sx={{
        backgroundColor: theme.palette.background.default,
        boxShadow: "none",
      }}
    >
      <Toolbar>
        <IconButton
          size="large"
          edge="start"
          color="secondary"
          aria-label="menu"
          sx={{ mr: 2 }}
          onClick={() => toggleIsMinimized()}
        >
          <MenuIcon />
        </IconButton>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          <Breadcrumbs aria-label="breadcrumb">
            <Link variant="body2" underline="hover" color="inherit" href="/">
              TestPoint
            </Link>
            <Typography variant="body2" color="text.primary">
              {currentPath ? currentPath.name : "Home"}
            </Typography>
          </Breadcrumbs>
        </Typography>
        <div>
          <IconButton
            size="large"
            aria-label="account of current user"
            aria-controls="menu-appbar"
            aria-haspopup="true"
            color="secondary"
          >
            <AccountCircle fontSize="large" />
          </IconButton>
        </div>
      </Toolbar>
    </AppBar>
  );
};
