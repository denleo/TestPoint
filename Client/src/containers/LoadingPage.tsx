import React, { FC } from "react";

import { Box, CircularProgress } from "@mui/material";

import { useBreakpoint } from "@/api/hooks/useBreakPoint";
import { IconFullLogo } from "@/common/icons";

const LoadingPage: FC = () => {
  const mdUp = useBreakpoint("md");

  return (
    <Box height="100vh" width="100vw" display="flex" justifyContent="center" alignItems="center" flexDirection="column">
      <IconFullLogo width={mdUp ? 360 : 180} height={mdUp ? 70 : 35} />
      <CircularProgress color="secondary" size={mdUp ? 80 : 40} sx={{ mt: 2 }} />
    </Box>
  );
};

export default LoadingPage;
