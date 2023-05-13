/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { FC, useState } from "react";

import { Button, Checkbox, FormControlLabel, Grid, Link, Typography, useTheme } from "@mui/material";
import { Form, useFormikContext } from "formik";

import { ForgotPasswordModal } from "@/components/ForgotPasswordModal";
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
  const [openForgotten, setOpenForgotten] = useState(false);

  return (
    <>
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
            <Link
              component="button"
              fontSize={theme.typography.pxToRem(12)}
              sx={{ textDecoration: "underline" }}
              onClick={() => setOpenForgotten(true)}
            >
              Forgot password?
            </Link>
          </Grid>
          <Grid item>
            <FormControlLabel
              label={<Typography variant="body2">Remember me</Typography>}
              control={
                <Checkbox
                  name="rememberMe"
                  color="secondary"
                  size="small"
                  checked={rememberMe}
                  onChange={handleChange}
                />
              }
            />
          </Grid>
        </Grid>
      </Form>
      <ForgotPasswordModal open={openForgotten} setOpen={setOpenForgotten} />
    </>
  );
};
