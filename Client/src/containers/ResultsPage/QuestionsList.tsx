/* eslint-disable no-nested-ternary */
import React, { FC, useState, useMemo } from "react";

import CancelOutlinedIcon from "@mui/icons-material/CancelOutlined";
import CheckRoundedIcon from "@mui/icons-material/CheckRounded";
import CloseRoundedIcon from "@mui/icons-material/CloseRounded";
import DoneOutlineIcon from "@mui/icons-material/DoneOutline";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Checkbox,
  List,
  ListItem,
  Radio,
  styled,
  Typography,
  useTheme,
} from "@mui/material";

import { useBreakpoint } from "@/api/hooks/useBreakPoint";
import { QuestionType, TestQuestion } from "@/redux/adminData/state";

import { UserAnswer } from "./common";

interface Props {
  questions: TestQuestion[];
  history: UserAnswer[];
}

const QuestionBlock = styled(Accordion)(({ theme }) => ({
  backgroundColor: theme.palette.common.white,
  padding: theme.spacing(1),
  alignItems: "center",
  flex: 1,
}));

const isQuestionRight = (question: TestQuestion, answers: [string]) => {
  if (question.questionType === QuestionType.TextSubstitution) {
    return question.answers[0].answerText === answers[0];
  }

  const userAnswers = question.answers.map(
    ({ isCorrect, answerText }) =>
      (isCorrect && answers.includes(answerText)) || (!isCorrect && !answers.includes(answerText))
  );
  return !userAnswers.includes(false);
};

export const QuestionList: FC<Props> = ({ questions, history }) => {
  const [expanded, setExpanded] = useState<string | false>(false);
  const lgUp = useBreakpoint("lg");
  const mdUp = useBreakpoint("md");
  const theme = useTheme();

  const testResult = useMemo(() => {
    return questions.map((question) => {
      const userAnswers = history.filter((item) => item.questionId === question.id);
      const isCorrect = isQuestionRight(question, userAnswers[0].answers);

      return {
        question,
        userAnswer: userAnswers[0].answers,
        isCorrect,
      };
    });
  }, [questions, history]);

  const handleChangeExpanded = (panel: string) => (event: React.SyntheticEvent, newExpanded: boolean) => {
    setExpanded(newExpanded ? panel : false);
  };

  const questionLength = lgUp ? 1000 : mdUp ? 500 : 365;

  return (
    <List>
      {testResult.map(({ question, userAnswer, isCorrect }, index) => {
        const { questionText, id, questionType, answers } = question;
        return (
          <ListItem key={id}>
            <QuestionBlock expanded={expanded === id} onChange={handleChangeExpanded(id)}>
              <AccordionSummary
                expandIcon={<ExpandMoreIcon fontSize="large" />}
                sx={{ overflow: "hidden", width: "100%" }}
              >
                {isCorrect ? <DoneOutlineIcon color="success" sx={{ mr: 1 }} /> : <CancelOutlinedIcon color="error" />}
                <Typography
                  noWrap={expanded !== id}
                  sx={{ width: expanded !== id ? questionLength : undefined, textOverflow: "ellipsis" }}
                >
                  {questionText}
                </Typography>
              </AccordionSummary>
              <AccordionDetails aria-controls={`${id}-content`} id={`${id}-header`} sx={{ pl: "82px", width: "100%" }}>
                {questionType === QuestionType.TextSubstitution ? (
                  <>
                    <Typography variant="h6" display="inline-flex">
                      Answer: &nbsp;
                    </Typography>
                    <Typography> {answers[0].answerText}</Typography>
                    {!isCorrect && (
                      <>
                        <Typography variant="h6" display="inline-flex">
                          Your answer: &nbsp;
                        </Typography>
                        <Typography> {answers[0].answerText}</Typography>
                      </>
                    )}
                  </>
                ) : (
                  answers.map((variant) => {
                    const isUserCorrect = userAnswer.includes(variant.answerText);
                    return (
                      <div key={variant.id}>
                        {questionType === QuestionType.SingleOption && (
                          <Radio checked={variant.isCorrect || isUserCorrect} key={variant.id} disabled />
                        )}
                        {questionType === QuestionType.MultipleOptions && (
                          <Checkbox checked={variant.isCorrect} key={variant.id} disabled />
                        )}
                        <Typography display="inline-flex">{variant.answerText}</Typography>
                        {(variant.isCorrect || isUserCorrect) &&
                          (variant.isCorrect ? (
                            <CheckRoundedIcon fontSize="small" color="success" />
                          ) : (
                            <CloseRoundedIcon fontSize="small" color="error" />
                          ))}
                      </div>
                    );
                  })
                )}
              </AccordionDetails>
            </QuestionBlock>
          </ListItem>
        );
      })}
    </List>
  );
};
