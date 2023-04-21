/* eslint-disable react/no-unescaped-entities */
import React, { FC } from "react";

import { Box, DialogContentText, FormControl, styled } from "@mui/material";

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
}

export const CredentialsStep: FC<Props> = ({ onBack }) => {
  const [, meta] = useFieldStable("username");

  return (
    <ModalLayout>
      <TopSection onBack={onBack} />
      <Box display="flex" flexGrow={1} mt={2} flexDirection="column">
        <DialogContentText variant="body2">
          This is the last important step, try not to forget the entered data.
        </DialogContentText>
        <FormControl fullWidth sx={{ mt: 2 }}>
          <TextFieldFormik
            clearable
            required
            autoFocus
            name="email"
            label="Email"
            color="secondary"
            sx={{
              minHeight: 90,
            }}
          />
        </FormControl>
        <FormControl fullWidth>
          <TextFieldFormik
            hidden
            required
            type="password"
            size="small"
            name="password"
            label="Password"
            color="secondary"
            sx={{ minHeight: 71 }}
          />
        </FormControl>
        <FormControl fullWidth>
          <TextFieldFormik
            hidden
            required
            type="password"
            size="small"
            name="repeatPassword"
            label="Repeat password"
            color="secondary"
            sx={{ minHeight: 71 }}
          />
        </FormControl>
      </Box>
      <FormActions isSubmit disabled={!!meta.error} onBack={onBack} />
    </ModalLayout>
  );
};
