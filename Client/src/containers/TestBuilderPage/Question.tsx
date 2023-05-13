import React, { FC, useCallback, MouseEvent } from "react";

import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import MenuOutlinedIcon from "@mui/icons-material/MenuOutlined";
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  IconButton,
  Radio,
  styled,
  Typography,
  Checkbox,
} from "@mui/material";

import { QuestionIcon } from "@/components/QuestionIcon";
import { QuestionType, TestQuestion } from "@/redux/adminData/state";

const QuestionBlock = styled(Accordion)(({ theme }) => ({
  border: `2px solid ${theme.palette.divider}`,
  backgroundColor: theme.palette.common.white,
  borderRadius: theme.spacing(1),
  padding: theme.spacing(1),
  alignItems: "center",
  flex: 1,
  marginLeft: theme.spacing(1),
}));

interface Props {
  question: TestQuestion;
  expanded: string | false;
  onEdit: (question: TestQuestion) => void;
  onDelete: (question: TestQuestion) => void;
  setExpanded: (panel: string | false) => void;
}

export const Question: FC<Props> = ({ question, expanded, setExpanded, onEdit, onDelete }) => {
  const handleChangeExpanded = (panel: string) => (event: React.SyntheticEvent, newExpanded: boolean) => {
    setExpanded(newExpanded ? panel : false);
  };

  const handleEditQuestion = useCallback(
    (e: MouseEvent) => {
      e.preventDefault();
      e.stopPropagation();
      onEdit(question);
    },
    [onEdit, question]
  );

  const handleDeleteQuestion = useCallback(
    (e: MouseEvent) => {
      e.preventDefault();
      e.stopPropagation();
      onDelete(question);
    },
    [onEdit, question]
  );

  return (
    <>
      <IconButton>
        <MenuOutlinedIcon />
      </IconButton>
      <QuestionBlock expanded={expanded === question.id} onChange={handleChangeExpanded(question.id)}>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon fontSize="large" />}
          sx={{ width: "100%", maxHeight: 100, overflow: "hidden" }}
        >
          <QuestionIcon
            questionType={question.questionType}
            sx={{ width: 50, height: 50, mr: 2, alignSelf: "center" }}
          />
          <Typography>{question.questionText}</Typography>
          <IconButton sx={{ ml: "auto", width: 50, height: 50 }} onClick={handleEditQuestion}>
            <EditIcon />
          </IconButton>
          <IconButton sx={{ width: 50, height: 50 }} onClick={handleDeleteQuestion}>
            <DeleteIcon />
          </IconButton>
        </AccordionSummary>
        <AccordionDetails aria-controls={`${question.id}-content`} id={`${question.id}-header`} sx={{ pl: "82px" }}>
          {question.questionType === QuestionType.TextSubstitution ? (
            <>
              <Typography variant="h6" display="inline-flex">
                Answer: &nbsp;
              </Typography>
              <Typography display="inline-flex"> {question.answers[0].answerText}</Typography>
            </>
          ) : (
            question.answers.map((variant) => (
              <div key={variant.id}>
                {question.questionType === QuestionType.SingleOption && (
                  <Radio checked={variant.isCorrect} key={variant.id} disabled />
                )}
                {question.questionType === QuestionType.MultipleOptions && (
                  <Checkbox checked={variant.isCorrect} key={variant.id} disabled />
                )}
                <Typography display="inline-flex">{variant.answerText}</Typography>
              </div>
            ))
          )}
        </AccordionDetails>
      </QuestionBlock>
    </>
  );
};
