import React, { useState, useCallback, useRef, FC } from "react";

import EditIcon from "@mui/icons-material/Edit";
import { Box, BoxProps, Collapse, Grid, styled, Typography, IconButton, alpha } from "@mui/material";
import { Form, useFormikContext } from "formik";

import { WHITE } from "@/common/theme/colors";
import { useNotificationStore, NotificationType } from "@/components/NotificationProvider/useNotificationStore";
import { TextFieldFormik } from "@/components/TextFieldFormik";
import { useDispatch } from "@/redux/hooks";

import { AccountActions } from "../../redux/userAccount/actions";
import { useSidebarStore } from "../layout/useLayoutStore";

import { defaultPassword, ProfileFormValues } from "./common";
import ProfileFormActions from "./ProfileFormActions";

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

const ButtonChangeImage = styled(IconButton)(() => ({
  position: "absolute",
  right: 0,
  transition: "50%",
  bottom: 0,
}));

interface Props {
  creationDate: string;
  avatar: string;
}

const ProfileForm: FC<Props> = ({ creationDate, avatar }) => {
  const [isEdit, setEdit] = useState(false);
  const [isEditPassword, setEditPassword] = useState(false);
  const {
    setFieldValue,
    resetForm,
    validateForm,
    values: { password, oldPassword },
  } = useFormikContext<ProfileFormValues>();
  const notify = useNotificationStore((store) => store.notify);
  const dispatch = useDispatch();

  const imageInputRef = useRef<HTMLInputElement>(null);

  const isMinimized = useSidebarStore((store) => store.isMinimized);

  const toggleEditMode = useCallback(() => {
    resetForm();
    setEditPassword(false);
    setEdit(!isEdit);
  }, [isEdit]);

  const openEditPasswordMode = useCallback(() => {
    resetForm();
    setEdit(false);
    setEditPassword(true);
  }, [setFieldValue, resetForm]);

  const resetPasswordEdit = useCallback(() => {
    setEditPassword(false);
    resetForm();
  }, [setFieldValue]);

  const handleChangeImageClick = useCallback(() => {
    if (imageInputRef.current) {
      imageInputRef.current.click();
    }
  }, []);

  const handleImageInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files ? event.target.files[0] : null;
    if (file) {
      const reader = new FileReader();
      reader.onloadend = async () => {
        const base64Data = (reader.result as string).split(",")[1];
        setFieldValue("image", reader.result as string);
        const resultAction = await dispatch(AccountActions.changeAvatar(base64Data));
        if ("abort" in resultAction || "error" in resultAction) {
          console.log(resultAction);
          notify("Failed to update avatar", NotificationType.Error);
        } else {
          await dispatch(AccountActions.getUserData());
        }
      };
      reader.readAsDataURL(file);
    }
  };

  const submitChangePassword = useCallback(async () => {
    const resultAction = await dispatch(
      AccountActions.changePassword({
        newPassword: password,
        oldPassword,
      })
    );
    if ("abort" in resultAction || "error" in resultAction) {
      notify("Failed to update password", NotificationType.Error);
    } else {
      notify("Password has been changed", NotificationType.Success);
    }
    resetForm();
    setEditPassword(false);
  }, [password, oldPassword, dispatch, notify, resetForm]);

  return (
    <Form id="profile">
      <Grid container sx={{ maxWidth: 900 }} spacing={4} pb={3}>
        <Grid item xs={12} mb={4} sx={{ display: "flex", alignItems: "flex-end", justifyContent: "space-between" }}>
          <Typography variant="h2">Personal Information</Typography>
          <Typography variant="caption">{`account was created on ${creationDate} 🚀`}</Typography>
        </Grid>
        <Grid item xs={12} md={isMinimized ? 12 : 6} lg={6}>
          <ImageBox width={250} height={250} image={avatar} ml="auto" mr="auto">
            <ButtonChangeImage size="large" onClick={handleChangeImageClick}>
              <EditIcon />
            </ButtonChangeImage>
            <input type="file" hidden ref={imageInputRef} onChange={handleImageInputChange} />
          </ImageBox>
        </Grid>
        <Grid item xs>
          <Grid
            container
            direction="column"
            spacing={1}
            sx={{ backgroundColor: alpha(WHITE, 0.82), borderRadius: 2, p: 2 }}
          >
            <Grid item sx={{ pb: 3 }}>
              <TextFieldFormik
                fullWidth
                disabled
                size="small"
                name="username"
                label="Username"
                color="secondary"
                sx={{
                  minHeight: 71,
                }}
              />
              <Collapse in={isEditPassword}>
                <TextFieldFormik
                  fullWidth
                  autoFocus
                  size="small"
                  type="password"
                  name="oldPassword"
                  label="Old Password"
                  color="secondary"
                  sx={{
                    minHeight: 71,
                  }}
                />
                <TextFieldFormik
                  fullWidth
                  size="small"
                  type="password"
                  name="password"
                  label="Password"
                  color="secondary"
                  sx={{
                    minHeight: 71,
                  }}
                />
                <TextFieldFormik
                  fullWidth
                  size="small"
                  type="password"
                  name="repeatPassword"
                  label="Repeat password"
                  color="secondary"
                  sx={{
                    minHeight: 71,
                  }}
                />
              </Collapse>
              <Box sx={{ justifyContent: "flex-end", display: "flex" }}>
                <ProfileFormActions
                  password
                  isEdit={isEditPassword}
                  toggleEditMode={openEditPasswordMode}
                  onReset={resetPasswordEdit}
                  onSubmit={submitChangePassword}
                />
              </Box>
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
            <Grid item sx={{ justifyContent: "flex-end", display: "flex" }}>
              <ProfileFormActions isEdit={isEdit} toggleEditMode={toggleEditMode} />
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </Form>
  );
};

export default ProfileForm;
