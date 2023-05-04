import React, { FC } from "react";

import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import DeleteIcon from "@mui/icons-material/Delete";
import { Box, IconButton, Typography, useTheme } from "@mui/material";

import { UserGroup, UserInfo } from "./data";
import { UserList } from "./UserList";

interface Props {
  group: UserGroup;
  users: UserInfo[];
  onClickUser: (id: string) => void;
  onBack: () => void;
  onDelete: () => void;
}

export const ExpandedGroup: FC<Props> = ({ group, users, onClickUser, onBack, onDelete }) => {
  const theme = useTheme();
  return (
    <Box>
      <Box display="flex" gap={1} alignItems="center">
        <IconButton onClick={onBack}>
          <ArrowBackIcon />
        </IconButton>
        <Typography variant="h4" display="inline-flex">
          Group:&nbsp;
        </Typography>
        <Typography variant="h4" color={theme.palette.grey[700]}>
          {group.name}
        </Typography>
        <Box flex={1} display="flex" justifyContent="flex-end">
          <IconButton onClick={onDelete}>
            <DeleteIcon />
          </IconButton>
        </Box>
      </Box>
      <UserList users={users} isGroup onClickUser={onClickUser} />
    </Box>
  );
};
