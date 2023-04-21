import React, { ChangeEventHandler, FC, useCallback } from "react";

import { Box, Divider, List, Checkbox, ListItem, styled, Typography } from "@mui/material";

import { TestQuestion } from "../TestsPage/data";

const QuestionBlock = styled(Box)(({ theme }) => ({
  margin: theme.spacing(5, 2, 2, 2),
  display: "flex",
  // alignItems: "flex-end",
  height: 100,
}));

const VariantsList = styled(List)(({ theme }) => ({
  padding: theme.spacing(5),
}));

const QuestionVariant = styled(ListItem)(({ theme }) => ({
  display: "flex",
  alignItems: "center",
  cursor: "pointer",
  "&:hover": {
    backgroundColor: theme.palette.primary.light,
  },
}));

interface Props {
  testQuestion: TestQuestion;
  selectedAnswer?: number;
  onSelectAnswer(answerId: number): void;
}

export const QuestionComponent: FC<Props> = ({ testQuestion, selectedAnswer, onSelectAnswer }) => {
  const { question, variants } = { ...testQuestion };

  const handleChangeCheckbox = useCallback(
    (e: React.ChangeEvent<HTMLInputElement>, id: number) => {
      e.stopPropagation();
      onSelectAnswer(id);
    },
    [onSelectAnswer]
  );
  return (
    <div>
      <QuestionBlock>
        <Typography variant="h4">{question}</Typography>
      </QuestionBlock>
      <Divider variant="middle" />
      <VariantsList>
        {variants.map((variant) => (
          <QuestionVariant key={variant.id} onClick={() => onSelectAnswer(variant.id)}>
            <Checkbox checked={variant.id === selectedAnswer} onChange={(e) => handleChangeCheckbox(e, variant.id)} />
            <Typography ml={2} variant="body2">
              {variant.text}
            </Typography>
          </QuestionVariant>
        ))}
      </VariantsList>
    </div>
  );
};
