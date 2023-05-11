import React, { useEffect, useState } from "react";

import { alpha, Box, Grid, Typography, useTheme } from "@mui/material";
import { useLocation, useNavigate, useSearchParams } from "react-router-dom";

import { NotificationType, useNotificationStore } from "@/components/NotificationProvider/useNotificationStore";
import { ProgressScore } from "@/components/ProgressScore";
import { TestDifficultyChip } from "@/components/TestDifficultyChip";
import { TestData } from "@/redux/adminData/state";

import { httpAction } from "@api/httpAction";

import { TestResult } from "./common";
import { QuestionList } from "./QuestionsList";

const ResultsPage = () => {
  const [testData, setTestData] = useState<TestData | null>(null);
  const [testResult, setTestResult] = useState<TestResult | null>(null);

  const [searchParams] = useSearchParams();
  const notify = useNotificationStore((store) => store.notify);
  const testId = searchParams.get("testId");

  const theme = useTheme();
  const navigate = useNavigate();
  const location = useLocation();

  const from = location.state?.from?.pathname || "/";

  useEffect(() => {
    if (!testId) return;

    const fetchData = async () => {
      try {
        const fetchTestData = (await httpAction(`tests/${testId}`)) as TestData;
        const fetchTestResult = await httpAction(`tests/${testId}/results`);
        setTestData(fetchTestData);
        setTestResult(fetchTestResult);
      } catch (error) {
        notify("Failed to load test", NotificationType.Error);
        navigate(from);
      }
    };
    fetchData();
  }, [testId]);

  if (!testData || !testResult) return null;

  return (
    <Box display="flex" width="100%" height="100%" flexDirection="column" alignItems="center">
      <Grid
        container
        sx={{ maxWidth: 1280, backgroundColor: alpha(theme.palette.info.light, 0.32), p: 4, borderRadius: 4 }}
      >
        <Grid item display="flex" justifyContent="space-between" xs={12}>
          <Typography variant="h4">
            Test: <strong>{testData.name}</strong>
          </Typography>
          <TestDifficultyChip difficulty={testData.difficulty ?? 0} />
        </Grid>
        <Grid item xs={6} sx={{ mt: 5 }}>
          <Typography>
            Author: <strong>{testData.authorId}</strong>
          </Typography>
          <Typography>
            Estimated time: <strong>{testData.estimatedTime}</strong>
          </Typography>
          <Typography>
            Completion time: <strong>{testResult.completionTime}</strong>
          </Typography>
          <Typography>
            Result: <strong>{`${testResult.correctAnswersCount}/${testData.questions.length}`}</strong>
          </Typography>
        </Grid>
        <Grid item xs={6} display="flex" alignItems="center" justifyContent="flex-end" sx={{ mt: 5 }}>
          <ProgressScore percent={(testResult.score ?? 0) * 10} label={testResult.score.toString() ?? "0"} />
        </Grid>
      </Grid>
      <Box width={1280}>
        <QuestionList questions={testData.questions} history={testResult.history} />
      </Box>
    </Box>
  );
};

export default ResultsPage;