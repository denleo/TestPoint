import React, { FC, useMemo } from "react";

import { Chip, ChipProps } from "@mui/material";

import { TestDifficulty } from "@/redux/adminData/state";

interface Props extends ChipProps {
  difficulty: TestDifficulty;
}

export const TestDifficultyChip: FC<Props> = ({ difficulty, ...props }) => {
  const color = useMemo(() => {
    if (difficulty === TestDifficulty.Easy) return "success";

    if (difficulty === TestDifficulty.Medium) return "warning";

    return "info";
  }, [difficulty]);

  const text = useMemo(() => {
    if (difficulty === TestDifficulty.Easy) return "Easy";

    if (difficulty === TestDifficulty.Medium) return "Medium";

    return "Hard";
  }, [difficulty]);

  return <Chip color={color} {...props} label={text} sx={{ fontWeight: "bold" }} />;
};
