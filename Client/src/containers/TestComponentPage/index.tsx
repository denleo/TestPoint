import React, { useState, useCallback } from "react";

import ArrowForwardIosRoundedIcon from "@mui/icons-material/ArrowForwardIosRounded";
import DoneOutlineRoundedIcon from "@mui/icons-material/DoneOutlineRounded";
import ReplayRoundedIcon from "@mui/icons-material/ReplayRounded";
import { Button, IconButton, styled, Typography } from "@mui/material";

import { TestData, TEST_DATA_1 } from "../TestsPage/data";

import { QuestionComponent } from "./QuestionComponent";
import { TestPagination } from "./TestPagination";

const LayoutPage = styled("div")(({ theme }) => ({
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
  width: "100%",
  height: "100%",
}));

const TestContainer = styled("div")(({ theme }) => ({
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
  height: "100%",
  maxHeight: 800,
  flexGrow: 1,
}));

const TestInner = styled("div")(({ theme }) => ({
  maxWidth: 1000,
  height: "100%",
  display: "flex",
  flexDirection: "column",
  justifyContent: "space-between",
  borderTop: `8px solid ${theme.palette.secondary.light}`,
  borderRadius: theme.spacing(4),
  backgroundColor: theme.palette.common.white,
  flexGrow: 1,
  flexBasis: "100%",
}));

const TestHeader = styled("div")(({ theme }) => ({
  padding: theme.spacing(1, 3),
  borderRadius: theme.spacing(3, 3, 0, 0),
  backgroundColor: theme.palette.secondary.light,
  color: theme.palette.common.white,
}));

const TestFooter = styled("div")(({ theme }) => ({
  display: "flex",
  height: 60,
  justifyContent: "space-between",
}));

const ButtonNext = styled(IconButton)(({ theme }) => ({
  height: "90%",
  width: 100,
  borderRadius: theme.spacing(1),
  margin: theme.spacing(2),
  "&:disabled": {
    opacity: 0,
  },
}));

const ButtonReset = styled(Button)(({ theme }) => ({
  borderRadius: theme.spacing(0, 0, 0, 4),
  width: 200,
  backgroundColor: theme.palette.secondary.dark,
}));

const ButtonFinish = styled(Button)(({ theme }) => ({
  borderRadius: theme.spacing(0, 0, 4, 0),
  color: theme.palette.common.white,
  width: 200,
  backgroundColor: theme.palette.secondary.dark,
}));

const TestComponentPage = () => {
  const [testData, setTestData] = useState<TestData>(TEST_DATA_1);
  const [selectedQuestionId, setSelectedQuestionId] = useState<number>(0);
  const [selectedAnswers, setSelectedAnswers] = useState<Map<number, number>>(new Map());

  const handleSelectAnswer = useCallback(
    (answerId: number) => {
      const map = new Map(selectedAnswers);
      const answer = map.get(selectedQuestionId);
      if (!answer) {
        map.set(selectedQuestionId, answerId);
      } else if (answer === answerId) {
        map.delete(selectedQuestionId);
      } else {
        map.delete(selectedQuestionId);
        map.set(selectedQuestionId, answerId);
      }

      setSelectedAnswers(map);
    },
    [selectedQuestionId, selectedAnswers]
  );

  const handleGoNextQuestion = useCallback(() => {
    setSelectedQuestionId(selectedQuestionId + 1);
  }, [selectedQuestionId]);

  const handleGoPrevQuestion = useCallback(() => {
    setSelectedQuestionId(selectedQuestionId - 1);
  }, [selectedQuestionId]);

  const numberQuestions = testData?.questions?.length ?? 0;
  const notFinished = selectedAnswers.size === numberQuestions;

  if (!testData.questions) return null;

  return (
    <LayoutPage>
      <TestContainer>
        <ButtonNext
          disabled={selectedQuestionId === 0}
          color="secondary"
          sx={{ transform: "rotate(180deg)" }}
          onClick={handleGoPrevQuestion}
        >
          <ArrowForwardIosRoundedIcon fontSize="large" />
        </ButtonNext>
        <TestInner>
          <TestHeader>
            <Typography mr={0.5} display="inline-block" variant="h4">
              TEST: {testData.name}
            </Typography>
            <Typography variant="caption">#{testData.id}</Typography>
          </TestHeader>
          <QuestionComponent
            testQuestion={testData.questions[selectedQuestionId]}
            selectedAnswer={selectedAnswers.get(selectedQuestionId)}
            onSelectAnswer={handleSelectAnswer}
          />
          <TestFooter>
            <ButtonReset color="error" variant="contained" startIcon={<ReplayRoundedIcon fontSize="large" />}>
              Reset Question
            </ButtonReset>
            <TestPagination
              numberQuestions={numberQuestions}
              selectedAnswers={selectedAnswers}
              selectedQuestionId={selectedQuestionId}
              onChangeQuestionTab={setSelectedQuestionId}
            />
            <ButtonFinish
              disabled={!notFinished}
              color="success"
              variant="contained"
              endIcon={<DoneOutlineRoundedIcon fontSize="large" />}
            >
              Finish Test
            </ButtonFinish>
          </TestFooter>
        </TestInner>
        <ButtonNext
          disabled={selectedQuestionId === numberQuestions - 1}
          color="secondary"
          onClick={handleGoNextQuestion}
        >
          <ArrowForwardIosRoundedIcon fontSize="large" />
        </ButtonNext>
      </TestContainer>
    </LayoutPage>
  );
};

export default TestComponentPage;
