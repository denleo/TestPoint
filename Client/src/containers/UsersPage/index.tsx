import React, { useState, Fragment, useCallback, ChangeEvent } from "react";

import AddIcon from "@mui/icons-material/Add";
import GroupIcon from "@mui/icons-material/Group";
import PersonSearchIcon from "@mui/icons-material/PersonSearch";
import {
  Box,
  Divider,
  List,
  ListItem,
  styled,
  Typography,
  ListItemButton,
  useTheme,
  TextField,
  IconButton,
} from "@mui/material";

import { SearchField } from "../../components/SearchField";

import { UserGroup, USER_TEST_GROUPS, UserInfo, TEST_USERS } from "./data";
import { ExpandedGroup } from "./ExpandedGroup";
import { UserList } from "./UserList";

const LayoutPage = styled("div")(({ theme }) => ({
  display: "flex",
  justifyContent: "center",
  width: "100%",
  height: "100%",
}));

const UsersPage = () => {
  const [expandedGroup, setExpandedGroup] = useState<UserGroup | false>();
  const [groupUsers, setGroupUsers] = useState<UserInfo[]>([]);
  const [groupName, setGroupName] = useState<string>("");
  const [groups, setGroups] = useState<UserGroup[]>(USER_TEST_GROUPS);
  // const [users, setUsers] = useState<UserInfo[]>(TEST_USERS);
  const [users, setUsers] = useState<UserInfo[]>([]);

  const theme = useTheme();

  const handleOpenGroup = useCallback(
    (group: UserGroup) => () => {
      setExpandedGroup(group);
      setGroupUsers(TEST_USERS.slice(0, 6));
    },
    []
  );

  const removeUserFromGroup = useCallback((id: string) => console.log(`remove ${id}`), []);

  const addUserToGroup = useCallback(
    (id: string) => {
      if (!expandedGroup) return;
      console.log(`add ${id}`);
    },
    [expandedGroup]
  );

  const handleCloseGroup = useCallback(() => {
    setExpandedGroup(false);
    setGroupUsers([]);
  }, []);

  const deleteGroup = useCallback(() => {
    setExpandedGroup(false);
    setGroupUsers([]);
  }, []);

  const handleChangeName = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setGroupName(e.target.value);
  }, []);

  return (
    <LayoutPage>
      <Box flex={1} pr={3} overflow="auto">
        {expandedGroup ? (
          <ExpandedGroup
            group={expandedGroup}
            users={groupUsers}
            onClickUser={removeUserFromGroup}
            onBack={handleCloseGroup}
            onDelete={deleteGroup}
          />
        ) : (
          <>
            <Typography variant="h4" sx={{ mb: 1 }}>
              All Groups
            </Typography>
            <List>
              {groups.map((group) => (
                <Fragment key={group.id}>
                  <ListItem disablePadding>
                    <ListItemButton onClick={handleOpenGroup(group)} sx={{ p: 2 }}>
                      <Typography width="100%">
                        Group:&nbsp;<strong>{group.name}</strong>
                      </Typography>
                      <Typography display="inline-flex">
                        {group.count}
                        &nbsp;
                      </Typography>
                      <GroupIcon />
                    </ListItemButton>
                  </ListItem>
                  <Divider />
                </Fragment>
              ))}
              <ListItem disablePadding sx={{ p: 2, display: "flex", gap: 2, alignItems: "center" }}>
                <Typography>Create New&nbsp;</Typography>
                <Box flex={1}>
                  <TextField
                    placeholder="Group Name"
                    fullWidth
                    variant="standard"
                    value={groupName}
                    onChange={handleChangeName}
                  />
                </Box>
                <IconButton color="info">
                  <AddIcon />
                </IconButton>
              </ListItem>
            </List>
          </>
        )}
      </Box>
      <Divider orientation="vertical" flexItem />
      <Box flex={1} pl={3} overflow="auto">
        <Box display="flex">
          <Typography variant="h4" sx={{ mr: 2 }}>
            Users
          </Typography>
          <SearchField size="small" />
        </Box>
        {users.length ? (
          <UserList users={users} onClickUser={addUserToGroup} showIcon={!!expandedGroup} />
        ) : (
          <Box height="80%" display="flex" alignContent="center" justifyContent="center" flexDirection="column">
            <PersonSearchIcon color="disabled" sx={{ alignSelf: "center", height: 150, width: 150, mb: 3 }} />
            <Typography align="center">Enter the last name of an existing user to search</Typography>
          </Box>
        )}
      </Box>
    </LayoutPage>
  );
};

export default UsersPage;
