import { createTheme } from "@mui/material";

export const WHITE = "#FFFFFF";
export const BLACK = "#000000";
export const MAIN_BACKGROUND = "#F3F4F6";

export const { palette } = createTheme({
  palette: {
    primary: {
      main: "#D8DEE9",
      light: "#e5e9f0",
      dark: "#81a1c1",
    },
    secondary: {
      main: "#4263EB",
      light: "#748FFC",
      dark: "#364FC7",
    },
    success: {
      main: "#8ce99a",
      light: "#d3ffce",
      dark: "#66a80f",
    },
    info: {
      main: "#9775fa",
      light: "#d0bfff",
      dark: "#845ef7",
    },
    error: {
      main: "#FF1744",
      light: "#FF8A80",
      dark: "#D50000",
    },
    warning: {
      main: "#ffe066",
      light: "#fff3bf",
      dark: "#fcc419",
    },
    text: {
      primary: "#111827",
      secondary: "#6B7280",
      disabled: "#d8dee9",
    },
    background: {
      default: MAIN_BACKGROUND,
      paper: "#e5e9f0",
    },
  },
});
