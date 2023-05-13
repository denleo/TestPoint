import React, { FC, useState, useCallback } from "react";

import DoubleArrowIcon from "@mui/icons-material/DoubleArrow";
import { TextField, Typography, Button, styled, Grid, Dialog } from "@mui/material";
import { AxiosError } from "axios";

import { httpAction } from "@/api/httpAction";

import { NotificationType, useNotificationStore } from "./NotificationProvider/useNotificationStore";

interface Props {
  open: boolean;
  setOpen: (open: boolean) => void;
}

const ModalPaper = styled(Dialog)(({ theme }) => ({
  "& .MuiBackdrop-root": { opacity: "0!important" },
  ".MuiDialog-paper": {
    backgroundColor: theme.palette.common.white,
    width: 400,
    padding: theme.spacing(2),
  },
}));

export const ForgotPasswordModal: FC<Props> = ({ open, setOpen }) => {
  const [username, setUsername] = useState("");
  const notify = useNotificationStore((store) => store.notify);

  const handleClick = useCallback(async () => {
    if (username.length < 5) {
      notify("Please enter username. Minimum 5 symbols.", NotificationType.Error);
      return;
    }

    try {
      await httpAction("user/password/forgot-password", { username });
      notify("Success! Please check your email.", NotificationType.Success);
    } catch (error) {
      notify(
        error instanceof AxiosError ? error.message : "Failed to send username. Try again.",
        NotificationType.Error
      );
    }
  }, [username]);

  return (
    <ModalPaper open={open} onClose={() => setOpen(false)} maxWidth="md">
      <Grid container>
        <Grid item xs={12}>
          <Typography gutterBottom variant="body2">
            Please, enter your username
          </Typography>
        </Grid>
        <Grid item xs>
          <TextField
            fullWidth
            autoFocus
            size="small"
            value={username}
            placeholder="username"
            onChange={(e) => setUsername(e.target.value)}
          />
        </Grid>
        <Grid item sx={{ display: "flex", alignContent: "center", pl: 1 }}>
          <Button variant="contained" color="primary" size="small" onClick={handleClick}>
            <DoubleArrowIcon />
          </Button>
        </Grid>
      </Grid>
    </ModalPaper>
  );
};
