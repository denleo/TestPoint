import React, { FC, useState, useEffect, useCallback } from "react";

import { QuestionMarkRounded } from "@mui/icons-material";
import ArrowForwardIosOutlinedIcon from "@mui/icons-material/ArrowForwardIosOutlined";
import { Button, Grid, Paper, styled, Typography, useTheme } from "@mui/material";
import { useLocation, useNavigate } from "react-router-dom";

import { useBreakpoint } from "@/api/hooks/useBreakPoint";
import { httpAction } from "@/api/httpAction";
import { useNotificationStore, NotificationType } from "@/components/NotificationProvider/useNotificationStore";
import { ProgressScore } from "@/components/ProgressScore";
import { TestDifficultyChip } from "@/components/TestDifficultyChip";
import { TestInfo } from "@/redux/adminData/state";

import { TestResult } from "../ResultsPage/common";
import { useResultsPageStore } from "../ResultsPage/useResultsPageStore";

const PaperSection = styled(Paper)(({ theme }) => ({
  minHeight: 200,
  display: "flex",
  flexDirection: "column",
  justifyContent: "space-between",
  padding: theme.spacing(2),
  width: "100%",
  maxWidth: 500,
}));

const QuestionMarkWrapper = styled("div")(({ theme }) => ({
  borderRadius: "50%",
  padding: theme.spacing(0.5),
  backgroundColor: theme.palette.secondary.main,
  color: theme.palette.common.white,
}));

const TestInformationWrapper = styled("div")(({ theme }) => ({
  padding: theme.spacing(1, 4),
  flex: 1,
}));

const Dot = styled("p")(({ theme }) => ({
  borderRadius: "50%",
  display: "inline-flex",
  width: 7,
  height: 7,
  margin: theme.spacing(0, 1, 0, 2),
  backgroundColor: theme.palette.secondary.dark,
}));

interface Props {
  testInfo: TestInfo;
}

export const TestResultCard: FC<Props> = ({ testInfo }) => {
  const { name, questionCount, estimatedTime, author } = { ...testInfo };
  const notify = useNotificationStore((store) => store.notify);
  const location = useLocation();
  const theme = useTheme();

  const [testResult, setTestResult] = useState<TestResult>();

  const navigate = useNavigate();
  const mdUp = useBreakpoint("md");

  const openDetails = useResultsPageStore((store) => store.setTest);

  useEffect(() => {
    const fetchTestResult = async () => {
      try {
        const response = await httpAction(`tests/${testInfo.id}/results`);
        setTestResult(response as TestResult);
      } catch (error) {
        notify(`Failed to load tesult for test ${testInfo.name}`, NotificationType.Error);
      }
    };

    fetchTestResult();
  }, [testInfo, notify]);

  const openTestResults = useCallback(() => {
    openDetails(testInfo.id);
    navigate("/results", { state: { from: location } });
  }, [testResult]);

  if (!testResult) return null;

  return (
    <PaperSection>
      <Grid container alignItems="center" spacing={1}>
        <Grid item>
          <QuestionMarkWrapper>
            <QuestionMarkRounded fontSize="medium" />
          </QuestionMarkWrapper>
        </Grid>
        <Grid item xs>
          <Typography mr={0.5} display="inline-block" variant="h4">
            TEST: {name}
          </Typography>
        </Grid>
        <Grid item>
          <TestDifficultyChip difficulty={testInfo.difficulty} />
        </Grid>
      </Grid>

      <TestInformationWrapper>
        <Grid container spacing={2}>
          <Grid item flexDirection="column" xs={mdUp ? true : 12}>
            <Typography variant="subtitle2">Test Information:</Typography>
            <div>
              <Dot />
              <Typography component="span" variant="body2">
                Number of questions: <strong>{questionCount}</strong>
              </Typography>
            </div>
            <div>
              <Dot />
              <Typography component="span" variant="body2">
                Author: <strong>{author}</strong>
              </Typography>
            </div>
            <div>
              <Dot />
              <Typography component="span" variant="body2">
                Estimated time: <strong>{estimatedTime}</strong>
              </Typography>
            </div>
            <div>
              <Dot />
              <Typography component="span" variant="body2">
                Completion time: <strong>{testResult.completionTime}</strong>
              </Typography>
            </div>
            <div>
              <Dot />
              <Typography component="span" variant="body2">
                Result: <strong>{`${testResult.correctAnswersCount}/${testResult.history.length}`}</strong>
              </Typography>
            </div>
          </Grid>
          <Grid item display="flex" sx={{ mt: "auto", mb: "auto" }}>
            <ProgressScore percent={testResult.score * 10} label={testResult.score.toFixed(1)} />
          </Grid>
          <Grid item xs={12}>
            <Button
              fullWidth
              size="large"
              variant="text"
              color="primary"
              endIcon={<ArrowForwardIosOutlinedIcon />}
              onClick={openTestResults}
              sx={{ backgroundColor: theme.palette.info.light }}
            >
              Details
            </Button>
          </Grid>
        </Grid>
      </TestInformationWrapper>
    </PaperSection>
  );
};
