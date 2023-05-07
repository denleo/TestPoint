import create from "zustand";
import { combine } from "zustand/middleware";

import { generateUniqueId } from "../../api/generateId";

export enum NotificationType {
  Info = "info",
  Error = "error",
  Warning = "warning",
  Success = "success",
}

export interface Notification {
  id: string;
  type: NotificationType;
  message: string;
}

interface NotificationStoreValues {
  notifications: Notification[];
}

export const useNotificationStore = create(
  combine(
    {
      notifications: [],
    } as NotificationStoreValues,
    (set, get) => ({
      notify: (message: string, type?: NotificationType) => {
        const id = generateUniqueId();
        set({
          notifications: [
            ...get().notifications,
            {
              id,
              message,
              type: type ?? NotificationType.Info,
            },
          ],
        });
        setTimeout(() => {
          set({ notifications: get().notifications.filter((item) => item.id !== id) });
        }, 2000);
      },
    })
  )
);
