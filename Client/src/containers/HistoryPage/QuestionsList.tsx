import React, { FC, useState } from "react";

import CancelOutlinedIcon from "@mui/icons-material/CancelOutlined";
import DoneOutlineIcon from "@mui/icons-material/DoneOutline";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Box,
  Checkbox,
  List,
  ListItem,
  Radio,
  styled,
  Typography,
} from "@mui/material";

import { QuestionType, TestQuestion } from "@/redux/adminData/state";

interface Props {
  questions: TestQuestion[];
}

const QuestionBlock = styled(Accordion)(({ theme }) => ({
  backgroundColor: theme.palette.common.white,
  padding: theme.spacing(1),
  alignItems: "center",
  flex: 1,
}));

export const QuestionList: FC<Props> = ({ questions }) => {
  const [expanded, setExpanded] = useState<string | false>(false);

  const handleChangeExpanded = (panel: string) => (event: React.SyntheticEvent, newExpanded: boolean) => {
    setExpanded(newExpanded ? panel : false);
  };

  return (
    <List>
      {questions.map(({ questionText, id, questionType, answers }, index) => (
        <ListItem key={id}>
          <QuestionBlock expanded={expanded === id} onChange={handleChangeExpanded(id)}>
            <AccordionSummary expandIcon={<ExpandMoreIcon fontSize="large" />} sx={{ overflow: "hidden" }}>
              {false ? <DoneOutlineIcon color="success" sx={{ mr: 1 }} /> : <CancelOutlinedIcon color="error" />}
              <Typography
                noWrap={expanded !== id}
                sx={{ width: expanded !== id ? 720 : undefined, textOverflow: "ellipsis" }}
              >
                {questionText}
              </Typography>
            </AccordionSummary>
            <AccordionDetails aria-controls={`${id}-content`} id={`${id}-header`} sx={{ pl: "82px" }}>
              {questionType === QuestionType.TextSubstitution ? (
                <>
                  <Typography variant="h6" display="inline-flex">
                    Answer: &nbsp;
                  </Typography>
                  <Typography display="inline-flex"> {answers[0].answerText}</Typography>
                </>
              ) : (
                answers.map((variant) => (
                  <div key={variant.id}>
                    {questionType === QuestionType.SingleOption && (
                      <Radio checked={variant.isCorrect} key={variant.id} disabled />
                    )}
                    {questionType === QuestionType.MultipleOptions && (
                      <Checkbox checked={variant.isCorrect} key={variant.id} disabled />
                    )}
                    <Typography display="inline-flex">{variant.answerText}</Typography>
                  </div>
                ))
              )}
            </AccordionDetails>
          </QuestionBlock>
        </ListItem>
      ))}
    </List>
  );
};
