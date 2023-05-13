/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { FC, useCallback } from "react";

import GppBadIcon from "@mui/icons-material/GppBad";
import VerifiedUserIcon from "@mui/icons-material/VerifiedUser";
import { Box, Link, Tooltip, useTheme } from "@mui/material";
import { AxiosError } from "axios";

import { httpAction } from "@/api/httpAction";
import { WHITE } from "@/common/theme/colors";
import { useNotificationStore } from "@/components/NotificationProvider/useNotificationStore";

import { useSelector } from "@redux/hooks";
import { isEmailConfirmedSelector } from "@redux/selectors";

export const EmailConfirmedCheck = () => {
  const isConfirmed = useSelector(isEmailConfirmedSelector);
  const theme = useTheme();
  const notify = useNotificationStore((store) => store.notify);

  const confirmEmail = useCallback(async () => {
    try {
      await httpAction("session/user/email-verification", undefined, "POST");
      notify("Please, check your email for confirm.");
    } catch (error) {
      notify(error instanceof AxiosError ? error.message : "Failed to confirm email.");
    }
  }, [notify]);

  return (
    <>
      <Box width={10} height={10} sx={{ position: "absolute", top: 0, right: 0, backgroundColor: WHITE }} />
      {isConfirmed ? (
        <Tooltip title="Email is verified">
          <VerifiedUserIcon sx={{ position: "absolute", top: -5, right: -5 }} color="primary" />
        </Tooltip>
      ) : (
        <Tooltip
          title={
            <>
              Email is not confirmed &nbsp;
              <Link
                component="button"
                fontSize={theme.typography.pxToRem(12)}
                sx={{ textDecoration: "underline", color: theme.palette.common.white }}
                onClick={confirmEmail}
              >
                Confirm
              </Link>
            </>
          }
        >
          <GppBadIcon sx={{ position: "absolute", top: -5, right: -5 }} color="error" />
        </Tooltip>
      )}
    </>
  );
};
