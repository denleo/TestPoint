import React from "react";

import { Box, Typography } from "@mui/material";

import { useSelector } from "@/redux/hooks";
import { userAccountNameSelector } from "@/redux/selectors";

const ProfilePage = () => {
  const userName = useSelector(userAccountNameSelector);

  return (
    <Box width="100%" height="100%">
      <Typography variant="h1">{`Welcome back, ${userName}!`}</Typography>
    </Box>
  );
};

export default ProfilePage;
