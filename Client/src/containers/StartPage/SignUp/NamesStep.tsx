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

export const NamesStep: FC<Props> = ({ onBack, onNext }) => {
  const [, metaFirstName] = useFieldStable("firstName");
  const [, metaLastName] = useFieldStable("lastName");

  return (
    <ModalLayout>
      <TopSection onBack={onBack} />
      <Box display="flex" flexGrow={1} flexDirection="column">
        <Box mb={1} mt={3}>
          <Typography align="center" variant="h5">
            Please, introduce yourself
          </Typography>
        </Box>
        <FormControl fullWidth sx={{ mt: 2 }}>
          <TextFieldFormik
            required
            autoFocus
            name="firstName"
            label="First name"
            color="secondary"
            sx={{
              minHeight: 100,
            }}
          />
        </FormControl>
        <FormControl fullWidth>
          <TextFieldFormik
            required
            name="lastName"
            label="Last name"
            color="secondary"
            sx={{
              minHeight: 100,
            }}
          />
        </FormControl>
      </Box>
      <FormActions
        disabled={!!metaFirstName.error || !!metaLastName.error}
        onBack={onBack}
        onNext={onNext}
      />
    </ModalLayout>
  );
};
