/* eslint-disable no-param-reassign */
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

import { MOCK_USER } from "@/mock/user";

import { setUserTokenToStorage } from "@api/userToken";

import { AccountActions } from "./actions";
import { AccountUserState, ResponseStatuses } from "./state";

const initialState: AccountUserState = {
  // userData: MOCK_USER,
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
    clearUserData: (state) => initialState,
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
        userData: {
          ...action.payload,
          registryDate: new Date(action.payload.registryDate),
        },
        isAdmin: false,
      };
    });

    builder.addCase(AccountActions.requestLoginAdmin.pending, (state) => {
      state.status = ResponseStatuses.Pending;
    });
    builder.addCase(AccountActions.requestLoginAdmin.fulfilled, (state, action) => {
      setUserTokenToStorage(String(action.payload));
      state.status = ResponseStatuses.Success;
    });

    builder.addCase(AccountActions.getAdminData.pending, (state) => {
      state.status = ResponseStatuses.Pending;
    });
    builder.addCase(AccountActions.getAdminData.fulfilled, (state, action) => {
      return {
        status: ResponseStatuses.Success,
        adminData: {
          ...action.payload,
          registryDate: new Date(action.payload.registryDate),
        },
        isAdmin: true,
      };
    });
  },
});

export const { setUserData, clearUserData } = userAccountSlice.actions;

export default userAccountSlice.reducer;
