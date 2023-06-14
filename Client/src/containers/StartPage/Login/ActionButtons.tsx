import React, { FC, useCallback } from "react";

import GoogleIcon from "@mui/icons-material/Google";
import { Box, Button, Grid } from "@mui/material";
import { useGoogleLogin } from "@react-oauth/google";
import { AxiosError } from "axios";
import { useFormikContext } from "formik";

import { httpAction } from "@/api/httpAction";
import { setUserTokenToStorage } from "@/api/userToken";
import { NotificationType, useNotificationStore } from "@/components/NotificationProvider/useNotificationStore";

import { LoginUserFormValues, START_PAGE_STEPS } from "../common";
import { useStartPageStore } from "../useStartPageStore";

interface Props {
  withSignUp: boolean;
  finishLogin: () => Promise<void>;
}

export const ActionButtons: FC<Props> = ({ withSignUp, finishLogin }) => {
  const { isValid } = useFormikContext<LoginUserFormValues>();
  const setPageStep = useStartPageStore((state) => state.setPageStep);
  const notify = useNotificationStore((store) => store.notify);

  const handleSignUpClick = useCallback(() => {
    setPageStep(START_PAGE_STEPS.SIGN_UP);
  }, []);

  const handleGoogleLogin = useGoogleLogin({
    onSuccess: async ({ access_token: accessToken }) => {
      try {
        const authToken = await httpAction(`auth/google/user`, accessToken, "POST", {
          headers: {
            "Content-Type": "application/json",
          },
        });
        setUserTokenToStorage(authToken);
        await finishLogin();
      } catch (error) {
        notify(error instanceof AxiosError ? error.message : "Failed to login.", NotificationType.Error);
      }
    },
    onError: () => {
      notify("Failed to login.", NotificationType.Error);
    },
    flow: "implicit",
  });

  return (
    <Grid container sx={{ height: 50 }}>
      <Grid item xs={4}>
        {withSignUp && (
          <Button
            fullWidth
            aria-label="sign up"
            color="primary"
            variant="contained"
            type="button"
            onClick={handleSignUpClick}
          >
            Sign up
          </Button>
        )}
      </Grid>
      <Grid item xs>
        {withSignUp && (
          <Box display="flex" flex={1} justifyContent="center">
            <Button variant="contained" color="secondary" onClick={() => handleGoogleLogin()}>
              <GoogleIcon />
            </Button>
          </Box>
        )}
      </Grid>
      <Grid item xs={4}>
        <Button
          fullWidth
          aria-label="submit"
          form="login"
          type="submit"
          disabled={!isValid}
          color="primary"
          variant="contained"
        >
          Login
        </Button>
      </Grid>
    </Grid>
  );
};
