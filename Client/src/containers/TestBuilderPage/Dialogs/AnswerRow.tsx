import React, { FC, useCallback } from "react";

import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import { Checkbox, Grid, IconButton, Radio } from "@mui/material";
import { useFormikContext } from "formik";

import { TextFieldFormik } from "@/components/TextFieldFormik";

import { QuestionVariant, QuestionType } from "@redux/adminData/state";

import { QuestionEditFormValues } from "./EditDialogActions";

interface Props {
  variant: QuestionVariant;
  questionType: QuestionType;
}

export const AnswerRow: FC<Props> = ({ variant, questionType }) => {
  const { setFieldValue, values } = useFormikContext<QuestionEditFormValues>();

  const handleSelectCheckBox = useCallback(() => {
    const newVariants = values.answers.map((item) =>
      item.id === variant.id ? { ...item, isCorrect: !item.isCorrect } : item
    );
    setFieldValue("answers", newVariants);
  }, [values.answers, setFieldValue, variant]);

  const handleSelectRadio = useCallback(() => {
    const newVariants = values.answers.map((item) =>
      item.id === variant.id ? { ...item, isCorrect: true } : { ...item, isCorrect: false }
    );
    setFieldValue("answers", newVariants);
  }, [values.answers, setFieldValue, variant]);

  const handleDeleteAnswer = useCallback(() => {
    const newVariants = values.answers.filter((item) => item.id !== variant.id);
    setFieldValue("answers", newVariants);
  }, [values.answers, setFieldValue, variant]);

  const handleEditAnswer = useCallback(
    (text?: string) => {
      const newVariants = values.answers.map((item) =>
        item.id === variant.id ? { ...item, answerText: text ?? "" } : item
      );
      setFieldValue("answers", newVariants);
    },
    [values.answers, setFieldValue, variant]
  );

  return (
    <Grid container>
      <Grid item>
        {questionType === QuestionType.MultipleOptions && (
          <Checkbox key={variant.id} checked={variant.isCorrect} color="secondary" onChange={handleSelectCheckBox} />
        )}
        {questionType === QuestionType.SingleOption && (
          <Radio key={variant.id} checked={variant.isCorrect} color="secondary" onChange={handleSelectRadio} />
        )}
      </Grid>
      <Grid item xs>
        <TextFieldFormik
          fullWidth
          size="small"
          name="answers"
          color="secondary"
          variant="outlined"
          value={variant.answerText}
          onChange={handleEditAnswer}
          sx={{
            minHeight: 71,
          }}
        />
      </Grid>
      <Grid item>
        <IconButton color="error" onClick={handleDeleteAnswer}>
          <DeleteForeverIcon />
        </IconButton>
      </Grid>
    </Grid>
  );
};
