import React, { FC, useCallback } from "react";

import { Button, Grid } from "@mui/material";
import { useFormikContext } from "formik";

import { LoginUserFormValues, START_PAGE_STEPS } from "../common";
import { useStartPageStore } from "../useStartPageStore";

interface Props {
  withSignUp: boolean;
}

export const ActionButtons: FC<Props> = ({ withSignUp }) => {
  const { isValid } = useFormikContext<LoginUserFormValues>();
  const setPageStep = useStartPageStore((state) => state.setPageStep);

  const handleSignUpClick = useCallback(() => {
    setPageStep(START_PAGE_STEPS.SIGN_UP);
  }, []);

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
      <Grid item xs />
      <Grid item xs={4}>
        <Button
          fullWidth
          aria-label="submit"
          form="login"
          type="submit"
          disabled={!isValid}
          color="secondary"
          variant="contained"
        >
          Login
        </Button>
      </Grid>
    </Grid>
  );
};
