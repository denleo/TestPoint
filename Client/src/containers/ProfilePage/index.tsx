import React, { useMemo, useState, useCallback } from "react";

import { Box, BoxProps, Button, Collapse, Grid, styled, Typography, IconButton } from "@mui/material";
import { Form, Formik } from "formik";

import { IconInfo } from "@/common/icons";
import { TextFieldFormik } from "@/components/TextFieldFormik";
import { useSelector } from "@/redux/hooks";
import { userAccountDataSelector } from "@/redux/selectors";
import emptyAccountImage from "@/shared/emptyAvatar.png";

import { useSidebarStore } from "../layout/useLayoutStore";
import { validationSchema, validateForm } from "../StartPage/SignUp/validation";

import { ProfileFormValues } from "./common";

const ImageBox = styled(Box, {
  shouldForwardProp: (prop) => prop !== "image",
})<BoxProps & { image?: string }>(({ theme, image }) => ({
  border: `3px solid ${theme.palette.primary.main}`,
  position: "relative",
  borderRadius: 30,
  backgroundImage: `url(${image})`,
  backgroundSize: "cover",
  backgroundRepeat: "no-repeat",
  backgroundPosition: "center",
  backgroundColor: theme.palette.primary.light,
}));

const ButtonChangeImage = styled(IconButton)(({ theme }) => ({
  position: "absolute",
  right: 0,
  transition: "50%",
  bottom: 0,
}));

const ProfilePage = () => {
  const [isEdit, setEdit] = useState(false);
  const [isEditPassword, setEditPassword] = useState(false);
  const isMinimized = useSidebarStore((store) => store.isMinimized);
  const {
    data: { creationDate, email, firstName, lastName, username, avatar },
  } = useSelector(userAccountDataSelector);

  const creationDateString = useMemo(() => {
    const day = creationDate.getDay();
    const month = creationDate.getMonth();
    const year = creationDate.getFullYear();

    return `${day}/${month}/${year}`;
  }, [creationDate]);

  const initValues: ProfileFormValues = useMemo(
    () => ({
      email,
      firstName,
      lastName,
      username,
      avatar,
      image: avatar ?? emptyAccountImage,
      password: "password",
      repeatPassword: "",
    }),
    []
  );

  const toggleEditMode = useCallback(() => {
    setEdit(!isEdit);
    setEditPassword(false);
  }, [isEdit]);

  const handleChangePassword = useCallback(() => {
    if (isEdit) {
      setEditPassword(true);
    }
  }, [isEdit]);

  return (
    <Box width="100%" height="100%">
      <Formik
        validateOnBlur
        validateOnChange
        initialValues={initValues}
        onSubmit={() => {}}
        validationSchema={validationSchema}
        validateForm={validateForm}
      >
        {({ values: { image } }) => (
          <Form>
            <Grid container sx={{ maxWidth: 900 }} spacing={4} pb={3}>
              <Grid
                item
                xs={12}
                mb={4}
                sx={{ display: "flex", alignItems: "flex-end", justifyContent: "space-between" }}
              >
                <Typography variant="h2">Personal Information</Typography>
                <Typography variant="caption">{`account was created on ${creationDateString} 🚀`}</Typography>
              </Grid>
              <Grid item xs={12} md={isMinimized ? 12 : 6} lg={6}>
                <ImageBox width={250} height={250} image={image} ml="auto" mr="auto">
                  {isEdit && (
                    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
                    // @ts-ignore
                    <ButtonChangeImage size="large" component="label">
                      <IconInfo />
                      <input type="file" hidden />
                    </ButtonChangeImage>
                  )}
                </ImageBox>
              </Grid>
              <Grid item xs>
                <Grid container direction="column">
                  <Grid item>
                    <TextFieldFormik
                      fullWidth
                      disabled={!isEdit}
                      size="small"
                      name="username"
                      label="Username"
                      color="secondary"
                      sx={{
                        minHeight: 71,
                      }}
                    />
                  </Grid>
                  <Grid item>
                    <TextFieldFormik
                      fullWidth
                      disabled={!isEdit}
                      size="small"
                      name="email"
                      label="Email"
                      color="secondary"
                      sx={{
                        minHeight: 71,
                      }}
                    />
                  </Grid>
                  <Grid item>
                    <TextFieldFormik
                      fullWidth
                      disabled={!isEdit}
                      size="small"
                      name="firstName"
                      label="FirstName"
                      color="secondary"
                      sx={{
                        minHeight: 71,
                      }}
                    />
                  </Grid>
                  <Grid item>
                    <TextFieldFormik
                      fullWidth
                      disabled={!isEdit}
                      size="small"
                      name="lastName"
                      label="LastName"
                      color="secondary"
                      sx={{
                        minHeight: 71,
                      }}
                    />
                  </Grid>
                  <Grid item>
                    <TextFieldFormik
                      fullWidth
                      disabled={!isEdit}
                      size="small"
                      type="password"
                      name="password"
                      label="Password"
                      color="secondary"
                      onClick={handleChangePassword}
                      sx={{
                        minHeight: 71,
                      }}
                    />
                  </Grid>
                  <Collapse in={isEditPassword}>
                    <Grid item>
                      <TextFieldFormik
                        fullWidth
                        disabled={!isEdit}
                        size="small"
                        type="password"
                        name="repeatPassword"
                        label="Repeat password"
                        color="secondary"
                        sx={{
                          minHeight: 71,
                        }}
                      />
                    </Grid>
                  </Collapse>
                </Grid>
              </Grid>
              <Grid item xs={12} sx={{ justifyContent: "flex-end", display: "flex" }}>
                {!isEdit ? (
                  <Button variant="outlined" color="secondary" onClick={toggleEditMode}>
                    Edit profile
                  </Button>
                ) : (
                  <>
                    <Button variant="outlined" color="error" type="reset" onClick={toggleEditMode} sx={{ mr: 2 }}>
                      Cancel
                    </Button>
                    <Button variant="outlined" color="secondary" type="submit" onClick={toggleEditMode}>
                      Save changes
                    </Button>
                  </>
                )}
              </Grid>
            </Grid>
          </Form>
        )}
      </Formik>
    </Box>
  );
};

export default ProfilePage;
