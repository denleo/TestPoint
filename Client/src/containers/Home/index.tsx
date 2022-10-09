import React, { FC } from "react";

import { Box } from "@mui/material";

import { theme } from "@/common/theme/createTheme";

import { LayoutContainer } from "../layout/index";

const HomePage: FC = () => {
  return (
    <LayoutContainer>
      <Box flex={1} color={theme.palette.error.main}>
        MAXIM
      </Box>
    </LayoutContainer>
  );
};

export default HomePage;
