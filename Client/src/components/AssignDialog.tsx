import React, { FC, useState, useCallback, SyntheticEvent, Fragment } from "react";

import CloseIcon from "@mui/icons-material/Close";
import GroupIcon from "@mui/icons-material/Group";
import PersonSearchIcon from "@mui/icons-material/PersonSearch";
import {
  Avatar,
  Box,
  Button,
  Dialog,
  DialogContent,
  DialogTitle,
  IconButton,
  List,
  ListItem,
  ListItemAvatar,
  Tab,
  Tabs,
  Typography,
  useTheme,
} from "@mui/material";

import { TEST_USERS, UserGroup, UserInfo, USER_TEST_GROUPS } from "@/containers/UsersPage/data";
import emptyAccountImage from "@/shared/emptyAvatar.png";

import { SearchField } from "./SearchField";

interface Props {
  onClose: () => void;
}

enum DialogTabs {
  Group,
  Users,
}

export const AssignDialog: FC<Props> = ({ onClose }) => {
  const theme = useTheme();
  const [selectedTab, setSelectedTab] = useState<DialogTabs>(DialogTabs.Group);
  const [groups, setGroups] = useState<UserGroup[]>(USER_TEST_GROUPS);
  const [users, setUsers] = useState<UserInfo[]>(TEST_USERS);

  const handleChangeTab = useCallback((e: SyntheticEvent<Element, Event>, value: DialogTabs) => {
    setSelectedTab(value);
  }, []);

  const isUsers = selectedTab === DialogTabs.Users;

  return (
    <Dialog
      open
      onClose={onClose}
      maxWidth="md"
      sx={{
        "& .MuiBackdrop-root": { opacity: "0!important" },
        ".MuiDialog-paper": { backgroundColor: theme.palette.common.white, height: 600, width: 450 },
      }}
    >
      <DialogTitle sx={{ m: 0, p: 2 }}>
        <Typography display="inline-flex">Assign to test</Typography>
        <IconButton aria-label="close" onClick={onClose} sx={{ right: 8, top: 8, position: "absolute" }}>
          <CloseIcon />
        </IconButton>
      </DialogTitle>
      <DialogContent sx={{ "&:first-of-type": { p: theme.spacing(0, 2) } }}>
        <Box sx={{}}>
          <Tabs value={selectedTab} onChange={handleChangeTab} aria-label="dialog tabs" sx={{ mb: 1 }}>
            <Tab label="Groups" />
            <Tab label="Users" sx={{ ml: 1 }} />
          </Tabs>
        </Box>
        {isUsers ? (
          <>
            <SearchField fullWidth />
            {!users.length ? (
              <Box height="60%" display="flex" alignContent="center" justifyContent="center" flexDirection="column">
                <PersonSearchIcon color="disabled" sx={{ alignSelf: "center", height: 70, width: 70, mb: 3 }} />
                <Typography align="center">Enter the last name of an existing user to search</Typography>
              </Box>
            ) : (
              <List>
                {users.map(({ avatar, firstName, lastName, email, id }) => (
                  <ListItem
                    disablePadding
                    key={id}
                    sx={{ p: 2, border: `1px solid ${theme.palette.divider}`, mb: 1, display: "flex" }}
                  >
                    <ListItemAvatar>
                      <Avatar alt={[firstName, lastName].join("-")} src={emptyAccountImage} />
                    </ListItemAvatar>
                    <Box flex={1}>
                      <Typography>
                        {firstName}
                        &nbsp;
                        {lastName}
                      </Typography>
                      <Typography variant="caption">{email}</Typography>
                    </Box>
                    <Button variant="contained" color="primary">
                      Assign
                    </Button>
                  </ListItem>
                ))}
              </List>
            )}
          </>
        ) : (
          <List>
            {groups.map((group) => (
              <Fragment key={group.id}>
                <ListItem disablePadding sx={{ p: 2, border: `1px solid ${theme.palette.divider}`, mb: 1 }}>
                  <Typography width="100%">
                    Group:&nbsp;<strong>{group.name}</strong>
                  </Typography>
                  <Button variant="contained" color="primary" sx={{ minWidth: 130 }}>
                    Assign&nbsp;
                    {group.count}
                    &nbsp;
                    <GroupIcon />
                  </Button>
                </ListItem>
              </Fragment>
            ))}
          </List>
        )}
      </DialogContent>
    </Dialog>
  );
};
