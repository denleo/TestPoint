import React, { FC } from "react";

import { IconButton, InputAdornment, InputAdornmentProps } from "@mui/material";
import { useFormikContext } from "formik";

import { IconCross } from "@/common/icons";

export type FieldEndAdornmentClearProps = Omit<InputAdornmentProps, "position"> & {
  name: string | string[];
  value: unknown;
};

export const FieldEndAdornmentClear: FC<FieldEndAdornmentClearProps> = ({ name: nameOrNames, value, ...props }) => {
  const { setFieldValue } = useFormikContext();

  const handleClear = () => {
    const names = typeof nameOrNames === "string" ? [nameOrNames] : nameOrNames;
    names.forEach((name) => setFieldValue(name, value));
  };

  return (
    <InputAdornment position="end" {...props}>
      <IconButton aria-label="clear" onClick={handleClear} color="secondary" onMouseDown={handleClear}>
        <IconCross />
      </IconButton>
    </InputAdornment>
  );
};
