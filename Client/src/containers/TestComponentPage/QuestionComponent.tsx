import React, { FC, useCallback } from "react";

import { Box, Divider, List, Checkbox, ListItem, styled, Typography, Radio, TextField } from "@mui/material";

import { TestQuestion, QuestionType } from "../TestsPage/data";

const QuestionBlock = styled(Box)(({ theme }) => ({
  margin: theme.spacing(5, 2, 2, 2),
  display: "flex",
  // alignItems: "flex-end",
  height: 200,
}));

const VariantsList = styled(List)(({ theme }) => ({
  padding: theme.spacing(0, 5),
  margin: theme.spacing(2, 0),
  flex: 1,
  overflow: "auto",
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
  selectedAnswer?: string | string[];
  onSelectAnswer(answer: string | string[], questionType: QuestionType): void;
}

export const QuestionComponent: FC<Props> = ({ testQuestion, selectedAnswer, onSelectAnswer }) => {
  const { question, variants, type } = { ...testQuestion };

  const handleChangeSingleOption = useCallback(
    (e: React.ChangeEvent<HTMLInputElement>, id: string) => {
      e.stopPropagation();
      onSelectAnswer(id, type);
    },
    [onSelectAnswer, type]
  );

  const handleChangeMultipleOption = useCallback(
    (id: string, e?: React.ChangeEvent<HTMLInputElement>) => {
      e?.stopPropagation();
      const answers = selectedAnswer ? (selectedAnswer as string[]) : new Array<string>();
      if (answers?.includes(id)) {
        onSelectAnswer(
          answers.filter((answer) => answer !== id),
          type
        );
      } else {
        onSelectAnswer([...answers, id], type);
      }
    },
    [onSelectAnswer, selectedAnswer, type]
  );

  return (
    <>
      <QuestionBlock>
        <Typography variant="h4">{question}</Typography>
      </QuestionBlock>
      <Divider variant="middle" />
      {type === QuestionType.SingleOption && (
        <VariantsList>
          {variants.map((variant) => (
            <QuestionVariant key={variant.id} onClick={() => onSelectAnswer(variant.id, type)}>
              <Radio
                checked={variant.id === selectedAnswer}
                onChange={(e) => handleChangeSingleOption(e, variant.id)}
              />
              <Typography ml={2} variant="body2">
                {variant.text}
              </Typography>
            </QuestionVariant>
          ))}
        </VariantsList>
      )}
      {type === QuestionType.MultipleOptions && (
        <VariantsList>
          {variants.map((variant) => (
            <QuestionVariant key={variant.id} onClick={() => handleChangeMultipleOption(variant.id)}>
              <Checkbox
                checked={selectedAnswer ? (selectedAnswer as string[]).includes(variant.id) : false}
                onChange={(e) => handleChangeMultipleOption(variant.id, e)}
              />
              <Typography ml={2} variant="body2">
                {variant.text}
              </Typography>
            </QuestionVariant>
          ))}
        </VariantsList>
      )}
      {type === QuestionType.TextSubstitution && (
        <Box p={5} flex={1}>
          <Typography>Your answer:</Typography>
          <TextField
            value={selectedAnswer}
            focused
            autoFocus
            variant="standard"
            onChange={(e) => onSelectAnswer(e.target.value, type)}
            sx={{ minWidth: 400 }}
          />
        </Box>
      )}
    </>
  );
};
