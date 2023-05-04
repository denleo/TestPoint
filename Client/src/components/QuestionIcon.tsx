import React, { FC } from "react";

import DoneAllOutlinedIcon from "@mui/icons-material/DoneAllOutlined";
import RadioButtonCheckedIcon from "@mui/icons-material/RadioButtonChecked";
import TextFieldsOutlinedIcon from "@mui/icons-material/TextFieldsOutlined";
import { SvgIconProps } from "@mui/material";

import { QuestionType } from "../containers/TestsPage/data";

interface Props extends SvgIconProps {
  questionType: QuestionType;
}

export const QuestionIcon: FC<Props> = ({ questionType, ...props }) => {
  if (questionType === QuestionType.SingleOption) {
    return <RadioButtonCheckedIcon {...props} />;
  }

  if (questionType === QuestionType.MultipleOptions) {
    return <DoneAllOutlinedIcon {...props} />;
  }

  if (questionType === QuestionType.TextSubstitution) {
    return <TextFieldsOutlinedIcon {...props} />;
  }

  return null;
};
