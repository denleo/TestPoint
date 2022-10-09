/* eslint-disable @typescript-eslint/no-shadow */
import React, { FC, useCallback } from "react";

import { AccountCircle } from "@mui/icons-material";
import MenuIcon from "@mui/icons-material/Menu";
import {
  Breadcrumbs,
  IconButton,
  Link,
  Menu,
  MenuItem,
  styled,
  Toolbar,
  Typography,
  useTheme,
} from "@mui/material";
import MuiAppBar, { AppBarProps } from "@mui/material/AppBar";

import { DRAWER_WIDTH, HEADER_HEIGHT } from "./common";

interface Props {
  open: boolean;
  setOpen: (open: boolean) => void;
}

const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== "open",
})<AppBarProps & { open?: boolean }>(({ theme, open }) => ({
  height: HEADER_HEIGHT,
  transition: theme.transitions.create(["margin", "width"], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(open && {
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

export const Header: FC<Props> = ({ open, setOpen }) => {
  const theme = useTheme();
  const handleOpenSideBar = useCallback(() => {
    setOpen(!open);
  }, [open, setOpen]);

  return (
    <AppBar
      open={open}
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
          onClick={handleOpenSideBar}
        >
          <MenuIcon />
        </IconButton>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          <Breadcrumbs aria-label="breadcrumb">
            <Link variant="body2" underline="hover" color="inherit" href="/">
              TestPoint
            </Link>
            <Typography variant="body2" color="text.primary">
              Home
            </Typography>
          </Breadcrumbs>
        </Typography>
        <div>
          <IconButton
            size="large"
            aria-label="account of current user"
            aria-controls="menu-appbar"
            aria-haspopup="true"
            color="inherit"
          >
            <AccountCircle />
          </IconButton>
          <Menu
            open={false}
            id="menu-appbar"
            anchorOrigin={{
              vertical: "top",
              horizontal: "right",
            }}
            keepMounted
            transformOrigin={{
              vertical: "top",
              horizontal: "right",
            }}
          >
            <MenuItem>Profile</MenuItem>
            <MenuItem>My account</MenuItem>
          </Menu>
        </div>
      </Toolbar>
    </AppBar>
  );
};
