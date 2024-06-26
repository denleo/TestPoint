import React, { useCallback } from "react";

import ArrowForwardIosRoundedIcon from "@mui/icons-material/ArrowForwardIosRounded";
import DoneOutlineRoundedIcon from "@mui/icons-material/DoneOutlineRounded";
import ReplayRoundedIcon from "@mui/icons-material/ReplayRounded";
import { Button, IconButton, styled, Typography } from "@mui/material";
import { AxiosError } from "axios";
import { useNavigate } from "react-router-dom";

import { httpAction } from "@/api/httpAction";
import { NotificationType, useNotificationStore } from "@/components/NotificationProvider/useNotificationStore";
import { QuestionType } from "@/redux/adminData/state";

import { useResultsPageStore } from "../ResultsPage/useResultsPageStore";

import { QuestionComponent } from "./QuestionComponent";
import { TestPagination } from "./TestPagination";
import { useTestComponentStore } from "./useTestComponentStore";

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
  const notify = useNotificationStore((store) => store.notify);
  const testData = useTestComponentStore((store) => store.test);
  const questionIndex = useTestComponentStore((store) => store.questionIndex);
  const selectedAnswers = useTestComponentStore((store) => store.selectedAnswers);
  const setQuestionIndex = useTestComponentStore((store) => store.setQuestionIndex);
  const setSelectedAnswers = useTestComponentStore((store) => store.setSelectedAnswers);
  const setTestResult = useResultsPageStore((store) => store.setTest);

  const navigate = useNavigate();

  if (!testData || !testData?.questions) return null;

  const handleSelectAnswer = useCallback(
    (answer: string | string[], questionType: QuestionType) => {
      const map = new Map(selectedAnswers);

      if (questionType === QuestionType.TextSubstitution) {
        map.set(questionIndex, answer);
      }

      if (questionType === QuestionType.SingleOption) {
        const currentAnswer = map.get(questionIndex);
        if (!currentAnswer) {
          map.set(questionIndex, answer);
        } else if (currentAnswer === answer) {
          map.delete(questionIndex);
        } else {
          map.delete(questionIndex);
          map.set(questionIndex, answer);
        }
      }

      if (questionType === QuestionType.MultipleOptions) {
        map.set(questionIndex, answer);
      }

      setSelectedAnswers(map);
    },
    [questionIndex, selectedAnswers]
  );

  const handleGoNextQuestion = useCallback(() => {
    setQuestionIndex(questionIndex + 1);
  }, [questionIndex]);

  const handleGoPrevQuestion = useCallback(() => {
    setQuestionIndex(questionIndex - 1);
  }, [questionIndex]);

  const finishTest = useCallback(async () => {
    const history = testData.questions.map((question, index) => {
      const answersIds = selectedAnswers.get(index) as string[];
      const answers =
        question.questionType === QuestionType.TextSubstitution
          ? [answersIds]
          : question.answers.filter(({ id }) => answersIds.includes(id)).map(({ answerText }) => answerText);
      return { questionId: question.id, answers };
    });
    try {
      await httpAction(`tests/${testData.id}/results`, {
        testId: testData.id,
        completionTime: 20,
        history,
      });
      notify("You have successfully completed the test.", NotificationType.Success);
      navigate("/results");
      setTestResult(testData.id);
    } catch (error) {
      notify(error instanceof AxiosError ? error.message : "Failed to submit test.", NotificationType.Error);
    }
  }, [selectedAnswers, testData]);

  const numberQuestions = testData?.questions?.length ?? 0;
  const notFinished = selectedAnswers.size === numberQuestions;

  return (
    <LayoutPage>
      <TestContainer>
        <ButtonNext
          disabled={questionIndex === 0}
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
            testQuestion={testData.questions[questionIndex]}
            selectedAnswer={selectedAnswers.get(questionIndex)}
            onSelectAnswer={handleSelectAnswer}
          />
          <TestFooter>
            <ButtonReset color="error" variant="contained" startIcon={<ReplayRoundedIcon fontSize="large" />}>
              Reset Question
            </ButtonReset>
            <TestPagination
              numberQuestions={numberQuestions}
              selectedAnswers={selectedAnswers}
              selectedQuestionId={questionIndex}
              onChangeQuestionTab={setQuestionIndex}
            />
            <ButtonFinish
              disabled={!notFinished}
              color="success"
              variant="contained"
              endIcon={<DoneOutlineRoundedIcon fontSize="large" />}
              onClick={finishTest}
            >
              Finish Test
            </ButtonFinish>
          </TestFooter>
        </TestInner>
        <ButtonNext disabled={questionIndex === numberQuestions - 1} color="secondary" onClick={handleGoNextQuestion}>
          <ArrowForwardIosRoundedIcon fontSize="large" />
        </ButtonNext>
      </TestContainer>
    </LayoutPage>
  );
};

export default TestComponentPage;
