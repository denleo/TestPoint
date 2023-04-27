/* eslint-disable no-param-reassign */
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

import { MOCK_USER } from "@/mock/user";

import { setUserTokenToStorage } from "@api/userToken";

import { AccountActions } from "./actions";
import { AccountUserState, ResponseStatuses } from "./state";

const initialState: AccountUserState = {
  data: MOCK_USER,
  status: null,
  isAdmin: false,
};

export const userAccountSlice = createSlice({
  name: "account_user",
  initialState,
  reducers: {
    setUserData: (state, action: PayloadAction<AccountUserState>) => ({
      ...state,
      ...action.payload,
    }),
    clearUserData: (state) => ({
      ...state,
      ...initialState,
    }),
  },
  extraReducers: (builder) => {
    builder.addCase(AccountActions.requestLogin.pending, (state) => {
      state.status = ResponseStatuses.Pending;
    });
    builder.addCase(AccountActions.requestLogin.fulfilled, (state, action) => {
      setUserTokenToStorage(String(action.payload));
      state.status = ResponseStatuses.Success;
    });

    builder.addCase(AccountActions.getUserData.pending, (state) => {
      state.status = ResponseStatuses.Pending;
    });
    builder.addCase(AccountActions.getUserData.fulfilled, (state, action) => {
      return {
        status: ResponseStatuses.Success,
        data: {
          ...action.payload,
          username: action.payload.username,
        },
        isAdmin: false,
      };
    });
  },
});

export const { setUserData, clearUserData } = userAccountSlice.actions;

export default userAccountSlice.reducer;
