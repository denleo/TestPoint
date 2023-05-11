import create from "zustand";
import { combine } from "zustand/middleware";

export const useResultsPageStore = create(
  combine(
    {
      testId: "",
      userId: "",
    } as { testId: string; userId: string },
    (set) => ({
      setTest: (testId: string, userId?: string) => set({ testId, userId }),
    })
  )
);
