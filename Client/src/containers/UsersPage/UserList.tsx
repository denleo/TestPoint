import React, { FC } from "react";

import AddIcon from "@mui/icons-material/Add";
import RemoveIcon from "@mui/icons-material/Remove";
import {
  Avatar,
  Box,
  List,
  ListItem,
  ListItemAvatar,
  ListItemButton,
  ListItemIcon,
  Typography,
  useTheme,
} from "@mui/material";

import { CopyToClipboardLink } from "@/components/CopyToClipboardLink";
import emptyAccountImage from "@/shared/emptyAvatar.png";

import { UserInfo } from "./data";

interface Props {
  users: UserInfo[];
  onClickUser: (id: string) => void;
  isGroup?: boolean;
  showIcon?: boolean;
}

export const UserList: FC<Props> = ({ users, onClickUser, isGroup = false, showIcon = true }) => {
  const theme = useTheme();
  if (!users.length) return null;

  return (
    <List>
      {users.map(({ base64Avatar: avatar, firstName, lastName, email, id }) => (
        <ListItem disablePadding key={id}>
          <ListItemButton onClick={() => onClickUser(id)}>
            <ListItemAvatar>
              <Avatar
                alt={[firstName, lastName].join("-")}
                src={isGroup ? avatar ?? emptyAccountImage : emptyAccountImage}
              />
            </ListItemAvatar>
            <Box>
              <Typography>
                {firstName}
                &nbsp;
                {lastName}
              </Typography>
              <CopyToClipboardLink
                href={email}
                color="secondary"
                sx={{ "&:hover, &:active": { color: theme.palette.common.white } }}
              >
                {email}
              </CopyToClipboardLink>
            </Box>
            {showIcon && <ListItemIcon sx={{ ml: "auto" }}>{isGroup ? <RemoveIcon /> : <AddIcon />}</ListItemIcon>}
          </ListItemButton>
        </ListItem>
      ))}
    </List>
  );
};
