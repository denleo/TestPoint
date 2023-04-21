import React, { FC } from "react";

import { styled, Tab, TabProps, Tabs } from "@mui/material";

const PaginationTabs = styled(Tabs)(({ theme }) => ({
  padding: theme.spacing(2),
  alignSelf: "center",
}));

const QuestionTab = styled(Tab, {
  shouldForwardProp: (prop) => prop !== "answered",
})<TabProps & { answered?: boolean }>(({ theme, answered }) => ({
  margin: theme.spacing(0, 0.3),
  border: "2px solid transparent",
  "&.Mui-selected": {
    backgroundColor: theme.palette.primary.light,
    color: theme.palette.secondary.main,
  },
  ...(answered && {
    // backgroundColor: theme.palette.success.main,
    border: `2px solid ${theme.palette.success.main}`,
  }),
}));

interface Props {
  selectedAnswers: Map<number, number>;
  numberQuestions: number;
  selectedQuestionId: number;
  onChangeQuestionTab(value: number): void;
}

export const TestPagination: FC<Props> = ({
  selectedAnswers,
  numberQuestions,
  selectedQuestionId,
  onChangeQuestionTab,
}) => {
  return (
    <PaginationTabs value={selectedQuestionId} onChange={(e, value) => onChangeQuestionTab(value)}>
      {Array(numberQuestions)
        .fill(5)
        .map((e, index) => (
          <QuestionTab answered={selectedAnswers.has(index)} key={`key=${e + index}`} value={index} label={index + 1} />
        ))}
    </PaginationTabs>
  );
};
