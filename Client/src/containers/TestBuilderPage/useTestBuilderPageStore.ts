import create from "zustand";
import { combine, persist } from "zustand/middleware";

// export const useStartPageStore = create(
//   persist(
//     combine(
//       {
//         pageStep: START_PAGE_STEPS.LOGIN,
//       },
//       (set) => ({
//         setPageStep: (pageStep: START_PAGE_STEPS) => set({ pageStep }),
//       })
//     ),
//     {
//       name: "start-page",
//       version: 1,
//     }
//   )
// );
