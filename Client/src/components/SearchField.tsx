import React, { FC } from "react";

import SearchIcon from "@mui/icons-material/Search";
import { TextField, TextFieldProps } from "@mui/material";

type Props = TextFieldProps;

export const SearchField: FC<Props> = ({ ...props }) => {
  return <TextField placeholder="Search by surname" {...props} InputProps={{ endAdornment: <SearchIcon /> }} />;
};
