/* eslint-disable @typescript-eslint/no-shadow */
import React, { useState, useCallback } from "react";

import { DragDropContext, Droppable, Draggable, DropResult } from "@hello-pangea/dnd";
import AddIcon from "@mui/icons-material/Add";
import SaveIcon from "@mui/icons-material/Save";
import {
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

import { httpAction } from "@/api/httpAction";
import { NotificationType, useNotificationStore } from "@/components/NotificationProvider/useNotificationStore";
import { QuestionType, TestDifficulty, TestQuestion, TestData } from "@/redux/adminData/state";

import { useBreakpoint } from "@api/hooks/useBreakPoint";

import { generateUniqueId } from "../../api/generateId";
import { getInitQuestionVariants } from "../TestsPage/data";

import { QuestionEditFormValues } from "./Dialogs/EditDialogActions";
import { QuestionEditFormDialog } from "./Dialogs/QuestionEditFormDialog";
import { QuestionTypeDialog } from "./Dialogs/QuestionTypeDialog";
import { EmptyBlock } from "./EmptyBlock";
import { Question } from "./Question";
import { StartScreen } from "./StartScreen";
import { useTestBuilderStore } from "./useTestBuilderPageStore";

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

const TestBuilderPage = () => {
  const test = useTestBuilderStore((store) => store.test);
  const setTest = useTestBuilderStore((store) => store.setTest);
  const notify = useNotificationStore((store) => store.notify);
  const [expanded, setExpanded] = useState<string | false>(false);
  const [testName, setTestName] = useState<string>(test?.name ?? "Test Name");
  const [testDifficulty, setTestDifficulty] = useState<TestDifficulty>(test?.difficulty ?? TestDifficulty.Easy);
  const [start, setStart] = useState(!test);
  const [openQuestionDialog, setOpenQuestionDialog] = useState(false);
  const [questions, setQuestions] = useState<TestQuestion[]>(test?.questions ?? []);
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
        questionType,
        questionText: "Your question is here...",
        answers: getInitQuestionVariants(questionType),
      };
      setQuestions([...(questions ?? []), newQuestion]);
      setOpenQuestionDialog(false);
      setEditQuestion(newQuestion);
    },
    [questions]
  );

  const createTest = useCallback(() => {
    setTest(null);
    setStart(false);
  }, [setTest]);

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

  const handleClickCancel = useCallback(() => {
    setTest(null);
    setStart(true);
  }, [setTest]);

  const saveTest = useCallback(async () => {
    try {
      await httpAction("tests", {
        difficulty: testDifficulty,
        estimatedTime: 120,
        name: testName,
        questions,
      } as Omit<TestData, "authorId">);
      setTest(null);
      setStart(true);
      notify("Test has been created.", NotificationType.Success);
    } catch {
      notify("Failed to create test.", NotificationType.Error);
    }
  }, [test, testDifficulty, testName, questions, notify]);

  return start ? (
    <StartScreen onCreate={createTest} />
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
              sx={{ mr: 1 }}
              disableElevation
              startIcon={<SaveIcon sx={{ height: 24, width: 24 }} />}
              onClick={saveTest}
            >
              Save to system
            </Button>
            <Button variant="contained" color="error" disableElevation onClick={handleClickCancel}>
              Cancel
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
