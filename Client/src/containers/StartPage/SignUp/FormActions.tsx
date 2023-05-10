import React, { FC, useCallback } from "react";

import { Box, Button } from "@mui/material";
import { useFormikContext } from "formik";

import { useDispatch } from "@/redux/hooks";
import { AccountActions } from "@/redux/userAccount/actions";

import { SignUpUserFormValues, START_PAGE_STEPS } from "../common";
import { useStartPageStore } from "../useStartPageStore";

interface Props {
  disabled: boolean;
  isSubmit?: boolean;
  onBack: () => void;
  onNext?: () => void;
}

export const FormActions: FC<Props> = ({ onBack, onNext, disabled, isSubmit = false }) => {
  const { values } = useFormikContext<SignUpUserFormValues>();
  const dispatch = useDispatch();
  const setPageStep = useStartPageStore((state) => state.setPageStep);

  const submitForm = useCallback(async () => {
    try {
      await dispatch(AccountActions.registerUser(values));
      setPageStep(START_PAGE_STEPS.LOGIN);
    } catch (error) {
      console.log(error);
    }
  }, [values]);

  return (
    <Box display="flex" width="100%" alignItems="center" justifyContent="flex-end">
      <Button variant="text" type="reset" form="sign-up">
        Reset
      </Button>
      {isSubmit ? (
        <Button type="submit" form="sign-up" onClick={submitForm} color="secondary">
          Create
        </Button>
      ) : (
        <Button disabled={disabled} onClick={onNext} form="sign-up" color="secondary">
          Next
        </Button>
      )}
    </Box>
  );
};
