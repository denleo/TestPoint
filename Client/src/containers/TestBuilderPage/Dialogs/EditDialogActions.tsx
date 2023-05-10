import React, { FC, useCallback } from "react";

import AddIcon from "@mui/icons-material/Add";
import SaveIcon from "@mui/icons-material/Save";
import { Button, DialogActions } from "@mui/material";
import { useFormikContext } from "formik";

import { generateUniqueId } from "@/api/generateId";
import { QuestionType, QuestionVariant } from "@/redux/adminData/state";

interface Props {
  questionType: QuestionType;
}

export interface QuestionEditFormValues {
  questionText: string;
  answers: QuestionVariant[];
}

export const EditDialogActions: FC<Props> = ({ questionType }) => {
  const { setFieldValue, values, submitForm } = useFormikContext<QuestionEditFormValues>();

  const handleAddAnswer = useCallback(() => {
    const newVariants = [
      ...values.answers,
      {
        id: generateUniqueId(),
        text: `answer ${values.answers.length}`,
        isCorrect: false,
      },
    ];
    setFieldValue("answers", newVariants);
  }, [values.answers]);

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
