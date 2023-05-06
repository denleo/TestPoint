import React from "react";

import { Alert, Slide, SlideProps, Snackbar } from "@mui/material";

import { useNotificationStore } from "./useNotificationStore";

const TransitionLeft = (props: SlideProps) => {
  return <Slide {...props} direction="left" />;
};

export const NotificationProvider = () => {
  const notifications = useNotificationStore((store) => store.notifications);
  return (
    <>
      {notifications.map(({ id, message, type }) => (
        <Snackbar
          key={id}
          open
          TransitionComponent={TransitionLeft}
          anchorOrigin={{ horizontal: "right", vertical: "bottom" }}
        >
          <Alert severity={type} sx={{ width: 200 }}>
            {message}
          </Alert>
        </Snackbar>
      ))}
    </>
  );
};
