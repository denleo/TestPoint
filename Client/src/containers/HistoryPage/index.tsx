import React, { useState, useEffect } from "react";

import EmojiNatureOutlinedIcon from "@mui/icons-material/EmojiNatureOutlined";
import { Box, Grid, Typography, useTheme } from "@mui/material";

import { TestInfo } from "@/redux/adminData/state";

import { httpAction } from "@api/httpAction";
import { useNotificationStore } from "@components/NotificationProvider/useNotificationStore";

import { TestResultCard } from "./TestResultCard";

const HistoryPage = () => {
  const theme = useTheme();
  const [tests, setTests] = useState<TestInfo[]>([]);
  const notify = useNotificationStore((store) => store.notify);

  useEffect(() => {
    const fetchTests = async () => {
      try {
        const response = await httpAction("/user/tests/?filter=passed");
        setTests(response as TestInfo[]);
      } catch (error) {
        notify("Failed to load history.");
      }
    };

    fetchTests();
  }, []);

  if (!tests.length) {
    return (
      <Box height="80%" display="flex" alignContent="center" justifyContent="center" flexDirection="column">
        <EmojiNatureOutlinedIcon color="disabled" sx={{ alignSelf: "center", height: 150, width: 150, mb: 3 }} />
        <Typography align="center">Unfortunately there are no tests available to you</Typography>
      </Box>
    );
  }

  return (
    <Box display="flex" justifyContent="center" width="100%">
      <Grid spacing={3} container display="flex" justifyContent="center" sx={{ maxWidth: 1280 }}>
        {tests.map((test) => (
          <Grid item xs={12} md={6} key={test.id}>
            <TestResultCard testInfo={test} />
          </Grid>
        ))}
      </Grid>
    </Box>
  );
};

export default HistoryPage;
