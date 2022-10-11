import React, { FC } from "react";

import MailIcon from "@mui/icons-material/Mail";
import MenuIcon from "@mui/icons-material/Menu";
import InboxIcon from "@mui/icons-material/MoveToInbox";
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
} from "@mui/material";

import { useBreakpoint } from "@/api/hooks/useBreakPoint";
import { SVGFullLogo } from "@/common/icons";

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

export const SideBar: FC = () => {
  const mdUp = useBreakpoint("md");
  const isMinimized = useSidebarStore((store) => store.isMinimized);
  const toggleIsMinimized = useSidebarStore((store) => store.toggleIsMinimized);

  return (
    <Drawer variant="persistent" anchor="left" open={isMinimized}>
      <DrawerHeader>
        <SVGFullLogo />
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
        {["Inbox", "Starred", "Send email", "Drafts"].map((text, index) => (
          <ListItem key={text} disablePadding>
            <ListItemButton>
              <ListItemIcon>
                {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
              </ListItemIcon>
              <ListItemText primary={text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
      <Divider />
      <List>
        {["All mail", "Trash", "Spam"].map((text, index) => (
          <ListItem key={text} disablePadding>
            <ListItemButton>
              <ListItemIcon>
                {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
              </ListItemIcon>
              <ListItemText primary={text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
    </Drawer>
  );
};
