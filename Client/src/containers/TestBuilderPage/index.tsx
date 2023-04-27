/* eslint-disable @typescript-eslint/no-shadow */
import React, { useState, useCallback, FC } from "react";

import { DragDropContext, Droppable, Draggable, DropResult } from "@hello-pangea/dnd";
import AddIcon from "@mui/icons-material/Add";
import SaveIcon from "@mui/icons-material/Save";
import {
  Box,
  Button,
  List,
  ListItem,
  MenuItem,
  Paper,
  Select,
  SelectChangeEvent,
  styled,
  TextField,
  useTheme,
} from "@mui/material";

import { useBreakpoint } from "@api/hooks/useBreakPoint";

import { generateUniqueId } from "../../api/generateId";
import { QuestionType, TestData, TestQuestion, getInitQuestionVariants, TestDifficulty } from "../TestsPage/data";

import { QuestionEditFormValues } from "./Dialogs/EditDialogActions";
import { QuestionEditFormDialog } from "./Dialogs/QuestionEditFormDialog";
import { QuestionTypeDialog } from "./Dialogs/QuestionTypeDialog";
import { EmptyBlock } from "./EmptyBlock";
import { Question } from "./Question";
import { StartScreen } from "./StartScreen";

interface Props {
  test: TestData;
}

const LayoutPage = styled("div")(({ theme }) => ({
  display: "flex",
  flexDirection: "column",
  width: "100%",
  height: "100%",
}));

const QuestionBlock = styled(ListItem)(({ theme }) => ({
  width: "100%",
  cursor: "pointer",
  marginBottom: theme.spacing(1),
}));

const reorder = (questions: TestQuestion[], startIndex: number, endIndex: number) => {
  const result = [...questions];
  const [removed] = result.splice(startIndex, 1);
  result.splice(endIndex, 0, removed);

  return result;
};

const TestBuilderPage: FC<Props> = ({ test }) => {
  const [expanded, setExpanded] = useState<string | false>(false);
  const [testName, setTestName] = useState<string>(test.name);
  const [testDifficulty, setTestDifficulty] = useState<TestDifficulty>(test.difficulty);
  const [start, setStart] = useState(false);
  const [openQuestionDialog, setOpenQuestionDialog] = useState(false);
  const [questions, setQuestions] = useState<TestQuestion[]>();
  const [editQuestion, setEditQuestion] = useState<TestQuestion | false>(false);

  const theme = useTheme();
  const mdUp = useBreakpoint("md");

  const handleDragEnd = useCallback(
    (result: DropResult) => {
      if (!result.destination || !questions) {
        return;
      }

      const items = reorder(questions, result.source.index, result.destination.index);
      setQuestions(items);
    },
    [questions]
  );

  const createNewQuestion = useCallback(
    (questionType: QuestionType) => {
      const newQuestion: TestQuestion = {
        id: generateUniqueId(),
        type: questionType,
        question: "Your question is here...",
        variants: getInitQuestionVariants(questionType),
      };
      setQuestions([...(questions ?? []), newQuestion]);
      setOpenQuestionDialog(false);
      setEditQuestion(newQuestion);
    },
    [questions]
  );

  const saveQuestion = useCallback(
    (values: QuestionEditFormValues) => {
      if (!editQuestion) return;
      const newQuestions = questions?.map((item) =>
        item.id === editQuestion.id
          ? {
              ...editQuestion,
              ...values,
            }
          : item
      );
      setQuestions(newQuestions);
      setEditQuestion(false);
    },
    [editQuestion, setEditQuestion, setQuestions, questions]
  );

  const selectDifficulty = useCallback((event: SelectChangeEvent<TestDifficulty>) => {
    setTestDifficulty(event.target.value as TestDifficulty);
  }, []);

  return start ? (
    <StartScreen onCreate={() => setStart(false)} />
  ) : (
    <>
      <LayoutPage>
        <Paper
          sx={{
            width: "100%",
            display: "flex",
            padding: theme.spacing(1),
            justifyContent: "space-between",
            alignItems: "center",
          }}
        >
          <div>
            <TextField
              variant="outlined"
              value={testName}
              label="Test Name"
              sx={{ minWidth: 350 }}
              onChange={(e) => setTestName(e.target.value)}
            />
            <Select
              value={testDifficulty}
              onChange={selectDifficulty}
              sx={{ "& .MuiBackdrop-root": { opacity: "0!important" }, ml: 1, width: 120 }}
            >
              <MenuItem value={TestDifficulty.Easy}>Easy</MenuItem>
              <MenuItem value={TestDifficulty.Medium}>Medium</MenuItem>
              <MenuItem value={TestDifficulty.Hard}>Hard</MenuItem>
            </Select>
          </div>
          <div>
            <Button
              variant="contained"
              disableElevation
              sx={{ mr: 1 }}
              startIcon={<AddIcon sx={{ height: 24, width: 24 }} />}
              onClick={() => setOpenQuestionDialog(true)}
            >
              Add new question
            </Button>
            <Button
              variant="contained"
              color="secondary"
              disableElevation
              startIcon={<SaveIcon sx={{ height: 24, width: 24 }} />}
            >
              Save to system
            </Button>
          </div>
        </Paper>
        {!questions?.length ? (
          <EmptyBlock />
        ) : (
          <DragDropContext onDragEnd={handleDragEnd}>
            <Droppable droppableId="droppable">
              {(provided, snapshot) => (
                <List {...provided.droppableProps} ref={provided.innerRef} sx={{ p: theme.spacing(3, 0) }}>
                  {questions.map((question, index) => (
                    <Draggable key={question.id} draggableId={question.id} index={index}>
                      {(provided, snapshot) => (
                        <QuestionBlock
                          ref={provided.innerRef}
                          {...provided.draggableProps}
                          {...provided.dragHandleProps}
                        >
                          <Question
                            setExpanded={setExpanded}
                            expanded={expanded}
                            question={question}
                            onEdit={setEditQuestion}
                          />
                        </QuestionBlock>
                      )}
                    </Draggable>
                  ))}
                  {provided.placeholder}
                </List>
              )}
            </Droppable>
          </DragDropContext>
        )}
      </LayoutPage>
      <QuestionTypeDialog
        onChooseQuestion={createNewQuestion}
        open={openQuestionDialog}
        onClose={() => setOpenQuestionDialog(false)}
      />
      {editQuestion && (
        <QuestionEditFormDialog
          question={editQuestion}
          onClose={() => setEditQuestion(false)}
          onSubmit={saveQuestion}
        />
      )}
    </>
  );
};

export default TestBuilderPage;
