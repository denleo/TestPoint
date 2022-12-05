import React, { FC } from "react";

import { Alert, Box, Divider, Grid, Typography, useTheme } from "@mui/material";

import { SVGSadFace } from "@/common/icons";

interface Props {
  error?: Error;
  message?: string;
}

export const ErrorScreen: FC<Props> = ({ error, message }) => {
  const theme = useTheme();
  return (
    <Box
      sx={{
        width: "100vw",
        minHeight: "100vh",
        backgroundColor: theme.palette.background.default,
      }}
    >
      <Grid container spacing={2} direction="column">
        <Grid
          item
          sx={{
            display: "flex",
            flexDirection: "column",
            margin: theme.spacing(2),
          }}
        >
          <SVGSadFace />
          <Typography variant="h4" sx={{ paddingTop: theme.spacing(2) }}>
            Sorry! Web-service is not responding. Contact the administrator.
          </Typography>
        </Grid>
        <Divider />
        {error && (
          <Grid item xs={12} sx={{ margin: theme.spacing(2) }}>
            <Typography
              variant="body2"
              component="p"
              sx={{ overflowWrap: "anywhere" }}
            >
              {error.stack}
            </Typography>
          </Grid>
        )}
        <Divider />
        <Grid item xs={12} md={6} sx={{ margin: theme.spacing(2) }}>
          <Alert severity="error">{message}</Alert>
        </Grid>
      </Grid>
    </Box>
  );
};
