import create from "zustand";
import { combine, persist } from "zustand/middleware";

export const useSidebarStore = create(
  persist(
    combine(
      {
        isMinimized: false,
      },
      (set, get) => ({
        toggleIsMinimized: (isMinimized?: boolean) => set({ isMinimized: isMinimized ?? !get().isMinimized }),
      })
    ),
    {
      name: "sidebar",
      version: 1,
    }
  )
);
