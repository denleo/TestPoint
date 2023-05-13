/* eslint-disable @typescript-eslint/no-shadow */
import React, { FC } from "react";

import MenuIcon from "@mui/icons-material/Menu";
import { Breadcrumbs, IconButton, Link, styled, Toolbar, Typography, useTheme } from "@mui/material";
import MuiAppBar, { AppBarProps } from "@mui/material/AppBar";
import { useNavigate } from "react-router-dom";

import { useCurrentPath } from "@/api/hooks/useCurrentPath";
import { useSelector } from "@/redux/hooks";
import { isAdminSelector, userAvatarSelector } from "@/redux/selectors";
import emptyAccountImage from "@/shared/emptyAvatar.png";

import { TESTPOINT_ROUTES } from "@api/pageRoutes";

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
  const isAdmin = useSelector(isAdminSelector);
  const base64Avatar = useSelector(userAvatarSelector);
  const navigate = useNavigate();
  const currentPath = useCurrentPath();
  const isMinimized = useSidebarStore((store) => store.isMinimized);
  const toggleIsMinimized = useSidebarStore((store) => store.toggleIsMinimized);

  const avatar = base64Avatar ? `data:image/png;base64,${base64Avatar}` : emptyAccountImage;

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
        {!isAdmin && (
          <div>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              color="secondary"
              sx={{
                "& img": {
                  width: 40,
                  height: 40,
                  borderRadius: "50%",
                },
              }}
              onClick={() => navigate(TESTPOINT_ROUTES.profile.path)}
            >
              <img src={avatar} alt="avatar" />
            </IconButton>
          </div>
        )}
      </Toolbar>
    </AppBar>
  );
};
