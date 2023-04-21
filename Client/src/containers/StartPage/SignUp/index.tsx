import React, { useCallback, useState } from "react";

import { Paper, styled } from "@mui/material";
import { Form, Formik } from "formik";

import { validateForm, validationSchema } from "../../../api/validation";
import { SignUpUserFormValues, SIGN_UP_STEPS, START_PAGE_STEPS } from "../common";
import { useStartPageStore } from "../useStartPageStore";

import { CredentialsStep } from "./CredentialsStep";
import { NamesStep } from "./NamesStep";
import { UserNameStep } from "./UserNameStep";

const ModalPaper = styled(Paper)(({ theme }) => ({
  width: 432,
  height: 448,
  padding: theme.spacing(2),
  backgroundColor: theme.palette.common.white,
  boxShadow: `0px 24px 52px ${theme.palette.divider}`,
  borderRadius: theme.spacing(2),
  border: `1px solid ${theme.palette.divider}`,
  [theme.breakpoints.down("md")]: {
    width: "100vw",
    height: "100vh",
    borderRadius: 0,
    border: "none",
  },
}));

const initialFormValues: SignUpUserFormValues = {
  username: "",
  firstName: "",
  lastName: "",
  email: "",
  password: "",
  repeatPassword: "",
};

export const SignUp = () => {
  const [signUpStep, setSignUpStep] = useState(SIGN_UP_STEPS.USERNAME);

  const setPageStep = useStartPageStore((state) => state.setPageStep);

  const backToLogin = useCallback(() => {
    setPageStep(START_PAGE_STEPS.LOGIN);
  }, []);

  const goToUserName = useCallback(() => {
    setSignUpStep(SIGN_UP_STEPS.USERNAME);
  }, []);

  const goToNames = useCallback(() => {
    setSignUpStep(SIGN_UP_STEPS.NAMES);
  }, []);

  const openCredentials = useCallback(() => {
    setSignUpStep(SIGN_UP_STEPS.CREDENTIALS);
  }, []);

  const openNames = useCallback(() => {
    setSignUpStep(SIGN_UP_STEPS.NAMES);
  }, []);

  const handleResetForm = useCallback(() => {
    setSignUpStep(SIGN_UP_STEPS.USERNAME);
  }, []);

  return (
    <ModalPaper>
      <Formik
        validateOnChange
        validateOnBlur
        initialValues={initialFormValues}
        validate={validateForm}
        validationSchema={validationSchema}
        onSubmit={() => {}}
        onReset={handleResetForm}
      >
        <Form style={{ height: "100%" }} id="sign-up">
          {signUpStep === SIGN_UP_STEPS.USERNAME && <UserNameStep onBack={backToLogin} onNext={openNames} />}
          {signUpStep === SIGN_UP_STEPS.NAMES && <NamesStep onBack={goToUserName} onNext={openCredentials} />}
          {signUpStep === SIGN_UP_STEPS.CREDENTIALS && <CredentialsStep onBack={goToNames} />}
        </Form>
      </Formik>
    </ModalPaper>
  );
};
