import { configureStore } from "@reduxjs/toolkit";
import thunk from "redux-thunk";

import userAccountReducer from "./userAccount/reducer";

export const store = configureStore({
  reducer: {
    userAccount: userAccountReducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(thunk),
});

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>;
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch;
