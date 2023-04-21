import { createTheme, lighten, ThemeOptions } from "@mui/material/styles";

import { IconError, IconInfo, IconSuccess, IconWarning, ChevronDownIcon } from "@/common/icons";

import { palette, WHITE, BLACK, MAIN_BACKGROUND } from "./colors";

export const HTML_FONT_SIZE = 14;

const { typography: typographyRaw } = createTheme({
  typography: {
    htmlFontSize: HTML_FONT_SIZE,
    fontWeightRegular: 400,
    fontWeightMedium: 600,
    fontWeightBold: 700,
    fontFamily: "Mulish, IBM Plex Serif, sans-serif",
  },
});

const { typography } = createTheme({
  typography: {
    ...typographyRaw,
    body1: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(16),
      fontWeight: typographyRaw.fontWeightRegular,
      lineHeight: typographyRaw.pxToRem(24),
      letterSpacing: "0.0094em",
    },
    body2: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(14),
      fontWeight: typographyRaw.fontWeightRegular,
      lineHeight: typographyRaw.pxToRem(20),
      letterSpacing: "0.0107em",
    },
    subtitle1: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(16),
      fontWeight: typographyRaw.fontWeightRegular,
      lineHeight: typographyRaw.pxToRem(28),
      letterSpacing: "0.0094em",
      textTransform: "none",
    },
    subtitle2: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(14),
      fontWeight: typographyRaw.fontWeightBold,
      lineHeight: typographyRaw.pxToRem(22),
      letterSpacing: "0.0071em",
    },
    h1: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(48),
      fontWeight: typographyRaw.fontWeightLight,
      lineHeight: "120%",
      letterSpacing: "-0.0156em",
    },
    h2: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(40),
      fontWeight: typographyRaw.fontWeightLight,
      lineHeight: "120%",
      letterSpacing: "-0.0083em",
    },
    h3: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(32),
      fontWeight: typographyRaw.fontWeightRegular,
      lineHeight: "124%",
      letterSpacing: 0,
    },
    h4: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(22),
      lineHeight: typographyRaw.pxToRem(32),
      fontWeight: typographyRaw.fontWeightMedium,
      letterSpacing: 0,
    },
    h5: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(20),
      lineHeight: "150%",
      fontWeight: typographyRaw.fontWeightBold,
      letterSpacing: 0,
    },
    h6: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(16),
      fontWeight: typographyRaw.fontWeightBold,
      lineHeight: "150%",
      letterSpacing: "0.0075em",
      textTransform: "none",
    },
    caption: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(12),
      fontWeight: typographyRaw.fontWeightRegular,
      lineHeight: typographyRaw.pxToRem(20),
      letterSpacing: "0.033em",
    },
    overline: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(12),
      fontWeight: typographyRaw.fontWeightRegular,
      lineHeight: typographyRaw.pxToRem(32),
      letterSpacing: "0.083em",
      textTransform: "uppercase",
    },
    button: {
      fontFamily: typographyRaw.fontFamily,
      fontSize: typographyRaw.pxToRem(14),
      fontWeight: typographyRaw.fontWeightBold,
      letterSpacing: "0.0286em",
      lineHeight: typographyRaw.pxToRem(24),
      textTransform: "capitalize",
    },
  },
});

const { breakpoints, spacing, transitions } = createTheme({
  breakpoints: {
    values: {
      xs: 0,
      sm: 400,
      md: 720,
      lg: 1280,
      xl: 1440,
    },
  },
});

