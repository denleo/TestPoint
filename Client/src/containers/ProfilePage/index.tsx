import React, { useMemo } from "react";

import { Box } from "@mui/material";
import { Formik } from "formik";

import { useSelector } from "@/redux/hooks";
import { userAccountDataSelector } from "@/redux/selectors";
import emptyAccountImage from "@/shared/emptyAvatar.png";

import { validationSchema, validateForm } from "@api/validation";

import { ProfileFormValues } from "./common";
import ProfileForm from "./ProfileForm";

const ProfilePage = () => {
  const {
    data: { creationDate, email, firstName, lastName, username, avatar },
  } = useSelector(userAccountDataSelector);

  const creationDateString = useMemo(() => {
    const day = creationDate.getDay();
    const month = creationDate.getMonth();
    const year = creationDate.getFullYear();

    return `${day}/${month}/${year}`;
  }, [creationDate]);

  const initialValues: ProfileFormValues = useMemo(
    () => ({
      email,
      firstName,
      lastName,
      username,
      avatar,
      image: avatar ?? emptyAccountImage,
      password: "Password123",
      repeatPassword: "",
    }),
    []
  );

  return (
    <Box width="100%" height="100%">
      <Formik
        validateOnBlur
        validateOnChange
        initialValues={initialValues}
        onSubmit={() => {}}
        validationSchema={validationSchema}
        validate={validateForm}
      >
        <ProfileForm creationDate={creationDateString} />
      </Formik>
    </Box>
  );
};

export default ProfilePage;
