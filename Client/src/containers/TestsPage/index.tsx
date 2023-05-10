import React, { useState, useEffect } from "react";

import AccessibleForwardIcon from "@mui/icons-material/AccessibleForward";
import { Box, Grid, Typography } from "@mui/material";

import { NotificationType, useNotificationStore } from "@/components/NotificationProvider/useNotificationStore";
import { TestInfo } from "@/redux/adminData/state";
import { useSelector } from "@/redux/hooks";
import { isAdminSelector } from "@/redux/selectors";

import { httpAction } from "@api/httpAction";

import { TestPreviewCard } from "./TestPreviewCard";

const TestsPage = () => {
  const [tests, setTests] = useState<TestInfo[]>([]);
  const isAdmin = useSelector(isAdminSelector);
  const notify = useNotificationStore((store) => store.notify);

  useEffect(() => {
    const getTests = async () => {
      const response = await httpAction("tests");
      setTests((response ?? []) as TestInfo[]);
    };

    try {
      if (isAdmin) getTests();
    } catch (error) {
      notify("Failed to load tests.", NotificationType.Error);
    }
  }, []);

  if (!tests.length) {
    return (
      <Box height="80%" display="flex" alignContent="center" justifyContent="center" flexDirection="column">
        <AccessibleForwardIcon color="disabled" sx={{ alignSelf: "center", height: 150, width: 150, mb: 3 }} />
        <Typography align="center">Unfortunately there are no tests available to you</Typography>
      </Box>
    );
  }

  return (
    <Grid spacing={3} container>
      {tests.map((test) => (
        <Grid item xs={12} key={test.id}>
          <TestPreviewCard testData={test} />
        </Grid>
      ))}
    </Grid>
  );
};

export default TestsPage;
