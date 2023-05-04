import create from "zustand";
import { combine } from "zustand/middleware";

import { TestData } from "../TestsPage/data";

export const useTestBuilderStore = create(
  combine(
    {
      test: null,
    } as { test: TestData | null },
    (set) => ({
      setTest: (test: TestData | null) => set({ test }),
    })
  )
);
