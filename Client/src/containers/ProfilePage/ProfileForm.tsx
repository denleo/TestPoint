import React, { useState, useCallback, useRef, FC } from "react";

import EditIcon from "@mui/icons-material/Edit";
import { Box, BoxProps, Button, Collapse, Grid, styled, Typography, IconButton } from "@mui/material";
import { Form, useFormikContext } from "formik";

import { TextFieldFormik } from "@/components/TextFieldFormik";

import { useSidebarStore } from "../layout/useLayoutStore";

import { ProfileFormValues } from "./common";
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
}

const ProfileForm: FC<Props> = ({ creationDate }) => {
  const [isEdit, setEdit] = useState(false);
  const [isEditPassword, setEditPassword] = useState(false);
  const {
    values: { image },
    setFieldValue,
  } = useFormikContext<ProfileFormValues>();

  const imageInputRef = useRef<HTMLInputElement>(null);

  const isMinimized = useSidebarStore((store) => store.isMinimized);

  const toggleEditMode = useCallback(() => {
    setEdit(!isEdit);
    setEditPassword(false);
  }, [isEdit]);

  const handleChangePassword = useCallback(() => {
    if (isEdit) {
      setEditPassword(true);
    }
  }, [isEdit]);

  const handleChangeImageClick = useCallback(() => {
    if (imageInputRef.current) {
      imageInputRef.current.click();
    }
  }, []);

  const handleImageInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files ? event.target.files[0] : null;
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        setFieldValue("image", reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  };

  return (
    <Form id="profile">
      <Grid container sx={{ maxWidth: 900 }} spacing={4} pb={3}>
        <Grid item xs={12} mb={4} sx={{ display: "flex", alignItems: "flex-end", justifyContent: "space-between" }}>
          <Typography variant="h2">Personal Information</Typography>
          <Typography variant="caption">{`account was created on ${creationDate} 🚀`}</Typography>
        </Grid>
        <Grid item xs={12} md={isMinimized ? 12 : 6} lg={6}>
          <ImageBox width={250} height={250} image={image} ml="auto" mr="auto">
            {isEdit && (
              <>
                <ButtonChangeImage size="large" onClick={handleChangeImageClick}>
                  <EditIcon />
                </ButtonChangeImage>
                <input type="file" hidden ref={imageInputRef} onChange={handleImageInputChange} />
              </>
            )}
          </ImageBox>
        </Grid>
        <Grid item xs>
          <Grid container direction="column" spacing={1}>
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
            <Grid item>
              <Collapse in={isEditPassword}>
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
              </Collapse>
            </Grid>
          </Grid>
        </Grid>
        <Grid item xs={12} sx={{ justifyContent: "flex-end", display: "flex" }}>
          <ProfileFormActions isEdit={isEdit} toggleEditMode={toggleEditMode} />
        </Grid>
      </Grid>
    </Form>
  );
};

export default ProfileForm;
