import React, { FC } from "react";

import { Checkbox, FormControlLabel, Grid, Link, Typography, useTheme } from "@mui/material";
import { Form, useFormikContext } from "formik";

import { TextFieldFormik } from "@/components/TextFieldFormik";

import { LoginUserFormValues } from "../common";

interface Props {
  isUser: boolean;
}

export const LoginForm: FC<Props> = ({ isUser }) => {
  const theme = useTheme();
  const {
    values: { rememberMe },
    handleChange,
  } = useFormikContext<LoginUserFormValues>();

  return (
    <Form id="login">
      <Grid container spacing={1} direction="column" sx={{ minHeight: 187 }}>
        <Grid item>
          <TextFieldFormik
            fullWidth
            size="small"
            name="username"
            label={isUser ? "Username/Email" : "Username"}
            color="secondary"
            autoComplete="username"
            sx={{
              minHeight: 71,
            }}
          />
        </Grid>
        <Grid item>
          <TextFieldFormik
            fullWidth
            size="small"
            name="password"
            label="Password"
            type="password"
            color="secondary"
            autoComplete="current-password"
            sx={{
              minHeight: 71,
            }}
          />
        </Grid>
        <Grid item>
          <Link fontSize={theme.typography.pxToRem(12)} href="google.com">
            Forgot password?
          </Link>
        </Grid>
        <Grid item>
          <FormControlLabel
            label={<Typography variant="body2">Remember me</Typography>}
            control={
              <Checkbox name="rememberMe" color="secondary" size="small" checked={rememberMe} onChange={handleChange} />
            }
          />
        </Grid>
      </Grid>
    </Form>
  );
};
