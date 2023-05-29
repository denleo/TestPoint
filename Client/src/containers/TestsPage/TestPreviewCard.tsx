import React, { FC, useCallback, useState } from "react";

import { QuestionMarkRounded, PlayArrowRounded } from "@mui/icons-material";
import ContentCopyIcon from "@mui/icons-material/ContentCopy";
import DeleteIcon from "@mui/icons-material/Delete";
import EqualizerIcon from "@mui/icons-material/Equalizer";
import PersonAddAlt1Icon from "@mui/icons-material/PersonAddAlt1";
import { Button, Grid, Paper, styled, Typography } from "@mui/material";
import { AxiosError } from "axios";
import { useLocation, useNavigate } from "react-router-dom";

import { useBreakpoint } from "@/api/hooks/useBreakPoint";
import { httpAction } from "@/api/httpAction";
import { AssignDialog } from "@/components/AssignDialog";
import { ConfirmDialog } from "@/components/ConfirmDialog";
import { useNotificationStore, NotificationType } from "@/components/NotificationProvider/useNotificationStore";
import { TestDifficultyChip } from "@/components/TestDifficultyChip";
import { TestData, TestInfo } from "@/redux/adminData/state";
import { useSelector } from "@/redux/hooks";
import { isAdminSelector } from "@/redux/selectors";

import { useTestBuilderStore } from "../TestBuilderPage/useTestBuilderPageStore";
import { useTestComponentStore } from "../TestComponentPage/useTestComponentStore";

const PaperSection = styled(Paper)(({ theme }) => ({
  minHeight: 200,
  display: "flex",
  flexDirection: "column",
  justifyContent: "space-between",
  padding: theme.spacing(2),
  width: "100%",
  maxWidth: 960,
}));

const QuestionMarkWrapper = styled("div")(({ theme }) => ({
  borderRadius: "50%",
  padding: theme.spacing(0.5),
  backgroundColor: theme.palette.secondary.main,
  color: theme.palette.common.white,
}));

const TestInformationWrapper = styled("div")(({ theme }) => ({
  padding: theme.spacing(1, 4),
  flex: 1,
}));

const Dot = styled("p")(({ theme }) => ({
  borderRadius: "50%",
  display: "inline-flex",
  width: 7,
  height: 7,
  margin: theme.spacing(0, 1, 0, 2),
  backgroundColor: theme.palette.secondary.dark,
}));

interface Props {
  testData: TestInfo;
  rerender: () => void;
}

export const TestPreviewCard: FC<Props> = ({ testData, rerender }) => {
  const { name, questionCount, estimatedTime, author } = { ...testData };
  const isAdmin = useSelector(isAdminSelector);
  const [openAssign, setOpenAssign] = useState(false);
  const [openConfirmDelete, setOpenConfirmDelete] = useState(false);
  const navigate = useNavigate();
  const location = useLocation();
  const mdUp = useBreakpoint("md");

  const setEditTest = useTestBuilderStore((store) => store.setTest);
  const setTest = useTestComponentStore((store) => store.setTest);
  const notify = useNotificationStore((store) => store.notify);

  const handleStartTest = useCallback(async () => {
    try {
      const test = await httpAction(`tests/${testData.id}`);
      navigate("/test");
      setTest(test as TestData);
    } catch (error) {
      notify(
        error instanceof AxiosError ? error.message : "An error occurred while loading test",
        NotificationType.Error
      );
    }
  }, [testData]);

  const toggleAssignDialog = useCallback(() => {
    setOpenAssign(!openAssign);
  }, [openAssign]);

  const editTest = useCallback(async () => {
    try {
      const test = (await httpAction(`tests/${testData.id}`)) as TestData;
      navigate("/constructor", { state: { from: location } });
      setEditTest({ ...test, name: `${test.name} [copy]` });
    } catch {
      notify("Failed to open editor for this test.", NotificationType.Error);
    }
  }, [testData, notify]);

  const onClickDelete = useCallback(() => {
    setOpenConfirmDelete(true);
  }, []);

  const deleteTest = useCallback(async () => {
    try {
      await httpAction(`tests/${testData.id}`, undefined, "DELETE");
      rerender();
    } catch (error) {
      notify(error instanceof AxiosError ? error.message : "Failed to delete test", NotificationType.Error);
    }
    setOpenConfirmDelete(false);
  }, [notify, testData.id, rerender]);

  return (
    <>
      <PaperSection>
        <Grid container alignItems="center" spacing={1}>
          <Grid item>
            <QuestionMarkWrapper>
              <QuestionMarkRounded fontSize="medium" />
            </QuestionMarkWrapper>
          </Grid>
          <Grid item xs>
            <Typography mr={0.5} display="inline-block" variant="h4">
              TEST: {name}
            </Typography>
          </Grid>
          <Grid item>
            <TestDifficultyChip difficulty={testData.difficulty} />
          </Grid>
        </Grid>

        <TestInformationWrapper>
          <Grid container>
            <Grid item flexDirection="column" xs={mdUp ? true : 12}>
              <Typography variant="subtitle2">Test Information:</Typography>
              <div>
                <Dot />
                <Typography component="span" variant="body2">
                  Number of questions: <strong>{questionCount}</strong>
                </Typography>
              </div>
              <div>
                <Dot />
                <Typography component="span" variant="body2">
                  Author: <strong>{author}</strong>
                </Typography>
              </div>
              <div>
                <Dot />
                <Typography component="span" variant="body2">
                  Estimated time: <strong>{estimatedTime}</strong>
                </Typography>
              </div>
            </Grid>
            <Grid item display="flex" alignItems="flex-end" sx={{ mt: !mdUp ? 1 : undefined }}>
              {isAdmin ? (
                <>
                  <Button
                    size="large"
                    variant="contained"
                    color="success"
                    endIcon={<PersonAddAlt1Icon />}
                    onClick={toggleAssignDialog}
                  >
                    Assign
                  </Button>
                  <Button
                    size="large"
                    variant="contained"
                    color="info"
                    endIcon={<ContentCopyIcon />}
                    sx={{ ml: 2 }}
                    onClick={editTest}
                  >
                    Copy
                  </Button>
                  <Button
                    size="large"
                    variant="contained"
                    color="secondary"
                    endIcon={<EqualizerIcon />}
                    sx={{ ml: 2 }}
                    onClick={toggleAssignDialog}
                  >
                    Statistics
                  </Button>
                  <Button
                    size="large"
                    variant="contained"
                    color="primary"
                    endIcon={<DeleteIcon />}
                    sx={{ ml: 2 }}
                    onClick={onClickDelete}
                  >
                    Delete
                  </Button>
                </>
              ) : (
                <Button
                  size="large"
                  variant="contained"
                  color="secondary"
                  endIcon={<PlayArrowRounded />}
                  onClick={handleStartTest}
                >
                  Start
                </Button>
              )}
            </Grid>
          </Grid>
        </TestInformationWrapper>
      </PaperSection>
      {isAdmin && openAssign && <AssignDialog onClose={toggleAssignDialog} testId={testData.id} />}
      {isAdmin && openConfirmDelete && (
        <ConfirmDialog callback={deleteTest} onClose={() => setOpenConfirmDelete(false)} />
      )}
    </>
  );
};
