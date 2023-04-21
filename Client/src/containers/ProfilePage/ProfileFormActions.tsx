import React, { FC, useCallback } from "react";

import { Button } from "@mui/material";
import { useFormikContext } from "formik";

import { ProfileFormValues } from "./common";

interface Props {
  isEdit: boolean;
  toggleEditMode: () => void;
}

const ProfileFormActions: FC<Props> = ({ isEdit, toggleEditMode }) => {
  const { resetForm } = useFormikContext<ProfileFormValues>();

  const handleCancelEdit = useCallback(() => {
    toggleEditMode();
    resetForm();
  }, [toggleEditMode]);

  return !isEdit ? (
    <Button variant="outlined" color="secondary" onClick={toggleEditMode}>
      Edit profile
    </Button>
  ) : (
    <>
      <Button variant="outlined" color="error" type="reset" form="profile" sx={{ mr: 2 }} onClick={handleCancelEdit}>
        Cancel
      </Button>
      <Button variant="outlined" color="secondary" type="submit" form="profile">
        Save changes
      </Button>
    </>
  );
};

export default ProfileFormActions;
