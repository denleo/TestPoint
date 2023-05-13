import React, { FC } from "react";

import CloseIcon from "@mui/icons-material/Close";
import { Dialog, DialogContent, DialogTitle, Grid, IconButton, Typography, useTheme } from "@mui/material";
import { Form, Formik } from "formik";

import { TextFieldFormik } from "@/components/TextFieldFormik";
import { QuestionType, QuestionVariant, TestQuestion } from "@/redux/adminData/state";

import { AnswerRow } from "./AnswerRow";
import { EditDialogActions, QuestionEditFormValues } from "./EditDialogActions";

interface Props {
  question: TestQuestion;
  onClose: () => void;
  onSubmit: (values: QuestionEditFormValues) => void;
}

export const QuestionEditFormDialog: FC<Props> = ({ question, onClose, onSubmit }) => {
  const theme = useTheme();

  return (
    <Formik
      initialValues={
        {
          questionText: question.questionText,
          answers: question.answers,
        } as QuestionEditFormValues
      }
      onSubmit={onSubmit}
    >
      {({ values, setFieldValue }) => (
        <Form>
          <Dialog
            open
            onClose={onClose}
            maxWidth="md"
            sx={{
              "& .MuiBackdrop-root": { opacity: "0!important" },
              ".MuiDialog-paper": { backgroundColor: theme.palette.common.white, height: 600 },
            }}
          >
            <DialogTitle sx={{ m: 0, p: 2 }}>
              <Typography display="inline-flex">Edit Question</Typography>
              <IconButton aria-label="close" onClick={onClose} sx={{ right: 8, top: 8, position: "absolute" }}>
                <CloseIcon />
              </IconButton>
            </DialogTitle>
            <DialogContent>
              <Grid container direction="column" spacing={1} sx={{ minWidth: 500 }}>
                <Grid item>
                  <Typography variant="caption">Question</Typography>
                  <TextFieldFormik
                    fullWidth
                    multiline
                    minRows={3}
                    size="small"
                    name="questionText"
                    color="secondary"
                    variant="outlined"
                    sx={{
                      minHeight: 71,
                      backgroundColor: theme.palette.common.white,
                    }}
                  />
                </Grid>
                {question.questionType === QuestionType.TextSubstitution && (
                  <Grid item>
                    <Typography variant="caption">Answer</Typography>
                    <TextFieldFormik
                      fullWidth
                      size="small"
                      name="answers"
                      color="secondary"
                      variant="outlined"
                      value={values.answers[0].answerText}
                      onChange={(text) =>
                        setFieldValue("answers", [{ ...values.answers[0], answerText: text } as QuestionVariant])
                      }
                      sx={{
                        minHeight: 71,
                      }}
                    />
                  </Grid>
                )}
                {question.questionType !== QuestionType.TextSubstitution && (
                  <Grid item sx={{ display: "flex", flexDirection: "column" }}>
                    <Typography variant="caption" display="block">
                      Answers
                    </Typography>
                    {values.answers.map((variant) => (
                      <AnswerRow variant={variant} questionType={question.questionType} />
                    ))}
                  </Grid>
                )}
              </Grid>
            </DialogContent>
            <EditDialogActions questionType={question.questionType} />
          </Dialog>
        </Form>
      )}
    </Formik>
  );
};
