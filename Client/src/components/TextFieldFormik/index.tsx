import React, { FC, useCallback } from "react";

import { FormHelperText, TextField, TextFieldProps, useTheme } from "@mui/material";
import { Field, FieldConfig } from "formik";
import { identity } from "lodash";

import { useFieldStable } from "@api/hooks/useFieldStable";

import { FieldEndAdornmentClear } from "../FieldEndAdornmentClear";

interface Props
  extends Omit<FieldConfig, "component" | "as" | "children" | "value" | "innerRef" | "type">,
    Omit<TextFieldProps, "name" | "onChange" | "error"> {
  onChange?: (value: string) => void;
  onChangeTransform?: (raw: string) => string;
  clearable?: boolean;
  defaultValue?: number | string | unknown[];
  error?: React.ReactNode;
}

export const TextFieldFormik: FC<Props> = ({
  error,
  name,
  onChange,
  onChangeTransform,
  clearable,
  helperText,
  defaultValue,
  children,
  select,
  FormHelperTextProps = {},
  ...textFieldFormikProps
}) => {
  const [field, meta, helpers] = useFieldStable(name);
  const theme = useTheme();

  const handleChange = useCallback(
    (event: React.ChangeEvent<HTMLInputElement>) => {
      const transform: (raw: string) => string = onChangeTransform ?? identity;
      const value = transform(event.target.value);
      helpers.setValue(value);
      onChange?.(value);
    },
    [helpers, onChange, onChangeTransform]
  );

  const computedError = meta.touched && (error ?? meta.error);

  return (
    <>
      <Field
        as={TextField}
        name={name}
        sx={{
          marginBottom: 0,
        }}
        select={select}
        {...textFieldFormikProps}
        InputProps={{
          ...textFieldFormikProps?.InputProps,
          endAdornment: clearable
            ? (!select || !!(Array.isArray(field.value) ? field.value.length : field.value)) && (
                <FieldEndAdornmentClear
                  sx={{ marginRight: select ? theme.spacing(3) : undefined }}
                  name={name}
                  value={defaultValue || meta.initialValue || ""}
                />
              )
            : textFieldFormikProps?.InputProps?.endAdornment,
        }}
        onChange={handleChange}
        error={!!computedError}
        helperText={computedError}
        FormHelperTextProps={{
          sx: { marginBottom: 0 },
        }}
      >
        {children}
      </Field>
      {helperText && (
        <FormHelperText sx={{ marginTop: 0 }} {...FormHelperTextProps}>
          {helperText}
        </FormHelperText>
      )}
    </>
  );
};
