import React, { useMemo, useCallback, useState } from "react";

import { Box } from "@mui/material";
import { Formik } from "formik";

import { NotificationType, useNotificationStore } from "@/components/NotificationProvider/useNotificationStore";
import { useSelector, useDispatch } from "@/redux/hooks";
import { userDataSelector } from "@/redux/selectors";
import emptyAccountImage from "@/shared/emptyAvatar.png";

import { validationSchema, validateForm } from "@api/validation";
import { AccountActions } from "@redux/userAccount/actions";

import { ProfileFormValues } from "./common";
import ProfileForm from "./ProfileForm";

const ProfilePage = () => {
  const data = useSelector(userDataSelector);
  const dispatch = useDispatch();
  const notify = useNotificationStore((store) => store.notify);

  if (!data) return null;
  const { registryDate, email, firstName, lastName, username, base64Avatar } = data;

  const creationDateString = useMemo(() => {
    const day = registryDate.getDay();
    const month = registryDate.getMonth();
    const year = registryDate.getFullYear();

    return `${day}/${month}/${year}`;
  }, [data, registryDate]);

  const initialValues: ProfileFormValues = useMemo(
    () => ({
      email,
      firstName,
      lastName,
      username,
      avatar: base64Avatar ? `data:image/png;base64,${base64Avatar}` : emptyAccountImage,
      oldPassword: "",
      password: "",
      repeatPassword: "",
    }),
    [data, data.username, data.firstName, data.email, data.lastName]
  );

  const submitForm = useCallback(
    async (values: ProfileFormValues) => {
      const resultAction = await dispatch(AccountActions.changeProfile({ ...values }));
      if ("error" in resultAction) {
        notify(resultAction.error.message ?? "Failed to update profile", NotificationType.Error);
      } else {
        notify("Profile has been updated", NotificationType.Success);
        await dispatch(AccountActions.getUserData());
      }
    },
    [dispatch, notify]
  );

  return (
    <Box display="flex" justifyContent="center" alignItems="center">
      <Formik
        validateOnBlur
        validateOnChange
        initialValues={initialValues}
        onSubmit={() => console.log("submit")}
        validationSchema={validationSchema}
        validate={validateForm}
      >
        <ProfileForm
          creationDate={creationDateString}
          avatar={base64Avatar ? `data:image/png;base64,${base64Avatar}` : emptyAccountImage}
        />
      </Formik>
    </Box>
  );
};

export default ProfilePage;
