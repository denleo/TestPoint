import React, { FC } from "react";

import { Box, Button } from "@mui/material";

interface Props {
  disabled: boolean;
  isSubmit?: boolean;
  onBack: () => void;
  onNext?: () => void;
}

export const FormActions: FC<Props> = ({
  onBack,
  onNext,
  disabled,
  isSubmit = false,
}) => {
  return (
    <Box
      display="flex"
      width="100%"
      alignItems="center"
      justifyContent="flex-end"
    >
      <Button variant="text" type="reset" form="sign-up">
        Reset
      </Button>
      {isSubmit ? (
        <Button onClick={onBack} type="submit" form="sign-up" color="secondary">
          Create
        </Button>
      ) : (
        <Button
          disabled={disabled}
          onClick={onNext}
          form="sign-up"
          color="secondary"
        >
          Next
        </Button>
      )}
    </Box>
  );
};
