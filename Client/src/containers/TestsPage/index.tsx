import React from "react";

import { Grid } from "@mui/material";

import { TEST_DATA_1 } from "./data";
import { TestPreviewCard } from "./TestPreviewCard";

const TestsPage = () => {
  return (
    <Grid spacing={3} container>
      <Grid item xs={12}>
        <TestPreviewCard testData={TEST_DATA_1} />
      </Grid>
      <Grid item xs={12}>
        <TestPreviewCard testData={TEST_DATA_1} />
      </Grid>
      <Grid item xs={12}>
        <TestPreviewCard testData={TEST_DATA_1} />
      </Grid>
    </Grid>
  );
};

export default TestsPage;
