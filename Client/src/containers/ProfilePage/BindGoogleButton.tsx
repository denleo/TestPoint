import React, { useCallback, useState } from "react";

import LockIcon from "@mui/icons-material/Lock";
import NoEncryptionGmailerrorredIcon from "@mui/icons-material/NoEncryptionGmailerrorred";
import { IconButton, Tooltip } from "@mui/material";
import { useGoogleLogin } from "@react-oauth/google";
import { AxiosError } from "axios";

import { httpAction } from "@/api/httpAction";
import { IconGoogle } from "@/common/icons";
import { NotificationType, useNotificationStore } from "@/components/NotificationProvider/useNotificationStore";
import { useSelector } from "@/redux/hooks";
import { isGoogleAuthEnabledSelector } from "@/redux/selectors";

export const BindGoogleButton = () => {
  const [complete, setComplete] = useState(false);
  const isGoogleAuthEnabled = useSelector(isGoogleAuthEnabledSelector);
  const notify = useNotificationStore((store) => store.notify);

  const bindGoogle = useGoogleLogin({
    onSuccess: async ({ access_token: accessToken }) => {
      try {
        await httpAction("session/user/bind-google", accessToken, "PUT", {
          headers: {
            "Content-Type": "application/json",
          },
        });
        notify("You successfully bind your account!");
        setComplete(true);
      } catch (error) {
        notify(error instanceof AxiosError ? error.message : "Failed to bind account.", NotificationType.Error);
      }
    },
    onError: () => {
      notify("Failed to login.", NotificationType.Error);
    },
    flow: "implicit",
  });

  const unbindGoogle = useCallback(async () => {
    try {
      await httpAction("session/user/unbind-google", undefined, "DELETE");
      setComplete(false);
      notify("You successfully unbind your account!");
    } catch (error) {
      notify(error instanceof AxiosError ? error.message : "Failed to unbind account.", NotificationType.Error);
    }
  }, [notify]);

  const isBind = complete || isGoogleAuthEnabled;

  return (
    <Tooltip title={`${isBind ? "Unbind" : "Bind"} Google account for authentication`}>
      <IconButton sx={{ position: "relative", ml: 1 }} onClick={isBind ? unbindGoogle : () => bindGoogle()}>
        <IconGoogle />
        {isBind ? (
          <LockIcon sx={{ position: "absolute", right: 0, top: 0, width: 15, height: 15 }} />
        ) : (
          <NoEncryptionGmailerrorredIcon
            color="error"
            sx={{ position: "absolute", right: 0, top: 0, width: 15, height: 15 }}
          />
        )}
      </IconButton>
    </Tooltip>
  );
};
