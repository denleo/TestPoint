import React, { useState, useEffect } from "react";

import { Grid, Typography } from "@mui/material";

import { NotificationType, useNotificationStore } from "@/components/NotificationProvider/useNotificationStore";
import { TestInfo } from "@/redux/adminData/state";
import { useSelector } from "@/redux/hooks";
import { isAdminSelector } from "@/redux/selectors";

import { httpAction } from "../../api/httpAction";

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

  return (
    <Grid spacing={3} container>
      {tests.length ? (
        <>
          {tests.map((test) => (
            <Grid item xs={12} key={test.id}>
              <TestPreviewCard testData={test} />
            </Grid>
          ))}
        </>
      ) : (
        <Typography>There is no tests</Typography>
      )}
    </Grid>
  );
};

export default TestsPage;
