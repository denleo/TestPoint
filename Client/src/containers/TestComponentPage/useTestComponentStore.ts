import create from "zustand";
import { combine } from "zustand/middleware";

import { TestData } from "@/redux/adminData/state";

interface StoreStates {
  test: TestData | null;
  questionIndex: number;
  selectedAnswers: Map<number, string | string[]>;
}

export const useTestComponentStore = create(
  combine(
    {
      test: null,
      questionIndex: 0,
      selectedAnswers: new Map(),
    } as StoreStates,
    (set) => ({
      setTest: (test: TestData | null) => set({ test }),
      setQuestionIndex: (questionIndex: number) => set({ questionIndex }),
      setSelectedAnswers: (selectedAnswers: Map<number, string | string[]>) => set({ selectedAnswers }),
    })
  )
);
