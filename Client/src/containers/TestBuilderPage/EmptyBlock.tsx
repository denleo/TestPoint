import React from "react";

import AutoAwesomeIcon from "@mui/icons-material/AutoAwesome";
import { Box, Typography, useTheme } from "@mui/material";

export const EmptyBlock = () => {
  const theme = useTheme();
  return (
    <Box flex={1} display="flex" alignItems="center" flexDirection="column" mb={8} justifyContent="center">
      <AutoAwesomeIcon color="disabled" sx={{ height: 150, width: 150, mb: 3 }} />
      <Typography variant="h4" color={theme.palette.text.disabled}>
        Start creating test by adding questions
      </Typography>
    </Box>
  );
};
