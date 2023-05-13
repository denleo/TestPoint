import React, { FC, useCallback, useMemo } from "react";

import { Button } from "@mui/material";
import { useFormikContext } from "formik";

import { NotificationType, useNotificationStore } from "@/components/NotificationProvider/useNotificationStore";
import { useDispatch } from "@/redux/hooks";
import { AccountActions } from "@/redux/userAccount/actions";

import { ProfileFormValues } from "./common";

interface Props {
  isEdit: boolean;
  password?: boolean;
  toggleEditMode: () => void;
  onReset?: () => void;
  onSubmit?: () => void;
}

const ProfileFormActions: FC<Props> = ({ isEdit, toggleEditMode, password = false, onReset, onSubmit }) => {
  const { resetForm, errors, values } = useFormikContext<ProfileFormValues>();
  const dispatch = useDispatch();
  const notify = useNotificationStore((store) => store.notify);

  const handleCancelEdit = useCallback(() => {
    toggleEditMode();
    resetForm();
  }, [toggleEditMode, resetForm]);

  const disabledSubmit = useMemo((): boolean => {
    if (password) return !!(errors.password || errors.repeatPassword);

    return !!(errors.email || errors.firstName || errors.lastName);
  }, [password, errors]);

  const submitForm = useCallback(async () => {
    const resultAction = await dispatch(AccountActions.changeProfile({ ...values }));
    if ("error" in resultAction) {
      notify(resultAction.error.message ?? "Failed to update profile", NotificationType.Error);
    } else {
      notify("Profile has been updated", NotificationType.Success);
      await dispatch(AccountActions.getUserData());
    }

    resetForm();
    toggleEditMode();
  }, [dispatch, notify, values, toggleEditMode, resetForm]);

  return !isEdit ? (
    <Button size="small" variant="outlined" color="secondary" onClick={toggleEditMode} sx={{ minWidth: 160 }}>
      {password ? "Edit password" : "Edit profile"}
    </Button>
  ) : (
    <>
      <Button
        size="small"
        variant="outlined"
        color="error"
        type="reset"
        form="profile"
        sx={{ mr: 2 }}
        onClick={onReset ?? handleCancelEdit}
      >
        Cancel
      </Button>
      <Button
        size="small"
        disabled={disabledSubmit}
        variant="outlined"
        color="secondary"
        type="submit"
        form={!password ? "profile" : undefined}
        onClick={password ? onSubmit : submitForm}
      >
        Save changes
      </Button>
    </>
  );
};

export default ProfileFormActions;
