import { createAsyncThunk } from "@reduxjs/toolkit";

import { AuthUserPayload } from "@/api/payloads";

import { httpAction } from "@api/httpAction";

import { UserData } from "./state";

export const AccountActions = {
  requestLogin: createAsyncThunk("users/requestLogin", async (payload: AuthUserPayload, thunkAPI) => {
    const response = await httpAction("auth/user", payload);
    return response;
  }),
  getUserData: createAsyncThunk("users/getUserData", async () => {
    const response = await httpAction("session/user"); // TODO add types
    return response as UserData;
  }),
};
