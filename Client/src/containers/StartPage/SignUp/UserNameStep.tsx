/* eslint-disable react/no-unescaped-entities */
import React, { FC, useCallback } from "react";

import {
  Box,
  Container,
  DialogContentText,
  FormControl,
  Grid,
  styled,
  Typography,
} from "@mui/material";

import { useFieldStable } from "@/api/hooks/useFieldStable";
import { TextFieldFormik } from "@/components/TextFieldFormik";

import { FormActions } from "./FormActions";
import { TopSection } from "./TopSection";

const ModalLayout = styled("div")(({ theme }) => ({
  display: "flex",
  height: "100%",
  flexDirection: "column",
  alignItems: "space-between",
}));

interface Props {
  onBack: () => void;
  onNext: () => void;
}

export const UserNameStep: FC<Props> = ({ onBack, onNext }) => {
  const [, meta] = useFieldStable("username");

  return (
    <ModalLayout>
      <TopSection onBack={onBack} />
      <Box display="flex" flexGrow={1} flexDirection="column">
        <Box mb={1} mt={3}>
          <Typography align="center" variant="h4">
            Create new account
          </Typography>
        </Box>
        <DialogContentText variant="body2">
          First, create your own username. Your username is the short name by
          which other users may identify you. It may be publicly visible, for
          example in leaderboards or emails. &nbsp;
        </DialogContentText>
        <FormControl fullWidth sx={{ mt: 2 }}>
          <TextFieldFormik
            clearable
            required
            autoFocus
            name="username"
            label="Username"
            color="secondary"
          />
        </FormControl>
      </Box>
      <FormActions disabled={!!meta.error} onBack={onBack} onNext={onNext} />
    </ModalLayout>
  );
};
