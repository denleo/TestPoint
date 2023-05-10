import React, { FC, useState, useEffect } from "react";

import { Box, CircularProgress, Typography, useTheme } from "@mui/material";

interface Props {
  percent: number;
  label: string;
}

export const ProgressScore: FC<Props> = ({ percent, label }) => {
  const [progress, setProgress] = useState(0);
  const theme = useTheme();

  useEffect(() => {
    const timer = setInterval(() => {
      setProgress((prevProgress) => (prevProgress >= percent ? prevProgress : prevProgress + 1));
    }, 20);

    return () => {
      clearInterval(timer);
    };
  }, [percent]);

  return (
    <Box sx={{ position: "relative", display: "inline-flex" }}>
      <CircularProgress value={progress} variant="determinate" size={80} sx={{ color: theme.palette.success.light }} />
      <Box
        sx={{
          top: 0,
          left: 0,
          bottom: 0,
          right: 0,
          position: "absolute",
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        <Typography variant="h4" component="div" sx={{ color: theme.palette.secondary.main }}>
          {label}
        </Typography>
      </Box>
    </Box>
  );
};
