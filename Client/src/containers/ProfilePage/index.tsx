import React, { useMemo } from "react";

import { Box, BoxProps, Grid, styled, Typography } from "@mui/material";

import { useSelector } from "@/redux/hooks";
import { userAccountDataSelector } from "@/redux/selectors";
import emptyAccount from "@/shared/emptyAvatar.png";

const ImageBox = styled(Box, {
  shouldForwardProp: (prop) => prop !== "image",
})<BoxProps & { image?: string }>(({ theme, image }) => ({
  border: `3px solid ${theme.palette.primary.main}`,
  borderRadius: 30,
  backgroundImage: `url(${image})`,
  backgroundSize: "cover",
  backgroundRepeat: "no-repeat",
  backgroundPosition: "center",
  backgroundColor: theme.palette.primary.light,
}));

const ProfilePage = () => {
  const {
    data: { creationDate },
  } = useSelector(userAccountDataSelector);

  const creationDateString = useMemo(() => {
    const day = creationDate.getDay();
    const month = creationDate.getMonth();
    const year = creationDate.getFullYear();

    return `${day}/${month}/${year}`;
  }, [creationDate]);

  return (
    <Box width="100%" height="100%">
      <Grid container sx={{ maxWidth: 900 }} spacing={4}>
        <Grid item sx={{ display: "flex", alignItems: "flex-end", justifyContent: "space-between", width: "100%" }}>
          <Typography variant="h2">Personal Information</Typography>
          <Typography variant="caption">{`account was created on ${creationDateString} 🚀`}</Typography>
        </Grid>
        <Grid item>
          <ImageBox width={250} height={250} image={emptyAccount} />
        </Grid>
      </Grid>
    </Box>
  );
};

export default ProfilePage;
