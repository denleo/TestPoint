import React, { FC } from "react";

import AnalyticsIcon from "@mui/icons-material/Analytics";
import HomeIcon from "@mui/icons-material/Home";
import LogoutIcon from "@mui/icons-material/Logout";
import MailIcon from "@mui/icons-material/Mail";
import MenuIcon from "@mui/icons-material/Menu";
import InboxIcon from "@mui/icons-material/MoveToInbox";
import QuizIcon from "@mui/icons-material/Quiz";
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
            <ListItemButton color={theme.palette.secondary.main}>
              <ListItemIcon>{icon}</ListItemIcon>
              <ListItemText primary={name} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
      <Divider />
      <List>
        <ListItem disablePadding>
          <ListItemButton>
            <ListItemIcon>
              <LogoutIcon />
            </ListItemIcon>
            <ListItemText primary="Exit" />
          </ListItemButton>
        </ListItem>
      </List>
    </Drawer>
  );
};
