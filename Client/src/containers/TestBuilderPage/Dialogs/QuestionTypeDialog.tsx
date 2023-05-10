import React, { FC } from "react";

import { Dialog, List, ListItemButton, Typography, useTheme } from "@mui/material";

import { QuestionIcon } from "@/components/QuestionIcon";
import { QuestionType } from "@/redux/adminData/state";

interface Props {
  onChooseQuestion: (type: QuestionType) => void;
  open: boolean;
  onClose: () => void;
}

export const QuestionTypeDialog: FC<Props> = ({ onChooseQuestion, open, onClose }) => {
  const theme = useTheme();
  return (
    <Dialog open={open} onClose={onClose} maxWidth="md" sx={{ "& .MuiBackdrop-root": { opacity: "0!important" } }}>
      <List disablePadding>
        <ListItemButton
          sx={{ justifyContent: "space-around", gap: 3, p: 2 }}
          onClick={() => onChooseQuestion(QuestionType.SingleOption)}
        >
          <QuestionIcon questionType={QuestionType.SingleOption} sx={{ width: 50, height: 50 }} />
          <div>
            <Typography variant="h6" gutterBottom>
              Single option question
            </Typography>
            <Typography>
              Type of survey format where participants are given only one answer option to a question.
            </Typography>
          </div>
        </ListItemButton>
        <ListItemButton
          sx={{ justifyContent: "space-around", gap: 3, p: 2 }}
          onClick={() => onChooseQuestion(QuestionType.MultipleOptions)}
        >
          <QuestionIcon questionType={QuestionType.MultipleOptions} sx={{ width: 50, height: 50 }} />
          <div>
            <Typography variant="h6" gutterBottom>
              Multiple options question
            </Typography>
            <Typography>Survey format where participants are given several answer options to a question.</Typography>
          </div>
        </ListItemButton>
        <ListItemButton
          sx={{ justifyContent: "space-around", gap: 3, p: 2 }}
          onClick={() => onChooseQuestion(QuestionType.TextSubstitution)}
        >
          <QuestionIcon questionType={QuestionType.TextSubstitution} sx={{ width: 50, height: 50 }} />
          <div>
            <Typography variant="h6">Text substitution question</Typography>
            <Typography>
              Survey format where participants are asked to replace a specific word or phrase in a sentence or paragraph
              with another word or phrase of their choice.
            </Typography>
          </div>
        </ListItemButton>
      </List>
    </Dialog>
  );
};