export const theme = createTheme({
  typography,
  breakpoints,
  spacing,
  transitions,
  palette,
  shape: {
    borderRadius: 8,
  },
  props: {
    MuiAppBar: {
      elevation: 0,
    },
    MuiPaper: {
      elevation: 0,
    },
    MuiCard: {
      elevation: 0,
    },
    MuiCardHeader: {
      titleTypographyProps: { variant: "h4" },
    },
    MuiCheckbox: {
      color: "default",
    },
    MuiInputAdornment: {
      disableTypography: true,
    },
    MuiLink: {
      color: "textSecondary",
    },
    MuiRadio: {
      color: "default",
    },
    MuiSwitch: {
      color: "default",
    },
    MuiTabs: {
      textColor: "primary",
    },
    MuiTooltip: {
      enterTouchDelay: 0,
    },
    MuiSelect: {
      IconComponent: ChevronDownIcon,
    },
    MuiSnackbar: {
      autoHideDuration: 3000,
    },
    MuiTextField: {
      variant: "filled",
      margin: "normal",
      fullWidth: true,
    },
    MuiButton: {
      size: "large",
    },
    MuiAlert: {
      iconMapping: {
        error: IconError,
        info: IconInfo,
        success: IconSuccess,
        warning: IconWarning,
      },
    },
    MuiPopover: {
      marginThreshold: 24,
    },
    MuiTablePagination: {
      labelRowsPerPage: "",
      component: "div",
      SelectProps: {
        renderValue: (selected: number | string) => `${selected} rows `,
      },
    },
  },
  components: {
    MuiAlert: {
      styleOverrides: {
        root: {
          borderRadius: 8,
        },
        icon: {
          color: "inherit!important",
        },
        action: {
          alignItems: "flex-start",
        },
        standardSuccess: {
          borderColor: "transparent",
          backgroundColor: palette.success.main,
        },
        standardWarning: {
          borderColor: "transparent",
          backgroundColor: palette.warning.main,
        },
        standardError: {
          borderColor: "transparent",
          backgroundColor: palette.error.main,
        },
        standardInfo: {
          borderColor: "transparent",
          backgroundColor: palette.info.main,
        },
      },
    },
    MuiAlertTitle: {
      styleOverrides: {
        root: {
          fontWeight: 600,
        },
      },
    },
    MuiAvatar: {
      styleOverrides: {
        root: {
          fontSize: typographyRaw.pxToRem(16),
          fontWeight: typographyRaw.fontWeightBold,
        },
      },
    },
    MuiBackdrop: {
      styleOverrides: {
        root: {
          backgroundColor: lighten("#000", 0.8),
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          whiteSpace: "nowrap",
          padding: spacing(0.75, 2),

          "& $label svg": {
            fontSize: typographyRaw.pxToRem(20), // TODO: check if needed
          },

          "&$outlined": {
            padding: spacing(0.5, 1.75),
          },
          transition: transitions.create(["background-color", "box-shadow"], {
            duration: transitions.duration.short,
          }),
        },
        endIcon: {
          marginRight: 0,
          marginLeft: 8,
        },
        startIcon: {
          marginRight: 8,
          marginLeft: 0,
        },
        sizeLarge: {
          fontSize: typographyRaw.pxToRem(16),
          lineHeight: typographyRaw.pxToRem(26),
          letterSpacing: "0.0289em",
          padding: spacing(1.25, 2),

          "& $label svg": {
            fontSize: typographyRaw.pxToRem(24), // TODO: check if needed
          },
          "&$outlined": {
            padding: spacing(1, 1.75),
          },
        },
        sizeSmall: {
          fontSize: typographyRaw.pxToRem(12),
          lineHeight: typographyRaw.pxToRem(22),
          letterSpacing: "0.0383em",
          padding: spacing(0.5, 2),

          "& $label svg": {
            fontSize: typographyRaw.pxToRem(18), // TODO: check if needed
          },
          "&$outlined": {
            padding: spacing(0.25, 1.75),
          },
        },
        text: {
          padding: spacing(0.75, 2),
          "&$disabled": {
            color: palette.action.disabled,
          },
          "&:active, &:hover": {
            backgroundColor: palette.action.hover,
          },
        },
        textPrimary: {
          "&:hover": {
            backgroundColor: palette.action.hover,
          },
        },
        textSecondary: {
          "&:hover": {
            backgroundColor: palette.action.hover,
          },
        },
        outlined: {
          borderWidth: 2,
          "&:hover": {
            borderWidth: 2,
            backgroundColor: palette.action.hover,
          },
          "&$disabled": {
            borderWidth: 2,
            borderColor: palette.action.disabledBackground,
            color: palette.action.disabled,
          },
        },
        outlinedPrimary: {
          borderWidth: 2,
          "&:hover": {
            borderWidth: 2,
            backgroundColor: palette.action.hover,
          },
          "&$disabled": {
            borderWidth: 2,
            borderColor: palette.action.disabledBackground,
            color: palette.action.disabled,
          },
        },
        outlinedSecondary: {
          borderWidth: 2,
          "&:hover": {
            borderWidth: 2,
            backgroundColor: palette.action.hover,
          },
          "&$disabled": {
            borderWidth: 2,
            borderColor: palette.action.disabledBackground,
            color: palette.action.disabled,
          },
        },
        contained: {
          boxShadow: "none",
          border: "none",

          "&$disabled": {
            boxShadow: "unset",
            backgroundColor: palette.action.disabledBackground,
            color: palette.action.disabled,
          },
        },
      },
    },
    MuiButtonGroup: {
      styleOverrides: {
        grouped: {
          minWidth: 36,
        },
        contained: {
          boxShadow: "none",
          padding: spacing(0.5),
          borderRadius: spacing(1.5),
        },
        groupedContained: {
          background: palette.primary.light,
          color: "#FF7E62",

          "&:hover": {
            background: "transparent",
            color: palette.text.primary,
          },

          "&[disabled]": {
            background: "transparent",
          },
        },
        groupedContainedPrimary: {
          color: palette.text.secondary,

          "&:not(:last-child)": {
            border: "none",
          },
        },
        groupedContainedSecondary: {
          background: palette.primary.light,
          color: palette.secondary.main,

          "&:not(:last-child)": {
            border: "none",
          },
        },
        groupedTextPrimary: {
          "&:not(:last-child)": {
            border: "none",
          },
        },
        groupedTextSecondary: {
          "&:not(:last-child)": {
            border: "none",
          },
        },
        groupedTextHorizontal: {
          "&:last-child": {
            borderTopLeftRadius: spacing(1),
            borderBottomLeftRadius: spacing(1),
          },
          "&:not(:last-child)": {
            borderTopRightRadius: spacing(1),
            borderBottomRightRadius: spacing(1),
            borderTopLeftRadius: spacing(1),
            borderBottomLeftRadius: spacing(1),
            borderRight: "none",
            marginRight: spacing(0.5),
          },
        },
        groupedContainedHorizontal: {
          "&:last-child": {
            borderTopLeftRadius: spacing(1),
            borderBottomLeftRadius: spacing(1),
          },
          "&:not(:last-child)": {
            borderTopRightRadius: spacing(1),
            borderBottomRightRadius: spacing(1),
            borderTopLeftRadius: spacing(1),
            borderBottomLeftRadius: spacing(1),
            borderRight: "none",
            marginRight: spacing(0.5),

            "&$disabled": {
              borderRight: "none",
            },
          },
        },
      },
    },
    MuiChip: {
      styleOverrides: {
        deleteIcon: {
          color: palette.text.disabled,
          width: spacing(3),
          height: spacing(3),
        },
        colorPrimary: {
          color: palette.text.primary,
          backgroundColor: palette.primary.light,
        },
      },
    },
    MuiDateTimePickerTabs: {
      styleOverrides: {
        tabs: {
          padding: spacing(2),
        },
      },
    },
    MuiCardHeader: {
      styleOverrides: {
        root: {
          padding: "32px 32px 0px 32px",
          "&:last-of-type": {
            padding: 32,
          },
        },
        action: {
          marginTop: 0,
          marginRight: 0,
          alignSelf: "center",
        },
      },
    },
    MuiCardContent: {
      styleOverrides: {
        root: {
          padding: "32px 32px 0px 32px",
          "&:last-of-type": {
            padding: 32,
          },
        },
      },
    },
    MuiCardActions: {
      styleOverrides: {
        root: {
          padding: "32px 32px 0px 32px",
          "&:last-of-type": {
            padding: 32,
          },
        },
      },
    },
    MuiDialog: {
      styleOverrides: {
        paperFullScreen: {
          border: "none",
        },
      },
    },
    MuiDialogTitle: {
      styleOverrides: {
        root: {
          padding: 32,
          paddingBottom: 24,
          paddingLeft: 24,
        },
      },
    },
    MuiDialogContent: {
      styleOverrides: {
        root: {
          padding: "12px 24px",
          "&:first-of-type": {
            paddingTop: 24,
          },
          "&:last-of-type": {
            paddingBottom: 24,
          },
        },
      },
    },
    MuiFilledInput: {
      styleOverrides: {
        root: {
          borderTopLeftRadius: undefined,
          borderTopRightRadius: undefined,
          borderRadius: spacing(1),

          "&$focused": {
            outlineStyle: "solid",
            outlineWidth: 2,
            outlineColor: palette.primary.main,
          },
        },
        underline: {
          "&:before": {
            content: undefined,
          },
          "&:after": {
            content: undefined,
          },
        },
        multiline: {
          lineHeight: typographyRaw.pxToRem(19), // for correct inactive label position
        },
        input: {
          "&:-webkit-autofill": {
            borderBottomLeftRadius: "inherit",
            borderBottomRightRadius: "inherit",
          },
        },
      },
    },
    MuiFormControlLabel: {
      styleOverrides: {
        root: {
          color: palette.text.primary,
          fontSize: typography.pxToRem(14),

          "&$disabled": {
            color: palette.action.disabled,
            cursor: "not-allowed",
          },
        },
      },
    },
    MuiFormLabel: {
      styleOverrides: {
        root: {
          fontSize: typographyRaw.pxToRem(16),
          lineHeight: typographyRaw.pxToRem(24),
          letterSpacing: "0.15px",
          color: palette.text.primary,
          "&$focused": {
            color: palette.text.primary,
            fontWeight: typographyRaw.fontWeightBold,
          },
        },
      },
    },
    MuiFormHelperText: {
      styleOverrides: {
        root: {
          color: palette.text.secondary,
        },
        contained: {
          marginLeft: 0,
        },
      },
    },
    MuiIconButton: {
      styleOverrides: {
        sizeSmall: {
          padding: spacing(0.5),
        },
        colorInherit: {
          color: "inherit",
          "&:hover": {
            color: "inherit",
          },
        },
      },
    },
    MuiInput: {
      styleOverrides: {
        underline: {
          "&:before": {
            borderBottom: `1px solid ${palette.primary.light}`,
          },
          "&:after": {
            borderBottom: `2px solid ${palette.primary.main}`,
          },
        },
      },
    },
    MuiInputBase: {
      styleOverrides: {
        root: {
          lineHeight: typographyRaw.pxToRem(24),
          letterSpacing: "0.0094em",
          borderColor: WHITE,
          "&:focus": {
            outline: "none",
          },
        },
      },
    },
    MuiInputLabel: {
      styleOverrides: {
        outlined: {
          color: palette.text.secondary,
        },
        filled: {
          color: palette.text.secondary,
          transform: "translate(12px, 16px) scale(1)",
          "&$shrink": {
            transform: "translate(12px, 10px)", // skip scale
            fontSize: typographyRaw.pxToRem(10),
            lineHeight: typographyRaw.pxToRem(12),
          },
          "&$marginDense": {
            transform: "translate(12px, 12px) scale(1)",
            "&$shrink": {
              transform: "translate(12px, 8px)", // skip scale
              fontSize: typographyRaw.pxToRem(10),
              lineHeight: typographyRaw.pxToRem(12),
            },
          },
        },
        animated: {
          // Add fontSize & lineHeight to transitions.
          transition:
            "color 200ms cubic-bezier(0.0, 0, 0.2, 1) 0ms,transform 200ms cubic-bezier(0.0, 0, 0.2, 1) 0ms,font-size 200ms cubic-bezier(0.0, 0, 0.2, 1) 0ms,line-height 200ms cubic-bezier(0.0, 0, 0.2, 1) 0ms",
        },
      },
    },
    MuiLink: {
      styleOverrides: {
        root: {
          fontFamily: typographyRaw.fontFamily,
          cursor: "pointer",

          "&:hover": {
            color: palette.secondary.light,
          },

          "&$underlineAlways": {
            textDecoration: "underline 2px",
          },

          "&$underlineHover": {
            "&:hover": {
              textDecoration: "underline 2px",
            },
          },

          "&$underlineNone": {
            "&:hover": {
              textDecoration: "none",
            },
          },

          "&.MuiTypography-colorPrimary": {
            color: palette.text.secondary,

            "&:hover": {
              color: palette.secondary.dark,
            },
          },
        },
      },
    },
    MuiList: {
      styleOverrides: {
        padding: {
          paddingTop: spacing(0.5),
          paddingBottom: spacing(0.5),
        },
      },
    },
    MuiListItem: {
      styleOverrides: {
        root: {
          fontFamily: typographyRaw.fontFamily,
          borderRadius: spacing(1),

          "&$selected": {
            backgroundColor: palette.secondary.light,

            "&:hover": {
              backgroundColor: palette.secondary.light,
            },
          },
        },
        button: {
          "&:not(:last-child)": {
            marginBottom: spacing(0.5),
          },
        },
        gutters: {
          "&$button": {
            width: `calc(100% - ${spacing(1)}px)`,
            marginLeft: spacing(0.5),
            marginRight: spacing(0.5),
            paddingLeft: spacing(1.5),
            paddingRight: spacing(1.5),
          },
        },
      },
    },
    MuiListItemButton: {
      styleOverrides: {
        root: {
          "&$selected": {
            backgroundColor: palette.secondary.light,

            "&:hover": {
              backgroundColor: palette.secondary.light,
            },
          },

          "&:hover": {
            backgroundColor: palette.secondary.light,
          },
        },
      },
    },
    MuiListSubheader: {
      styleOverrides: {
        root: {
          ...typography.caption,
          lineHeight: typographyRaw.pxToRem(48),
        },
      },
    },
    MuiListItemText: {
      styleOverrides: {
        primary: {
          ...typography.body2,
        },
      },
    },
    MuiListItemIcon: {
      styleOverrides: {
        root: {
          minWidth: spacing(4),
        },
      },
    },
    MuiPaper: {
      styleOverrides: {
        root: {
          border: "none",
        },
      },
    },
    MuiPickersMonth: {
      styleOverrides: {
        root: {
          color: palette.text.secondary,
          "&$selected, &:focus, &:hover": {
            color: palette.text.primary,
          },
        },
      },
    },
    MuiPopover: {
      styleOverrides: {
        paper: {
          borderRadius: 4,
        },
      },
    },
    MuiSelect: {
      styleOverrides: {
        filled: {
          lineHeight: typographyRaw.pxToRem(19), // for correct inactive label position

          "&$select:focus": {
            borderRadius: spacing(1),
          },
        },
        icon: {
          color: palette.primary.dark,
        },
      },
    },
    MuiSwitch: {
      styleOverrides: {
        root: {
          width: 40,
          height: 24,
          margin: spacing(1),
          padding: 0,
          display: "inline-flex",

          "&$sizeSmall": {
            width: 28,
            height: 16,
            margin: spacing(1),
            padding: 0,
            display: "inline-flex",
          },
        },
        switchBase: {
          padding: 4,

          "&$checked": {
            transform: "translateX(16px)",
            "& + $track": {
              opacity: 1,
            },
          },

          "&$disabled": {
            "& + $track": {
              opacity: 1, // Unset default value.
              backgroundColor: palette.action.disabledBackground,
            },
          },
        },
        thumb: {
          width: 16,
          height: 16,
          boxShadow: "none",
        },
        track: {
          borderRadius: 12,
          opacity: 1,

          "&$sizeSmall": {
            borderRadius: 8,
          },
        },
        colorPrimary: {
          "&$disabled": {
            color: palette.action.disabled,
            "& + $track": {
              opacity: 1, // Unset default value.
              backgroundColor: palette.action.disabledBackground,
            },
          },
        },
        colorSecondary: {
          "&$disabled": {
            color: palette.action.disabled,
            "& + $track": {
              opacity: 1, // Unset default value.
              backgroundColor: palette.action.disabledBackground,
            },
          },
        },
        sizeSmall: {
          "& $thumb": {
            width: 12,
            height: 12,
            boxShadow: "none",
          },
          "& $track": {
            borderRadius: 8,
          },
          "& $switchBase": {
            padding: 2,

            "&$checked": {
              transform: "translateX(12px)",
            },
          },
        },
      },
    },
    MuiSlider: {
      styleOverrides: {
        root: {
          "&$disabled": {
            color: palette.action.disabledBackground,
          },
        },
        rail: {
          height: 4,
        },
        track: {
          height: 4,
        },
        thumb: {
          "&$disabled": {
            backgroundColor: "transparent",
            width: 12,
            marginLeft: -5,
          },

          "& > span > span": {
            backgroundColor: "transparent",

            "& span": {
              lineHeight: typographyRaw.pxToRem(32),
              paddingLeft: 4,
              paddingRight: 4,
              borderRadius: spacing(0.5),
            },
          },
        },
        thumbColorPrimary: {
          backgroundColor: palette.grey[500],

          "& span": {
            backgroundColor: palette.primary.main,
            color: palette.primary.contrastText,
          },
          "&$active": {
            backgroundColor: palette.secondary.main,
          },
        },
        thumbColorSecondary: {
          backgroundColor: palette.secondary.dark,

          "& span": {
            backgroundColor: palette.secondary.dark,
            color: palette.secondary.contrastText,
          },
        },
        valueLabel: {
          top: -25,
          borderRadius: spacing(0.5),
        },
        marked: {
          marginBottom: 25,

          "@media (pointer: coarse)": {
            padding: "36px 0",
          },

          "& $thumb": {
            "&$active, &$focusVisible, &:hover": {
              boxShadow: "none",
            },
          },
          "& $thumbColorPrimary": {
            "&$active": {
              backgroundColor: palette.grey[400],
            },
          },
        },
        markLabel: {
          top: 35,

          "@media (pointer: coarse)": {
            bottom: 0,
            top: "auto",
          },
        },
        markLabelActive: {
          color: palette.text.secondary,
        },
        mark: {
          bottom: -4,
          height: 8,
          marginLeft: -1,

          "@media (pointer: coarse)": {
            bottom: 20,
          },
        },
        colorPrimary: {
          "& $mark": {
            backgroundColor: palette.primary.main,
          },
          "& $markActive": {
            backgroundColor: palette.primary.dark,
          },
        },
        colorSecondary: {
          "& $mark": {
            backgroundColor: "#29474E",
          },
          "& $markActive": {
            backgroundColor: palette.secondary.main,
          },
        },
      },
    },
    MuiTab: {
      styleOverrides: {
        root: {
          ...typography.button,
          fontSize: typographyRaw.pxToRem(16),
          lineHeight: typographyRaw.pxToRem(26),
          letterSpacing: "0.0289em",
          padding: spacing(1.25, 2),
          minWidth: 0,
          borderRadius: spacing(1),
          textTransform: "none",

          "&$disabled": {
            color: palette.text.disabled,
          },

          "&:hover": {
            backgroundColor: palette.action.hover,
          },

          "&.Mui-selected": {
            backgroundColor: palette.action.hover,
          },

          "&:not($textColorInherit)": {
            minHeight: 38,
            minWidth: 0,
          },
        },
        labelIcon: {
          minHeight: 48,
          paddingTop: spacing(0.5),
          paddingBottom: spacing(0.5),

          "& $wrapper > *:first-child": {
            marginBottom: 0,
            marginRight: spacing(0.5),
          },
        },
        wrapper: {
          flexDirection: "row",
        },
      },
    },
    MuiTabs: {
      styleOverrides: {
        root: {
          minHeight: 32,

          "& button": {
            "&:not(:last-child)": {
              marginRight: spacing(0),
            },
          },
        },
        indicator: {
          display: "none",
        },
        vertical: {
          "& button": {
            "&:not(:last-child)": {
              marginRight: 0,
              marginBottom: spacing(1),
            },
          },
        },
      },
    },
    MuiTabScrollButton: {
      styleOverrides: {
        root: {
          minHeight: 0,
          borderRadius: spacing(1),

          "&$disabled": {
            opacity: 1,
            color: palette.action.disabled,
          },

          "&:hover": {
            backgroundColor: palette.action.hover,
          },
        },
      },
    },
    MuiTable: {
      styleOverrides: {
        root: {
          borderCollapse: "separate",
          borderSpacing: `0 ${spacing(1)}px`,

          [breakpoints.down("sm")]: {
            borderSpacing: 0,
          },
        },
      },
    },
    MuiTableCell: {
      styleOverrides: {
        root: {
          ...typography.body2,
          borderBottom: "none",

          "&:first-child": {
            paddingLeft: spacing(3),
          },
          "&:last-child": {
            paddingRight: spacing(3),
          },

          [breakpoints.down("sm")]: {
            padding: spacing(2, 1),
            "&:first-child": {
              paddingLeft: 0,
            },
            "&:last-child": {
              paddingRight: 0,
            },
          },
        },
        head: {
          textTransform: "none",
          ...typography.caption,
          fontWeight: typographyRaw.fontWeightBold,
          color: palette.text.secondary,
          height: 32,
          whiteSpace: "nowrap",
          paddingBottom: spacing(1),
          paddingTop: spacing(1),
        },
        paddingCheckbox: {
          width: 46,
        },
        body: {
          [breakpoints.up("md")]: {
            "&:first-child": {
              borderTopLeftRadius: spacing(1),
              borderBottomLeftRadius: spacing(1),
            },

            "&:last-child": {
              borderTopRightRadius: spacing(1),
              borderBottomRightRadius: spacing(1),
            },
          },

          [breakpoints.down("sm")]: {
            borderBottom: `1px solid ${lighten(palette.text.primary, 0.08)}`,
          },
        },
      },
    },
    MuiTablePagination: {
      styleOverrides: {
        toolbar: {
          padding: 0,
        },
        selectRoot: {
          marginRight: spacing(3),
        },
        actions: {
          minWidth: spacing(11),
          justifyContent: "space-between",
          display: "flex",
          alignItems: "center",
          marginLeft: spacing(3),

          "& button": {
            borderRadius: spacing(1),
            padding: spacing(0.5),
            border: `2px solid ${lighten(palette.primary.contrastText, 0.23)}`,

            "&:disabled": {
              border: `2px solid ${lighten(palette.primary.contrastText, 0.1)}`,
            },
          },
        },
        select: {
          "&$select": {
            paddingRight: spacing(4),
          },

          "&:focus": {
            borderRadius: spacing(1),
          },
        },
      },
    },
    MuiTableRow: {
      styleOverrides: {
        root: {
          "&$hover": {
            cursor: "pointer",

            "&:hover": {
              backgroundColor: palette.action.hover,
            },
          },

          "&:not($head)": {
            // background: basedOnTheme(palette.type, fade(BLACK, 0.01), fade(BLACK, 0.08)),
          },

          [breakpoints.down("sm")]: {
            "&:not($head)": {
              background: "none",
            },

            "&:last-child td": {
              borderBottom: 0,
            },
          },
        },
      },
    },
    MuiTooltip: {
      styleOverrides: {
        tooltip: {
          padding: spacing(1, 2),
          fontFamily: typographyRaw.fontFamily,
          fontSize: typographyRaw.pxToRem(12),
          fontWeight: typographyRaw.fontWeightMedium,
          lineHeight: typographyRaw.pxToRem(16),
          letterSpacing: 0,
        },
      },
    },
  },
} as ThemeOptions);
