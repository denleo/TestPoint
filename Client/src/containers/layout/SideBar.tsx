import React, { FC } from "react";

import AccountBoxIcon from "@mui/icons-material/AccountBox";
import AnalyticsIcon from "@mui/icons-material/Analytics";
import HomeIcon from "@mui/icons-material/Home";
import LogoutIcon from "@mui/icons-material/Logout";
import MenuIcon from "@mui/icons-material/Menu";
import QuizIcon from "@mui/icons-material/Quiz";
import SensorDoorIcon from "@mui/icons-material/SensorDoor";
import {
  Divider,
  Drawer as MUIDrawer,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  styled,
  useTheme,
} from "@mui/material";
import { useNavigate } from "react-router-dom";

import { useBreakpoint } from "@/api/hooks/useBreakPoint";
import { TESTPOINT_ROUTES } from "@/api/pageRoutes";
import { IconFullLogo } from "@/common/icons";

import { DRAWER_WIDTH, HEADER_HEIGHT } from "./common";
import { useSidebarStore } from "./useLayoutStore";

const DrawerHeader = styled("div")(({ theme }) => ({
  display: "flex",
  height: `${HEADER_HEIGHT}px`,
  alignItems: "center",
  justifyContent: "space-between",
  padding: theme.spacing(0, 0, 0, 2),
  ...theme.mixins.toolbar,
}));

const Drawer = styled(MUIDrawer)(({ theme }) => ({
  width: DRAWER_WIDTH,
  flexShrink: 0,
  "& .MuiDrawer-paper": {
    width: DRAWER_WIDTH,
    backgroundColor: "white",
    boxSizing: "border-box",
    [theme.breakpoints.down("md")]: {
      width: "100vw",
    },
  },
  [theme.breakpoints.down("md")]: {
    width: "100vw",
  },
}));

const ExitButton = styled(ListItemButton)(({ theme }) => ({
  "&:hover, &:focus, &:active": {
    backgroundColor: theme.palette.error.light,
  },
}));

const ListButton = styled(ListItemButton)(({ theme }) => ({
  "&:hover, &:focus, &:active": {
    backgroundColor: theme.palette.secondary.light,
  },
}));

const routes = [
  {
    ...TESTPOINT_ROUTES.home,
    icon: <HomeIcon />,
  },
  {
    ...TESTPOINT_ROUTES.tests,
    icon: <QuizIcon />,
  },
  {
    ...TESTPOINT_ROUTES.profile,
    icon: <AccountBoxIcon />,
  },
  {
    ...TESTPOINT_ROUTES.statistics,
    icon: <AnalyticsIcon />,
  },
];

export const SideBar: FC = () => {
  const mdUp = useBreakpoint("md");
  const navigate = useNavigate();
  const theme = useTheme();
  const isMinimized = useSidebarStore((store) => store.isMinimized);
  const toggleIsMinimized = useSidebarStore((store) => store.toggleIsMinimized);

  return (
    <Drawer variant="persistent" anchor="left" open={isMinimized}>
      <DrawerHeader>
        <IconFullLogo width={180} height={35} />
        {!mdUp && (
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
        )}
      </DrawerHeader>
      <Divider />
      <List>
        {routes.map(({ icon, name, path }) => (
          <ListItem key={name} color={theme.palette.secondary.main} disablePadding onClick={() => navigate(path)}>
            <ListButton color={theme.palette.secondary.main}>
              <ListItemIcon>{icon}</ListItemIcon>
              <ListItemText primary={name} />
            </ListButton>
          </ListItem>
        ))}
      </List>
      <Divider />
      <List>
        <ListItem disablePadding>
          <ExitButton>
            <ListItemIcon>
              <SensorDoorIcon />
            </ListItemIcon>
            <ListItemText primary="Exit" />
          </ExitButton>
        </ListItem>
      </List>
    </Drawer>
  );
};
