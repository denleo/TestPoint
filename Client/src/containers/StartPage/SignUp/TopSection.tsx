import React, { FC } from "react";

import { ArrowBack } from "@mui/icons-material";
import { Box, Button } from "@mui/material";

import { IconFullLogo } from "@/common/icons";

interface Props {
  onBack: () => void;
}

export const TopSection: FC<Props> = ({ onBack }) => {
  return (
    <Box display="flex" width="100%" alignItems="center" justifyContent="space-between">
      <Button startIcon={<ArrowBack />} onClick={onBack}>
        Back
      </Button>
      <IconFullLogo />
    </Box>
  );
};
