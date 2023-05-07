import { configureStore } from "@reduxjs/toolkit";
import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";
import thunk from "redux-thunk";

import userAccountReducer from "./userAccount/reducer";

const persistConfig = {
  key: "root",
  storage,
  blacklist: ["apiProductSlice"],
};

const persistedUserReducer = persistReducer(persistConfig, userAccountReducer);

export const store = configureStore({
  reducer: {
    userAccount: persistedUserReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: false,
    }).concat(thunk),
});

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>;
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch;
