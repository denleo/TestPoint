/* eslint-disable no-console */
/* eslint-disable @typescript-eslint/no-explicit-any */
import axios, { AxiosError, AxiosRequestConfig, Method } from "axios";

import { getUserTokenFromStorage } from "./userToken";

// TODO configure api url
const instance = axios.create({
  baseURL: "api/",
});

const axiosConfig = (token: string | null) => ({
  headers: {
    ...(token && { Authorization: `Bearer ${token}` }),
    "ngrok-skip-browser-warning": true,
  },
});

export const httpAction = async <T = any>(
  url: string,
  data?: any,
  method: Method = data ? "post" : "get",
  axiosOptions: AxiosRequestConfig = {}
) => {
  const jwtToken = getUserTokenFromStorage();
  // if (!jwtToken) throw Error("Not logged in");
  const headers = { ...axiosConfig(jwtToken).headers, ...axiosOptions.headers };
  try {
    const response = await instance.request<T>({
      method,
      url,
      data,
      ...axiosConfig(jwtToken),
      ...axiosOptions,
      ...{ headers },
    });
    return response.data;
  } catch (error) {
    if (error instanceof AxiosError) {
      const err =
        error.response?.data?.error ||
        // error.response?.errors?.message ||
        error.response?.data?.message ||
        error.response?.data?.reason ||
        error.message;
      throw new Error(err);
    } else {
      throw error;
    }
  }
};
