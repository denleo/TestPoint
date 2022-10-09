import { Breakpoint, useMediaQuery, useTheme } from "@mui/material";

export function useBreakpoint(breakpoint: Breakpoint | number) {
  const theme = useTheme();
  // Fun fact of the week. Without { noSsr: true } this always returns "false" on
  // the first pass which results in massive, horrid flickering
  // https://github.com/mui-org/material-ui/issues/21142
  return useMediaQuery(theme.breakpoints.up(breakpoint), { noSsr: true });
}
