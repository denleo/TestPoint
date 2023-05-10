import React, { useState, Fragment, useCallback, ChangeEvent, useEffect } from "react";

import AddIcon from "@mui/icons-material/Add";
import GroupIcon from "@mui/icons-material/Group";
import PersonSearchIcon from "@mui/icons-material/PersonSearch";
import { Box, Divider, List, ListItem, styled, Typography, ListItemButton, TextField, IconButton } from "@mui/material";

import { httpAction } from "@/api/httpAction";
import { NotificationType, useNotificationStore } from "@/components/NotificationProvider/useNotificationStore";

import { SearchField } from "../../components/SearchField";

import { UserGroup, UserInfo } from "./data";
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
  const [groupName, setGroupName] = useState<string>("");
  const [groups, setGroups] = useState<UserGroup[]>([]);
  const notify = useNotificationStore((store) => store.notify);
  const [users, setUsers] = useState<UserInfo[]>([]);
  const [groupUsers, setGroupUsers] = useState<UserInfo[]>([]);

  const fetchUserGroups = useCallback(async () => {
    const response = await httpAction("usergroups");
    setGroups((response ?? []) as UserGroup[]);
  }, []);

  const fetchGroupUsers = useCallback(async () => {
    if (!expandedGroup) return;
    const response = await httpAction(`usergroups/${expandedGroup.id}/users`);
    setGroupUsers((response ?? []) as UserInfo[]);
  }, [expandedGroup]);

  useEffect(() => {
    try {
      fetchUserGroups();
    } catch (error) {
      notify("Failed to load groups.", NotificationType.Error);
    }
  }, []);

  const handleOpenGroup = useCallback(
    (group: UserGroup) => () => {
      setExpandedGroup(group);
    },
    []
  );

  const addUserToGroup = useCallback(
    async (id: string) => {
      if (!expandedGroup) return;
      await httpAction(`usergroups/${expandedGroup.id}/users/${id}`, undefined, "POST");
      await fetchGroupUsers();
    },
    [expandedGroup]
  );

  const handleCloseGroup = useCallback(() => {
    setExpandedGroup(false);
  }, []);

  const deleteGroup = useCallback(async () => {
    if (!expandedGroup) return;
    await httpAction(`usergroups/${expandedGroup.id}`, undefined, "DELETE");
    await fetchUserGroups();
    setExpandedGroup(false);
  }, [expandedGroup]);

  const handleChangeName = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setGroupName(e.target.value);
  }, []);

  const addNewGroup = useCallback(async () => {
    if (!groupName) {
      notify("Can't create group with empty name!", NotificationType.Error);
      return;
    }

    try {
      const newUserGroup = (await httpAction("usergroups", { groupName })) as UserGroup;
      await fetchUserGroups();
      setExpandedGroup(newUserGroup);
    } catch {
      notify("Failed to create user group!", NotificationType.Error);
    }
  }, [groupName]);

  const handleSearchUser = useCallback(async (search: string) => {
    try {
      const response = await httpAction(`users/?filter=${search}`);
      setUsers((response ?? []) as UserInfo[]);
    } catch {
      setUsers([]);
    }
  }, []);

  useEffect(() => {
    fetchGroupUsers();
  }, [expandedGroup]);

  const removeUser = useCallback(
    async (id: string) => {
      if (!expandedGroup) return;
      await httpAction(`usergroups/${expandedGroup.id}/users/${id}`, undefined, "DELETE");
      await fetchGroupUsers();
    },
    [expandedGroup]
  );

  return (
    <LayoutPage>
      <Box flex={1} pr={3} overflow="auto">
        {expandedGroup ? (
          <ExpandedGroup
            group={expandedGroup}
            users={groupUsers}
            onBack={handleCloseGroup}
            onDelete={deleteGroup}
            onClickUser={removeUser}
          />
        ) : (
          <>
            <Typography variant="h4" sx={{ mb: 1 }}>
              All Groups
            </Typography>
            <List>
              {!!groups.length &&
                groups.map((group) => (
                  <Fragment key={group.id}>
                    <ListItem disablePadding>
                      <ListItemButton onClick={handleOpenGroup(group)} sx={{ p: 2 }}>
                        <Typography width="100%">
                          Group:&nbsp;<strong>{group.name}</strong>
                        </Typography>
                        <Typography display="inline-flex">
                          {group.membersCount}
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
                <IconButton color="info" onClick={addNewGroup}>
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
          <SearchField size="small" onChange={(e) => handleSearchUser(e.target.value)} />
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
