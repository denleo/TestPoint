import React, { useCallback, useEffect, useState } from "react";

import { Alert, Grid, Paper, styled, Tab, TabProps, Tabs, useTheme } from "@mui/material";
import { Formik } from "formik";
import { useLocation, useNavigate } from "react-router-dom";
import * as yup from "yup";

import { useBreakpoint } from "@/api/hooks/useBreakPoint";
import { httpAction } from "@/api/httpAction";
import { IconFullLogo } from "@/common/icons";
import { MOCK_USER } from "@/mock/user";
import { useDispatch, useSelector } from "@/redux/hooks";
import { userAccountNameSelector } from "@/redux/selectors";
import { AccountActions } from "@/redux/userAccount/actions";
import { setUserData } from "@/redux/userAccount/reducer";
import { ResponseStatuses } from "@/redux/userAccount/state";

import { LoginUserFormValues, LOGIN_TAB } from "../common";

import { ActionButtons } from "./ActionButtons";
import { LoginForm } from "./LoginForm";

const ModalPaper = styled(Paper)(({ theme }) => ({
  width: 432,
  height: 400,
  padding: theme.spacing(2),
  backgroundColor: theme.palette.common.white,
  boxShadow: `0px 24px 52px ${theme.palette.divider}`,
  borderRadius: theme.spacing(0, 0, 2, 2),
  border: `1px solid ${theme.palette.divider}`,
  borderTop: "none",
  [theme.breakpoints.down("md")]: {
    width: "100vw",
    height: "calc(100vh - 48px)",
    borderRadius: 0,
    border: "none",
  },
}));

const CustomTab = styled(Tab, {
  shouldForwardProp: (prop) => prop !== "selected",
})<TabProps & { selected?: boolean }>(({ theme, selected }) => ({
  backgroundColor: theme.palette.background.paper,
  color: theme.palette.grey[400],
  borderRadius: theme.spacing(2, 0, 0, 0),
  padding: theme.spacing(1, 1),
  "&:last-child": {
    borderRadius: `0 ${theme.spacing(2)} 0 0`,
  },
  ...(selected && {
    backgroundColor: theme.palette.common.white,
    border: `1px solid ${theme.palette.divider}`,
    borderBottom: "none",
    color: theme.palette.text.primary,
    textDecoration: "none",
  }),
  [theme.breakpoints.down("md")]: {
    borderRadius: 0,
    border: "none",
    "&:last-child": {
      borderRadius: 0,
    },
  },
}));

export const Login = () => {
  const theme = useTheme();
  const mdUp = useBreakpoint("md");

  const navigate = useNavigate();
  const userName = useSelector(userAccountNameSelector);
  const location = useLocation();
  const dispatch = useDispatch();

  const [loginTab, setLoginTab] = useState(LOGIN_TAB.USER);
  const [error, setError] = useState<Error>();

  const from = location.state?.from?.pathname || "/";

  const handleChangeTab = useCallback(
    (e: React.SyntheticEvent<Element, Event>, value: LOGIN_TAB) => {
      setLoginTab(value);
    },
    [setLoginTab]
  );

  const submitLoginForm = useCallback(async (values: LoginUserFormValues) => {
    // await dispatch(
    //   AccountActions.requestLogin({
    //     login: values.username,
    //     password: values.password,
    //   })
    // );
    // await dispatch(AccountActions.getUserData());
    dispatch(
      setUserData({
        data: MOCK_USER,
        isAdmin: loginTab === LOGIN_TAB.ADMIN,
        status: ResponseStatuses.Success,
      })
    );
    navigate(from, { replace: true });
    console.log(userName);
  }, []);

  useEffect(() => {
    console.log(userName);
  }, [userName]);

  const isUser = loginTab === LOGIN_TAB.USER;

  return (
    <div>
      {error && mdUp && (
        <Alert sx={{ marginBottom: theme.spacing(2) }} variant="outlined" severity="error">
          {error.message}
        </Alert>
      )}
      <Tabs variant="fullWidth" value={loginTab} onChange={handleChangeTab} aria-label="choose login type">
        <CustomTab label="User" value={LOGIN_TAB.USER} selected={isUser} />
        <CustomTab label="Admin" value={LOGIN_TAB.ADMIN} selected={!isUser} />
      </Tabs>
      <ModalPaper>
        <Formik
          initialValues={
            {
              username: "",
              password: "",
              rememberMe: false,
            } as LoginUserFormValues
          }
          validationSchema={yup.object().shape({
            username: yup.string().min(3, "Minimum 3 symbols").max(255, "Maximum 255 symbols").required("Required"),
            password: yup.string().min(10, "At least 10 characters").required("Required"),
            rememberMe: yup.boolean(),
          })}
          onSubmit={submitLoginForm}
        >
          <Grid container spacing={2}>
            <Grid
              item
              xs={12}
              sx={{
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                margin: theme.spacing(1, 0),
              }}
            >
              <IconFullLogo width={180} height={35} />
            </Grid>
            <Grid item xs={12}>
              <LoginForm isUser={isUser} />
            </Grid>
            <Grid item>
              {error && !mdUp && (
                <Alert variant="outlined" severity="error">
                  {error.message}
                </Alert>
              )}
            </Grid>
            <Grid item xs={12}>
              <ActionButtons withSignUp={isUser} />
            </Grid>
          </Grid>
        </Formik>
      </ModalPaper>
    </div>
  );
};
