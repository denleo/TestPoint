import React, { useState, useEffect, useReducer } from "react";

import EmojiNatureOutlinedIcon from "@mui/icons-material/EmojiNatureOutlined";
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
  const [fetchData, setFetchData] = useState(false);

  useEffect(() => {
    const getAdminTests = async () => {
      const response = await httpAction("admin/tests");
      setTests((response ?? []) as TestInfo[]);
    };

    const getUserTests = async () => {
      const response = await httpAction(`user/tests/?filter=notPassed`);
      setTests((response ?? []) as TestInfo[]);
    };

    try {
      if (isAdmin) {
        getAdminTests();
      } else {
        getUserTests();
      }
    } catch (error) {
      notify("Failed to load tests.", NotificationType.Error);
    }
  }, [fetchData]);

  if (!tests.length) {
    return (
      <Box height="80%" display="flex" alignContent="center" justifyContent="center" flexDirection="column">
        <EmojiNatureOutlinedIcon color="disabled" sx={{ alignSelf: "center", height: 150, width: 150, mb: 3 }} />
        <Typography align="center">Unfortunately there are no tests available to you</Typography>
      </Box>
    );
  }

  return (
    <Grid spacing={3} container>
      {tests.map((test) => (
        <Grid item xs={12} key={test.id} justifyContent="center" display="flex">
          <TestPreviewCard testData={test} rerender={() => setFetchData(!fetchData)} />
        </Grid>
      ))}
    </Grid>
  );
};

export default TestsPage;
