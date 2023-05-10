import React from "react";

import { alpha, Box, Grid, Paper, Typography, useTheme } from "@mui/material";

import { ProgressScore } from "@/components/ProgressScore";
import { TestDifficultyChip } from "@/components/TestDifficultyChip";
import { TestDifficulty } from "@/redux/adminData/state";

import { TEST_DATA_1 } from "../TestsPage/data";

import { QuestionList } from "./QuestionsList";

const score = 8.9;
const testName = "Тестовое тестирование знаний";
const testDifficulty = TestDifficulty.Hard;
const testAuthor = "Maxim Rojkov";
const completionTime = 120;
const result = "89/100";

const HistoryPage = () => {
  const theme = useTheme();
  return (
    <Box display="flex" width="100%" height="100%" flexDirection="column" alignItems="center">
      <Grid
        container
        sx={{ maxWidth: 1280, backgroundColor: alpha(theme.palette.info.light, 0.32), p: 4, borderRadius: 4 }}
      >
        <Grid item display="flex" justifyContent="space-between" xs={12}>
          <Typography variant="h4">
            Test: <strong>{testName}</strong>
          </Typography>
          <TestDifficultyChip difficulty={testDifficulty} />
        </Grid>
        <Grid item xs={6} sx={{ mt: 5 }}>
          <Typography>
            Author: <strong>{testAuthor}</strong>
          </Typography>
          <Typography>
            Completion time: <strong>{completionTime}</strong>
          </Typography>
          <Typography>
            Result: <strong>{result}</strong>
          </Typography>
        </Grid>
        <Grid item xs={6} display="flex" alignItems="center" justifyContent="flex-end" sx={{ mt: 5 }}>
          <ProgressScore percent={score * 10} label={score.toString()} />
        </Grid>
      </Grid>
      <Box width={1280}>
        <QuestionList questions={TEST_DATA_1.questions} />
      </Box>
    </Box>
  );
};

export default HistoryPage;
