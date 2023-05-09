import { createAsyncThunk } from "@reduxjs/toolkit";

import {
  AdminDataResponse,
  AuthAdminPayload,
  AuthUserPayload,
  RegisterUserPayload,
  UserDataResponse,
  ChangePasswordPayload,
  ChangeProfilePayload,
} from "@/api/payloads";

import { httpAction } from "@api/httpAction";

export const AccountActions = {
  requestLogin: createAsyncThunk("users/requestLogin", async (payload: AuthUserPayload, thunkAPI) => {
    const response = await httpAction("auth/user", payload);
    return response;
  }),

  requestLoginAdmin: createAsyncThunk("users/requestLoginAdmin", async (payload: AuthAdminPayload, thunkAPI) => {
    const response = await httpAction("auth/admin", payload);
    return response;
  }),

  getUserData: createAsyncThunk("users/getUserData", async () => {
    const response = await httpAction("session/user");
    return response as UserDataResponse;
  }),

  getAdminData: createAsyncThunk("users/getAdminData", async () => {
    const response = await httpAction("session/admin");
    return response as AdminDataResponse;
  }),

  registerUser: createAsyncThunk("users/registerUser", async (payload: RegisterUserPayload, thunkAPI) => {
    const response = await httpAction("users", payload);
    return response;
  }),

  changeAvatar: createAsyncThunk("user/avatar", async (avatar: string, thunkAPI) => {
    const response = await httpAction("session/user/avatar", avatar, "PATCH");
    return response;
  }),

  changePassword: createAsyncThunk("user/password", async (payload: ChangePasswordPayload, thunkAPI) => {
    const response = await httpAction("session/user/password", payload, "PATCH");
    return response;
  }),

  changeProfile: createAsyncThunk("user/profile", async (payload: ChangeProfilePayload, thunkAPI) => {
    const response = await httpAction("session/user/contactInfo", payload, "PATCH");
    return response;
  }),
};
