import React, { FC, useCallback } from "react";

import { QuestionMarkRounded, PlayArrowRounded } from "@mui/icons-material";
import { Button, Chip, Grid, Paper, styled, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";

import { TestDifficultyChip } from "@/components/TestDifficultyChip";

import { TestData } from "./data";

const PaperSection = styled(Paper)(({ theme }) => ({
  minHeight: 200,
  maxWidth: 1200,
  display: "flex",
  flexDirection: "column",
  justifyContent: "space-between",
  padding: theme.spacing(2),
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
  testData: TestData;
}

export const TestPreviewCard: FC<Props> = ({ testData }) => {
  const { id, name, questions, completionTime } = { ...testData };
  const navigate = useNavigate();

  const handleStartTest = useCallback(() => {
    navigate("/test");
  }, []);

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
          <Typography variant="caption">#{id}</Typography>
        </Grid>
      </Grid>

      <TestInformationWrapper>
        <Grid container>
          <Grid item flexDirection="column" xs>
            <Typography variant="subtitle2">Test Information:</Typography>
            <div>
              <Dot />
              <Typography component="span" variant="body2">
                Number of questions: <strong>{questions?.length ?? 0}</strong>
              </Typography>
            </div>
            {/* TODO: use real author */}
            <div>
              <Dot />
              <Typography component="span" variant="body2">
                Author: <strong>{testData.author}</strong>
              </Typography>
            </div>
            <div>
              <Dot />
              <Typography component="span" variant="body2">
                Completion time: <strong>{completionTime}</strong>
              </Typography>
            </div>
            <div>
              <Dot />
              <Typography component="span" variant="body2">
                Difficulty: <TestDifficultyChip difficulty={testData.difficulty} />
              </Typography>
            </div>
          </Grid>
          <Grid item display="flex" alignItems="flex-end">
            <Button
              size="large"
              variant="contained"
              color="secondary"
              endIcon={<PlayArrowRounded />}
              onClick={handleStartTest}
            >
              Start
            </Button>
          </Grid>
        </Grid>
      </TestInformationWrapper>
    </PaperSection>
  );
};
