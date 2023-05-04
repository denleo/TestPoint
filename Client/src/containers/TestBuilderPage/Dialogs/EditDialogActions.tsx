import React, { FC, useCallback } from "react";

import AddIcon from "@mui/icons-material/Add";
import SaveIcon from "@mui/icons-material/Save";
import { Button, DialogActions } from "@mui/material";
import { useFormikContext } from "formik";

import { generateUniqueId } from "@/api/generateId";

import { QuestionType, QuestionVariant } from "../../TestsPage/data";

interface Props {
  questionType: QuestionType;
}

export interface QuestionEditFormValues {
  question: string;
  variants: QuestionVariant[];
}

export const EditDialogActions: FC<Props> = ({ questionType }) => {
  const { setFieldValue, values, submitForm } = useFormikContext<QuestionEditFormValues>();

  const handleAddAnswer = useCallback(() => {
    const newVariants = [
      ...values.variants,
      {
        id: generateUniqueId(),
        text: `answer ${values.variants.length}`,
        isCorrect: false,
      },
    ];
    setFieldValue("variants", newVariants);
  }, [values.variants]);

  const isTextQuestion = questionType === QuestionType.TextSubstitution;

  return (
    <DialogActions
      sx={{
        display: "flex",
        justifyContent: !isTextQuestion ? "space-between" : "flex-end",
        gap: 20,
      }}
    >
      {!isTextQuestion && (
        <Button variant="contained" color="secondary" fullWidth onClick={handleAddAnswer} startIcon={<AddIcon />}>
          Add answer
        </Button>
      )}
      <Button variant="contained" color="secondary" fullWidth onClick={submitForm} startIcon={<SaveIcon />}>
        Save
      </Button>
    </DialogActions>
  );
};
