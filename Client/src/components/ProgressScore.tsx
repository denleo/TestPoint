import React, { FC, useState, useEffect, useMemo } from "react";

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

  const color = useMemo(() => {
    if (percent >= 0 && percent < 33) return theme.palette.error.light;

    if (percent >= 33 && percent < 66) return theme.palette.warning.light;

    return theme.palette.success.light;
  }, [percent]);

  return (
    <Box sx={{ position: "relative", display: "inline-flex" }}>
      <CircularProgress value={progress} variant="determinate" size={80} sx={{ color, zIndex: 100 }} />
      <CircularProgress
        value={100}
        variant="determinate"
        size={80}
        sx={{ color: theme.palette.divider, zIndex: 1, position: "absolute" }}
      />
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
