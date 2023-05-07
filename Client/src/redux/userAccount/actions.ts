import { createAsyncThunk } from "@reduxjs/toolkit";

import {
  AdminDataResponse,
  AuthAdminPayload,
  AuthUserPayload,
  RegisterUserPayload,
  UserDataResponse,
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
};
