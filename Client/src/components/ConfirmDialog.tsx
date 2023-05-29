import React, { FC } from "react";

import { Box, Button, Dialog, styled, Typography } from "@mui/material";

const ModalPaper = styled(Dialog)(({ theme }) => ({
  "& .MuiBackdrop-root": { opacity: "0!important" },
  ".MuiDialog-paper": {
    backgroundColor: theme.palette.common.white,
    width: 380,
    padding: theme.spacing(2),
  },
}));

interface Props {
  onClose: () => void;
  callback: () => void;
}

export const ConfirmDialog: FC<Props> = ({ onClose, callback }) => {
  return (
    <ModalPaper open onClose={onClose}>
      <Typography align="center" variant="h6" gutterBottom>
        Are you sure to delete test?
      </Typography>
      <Typography variant="caption">*all the assignments will be removed and tests history will be cleared</Typography>
      <Box display="flex" justifyContent="space-around" mt={2}>
        <Button variant="contained" onClick={onClose}>
          Cancel
        </Button>
        <Button variant="contained" onClick={callback}>
          Confirm
        </Button>
      </Box>
    </ModalPaper>
  );
};
