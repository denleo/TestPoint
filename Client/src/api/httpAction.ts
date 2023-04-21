/* eslint-disable no-console */
/* eslint-disable @typescript-eslint/no-explicit-any */
import axios, { AxiosError, AxiosRequestConfig, Method } from "axios";

import { getUserTokenFromStorage } from "./userToken";

// TODO configure api url
const instance = axios.create({
  baseURL: "https://30cd-46-28-102-154.eu.ngrok.io/api/",
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
  console.log(jwtToken);
  // if (!jwtToken) throw Error("Not logged in");
  try {
    const response = await instance.request<T>({
      method,
      url,
      data,
      ...axiosConfig(jwtToken),
      ...axiosOptions,
    });
    return response.data;
  } catch (error) {
    if (error instanceof AxiosError) {
      const err =
        error.response?.data?.response?.errors?.message ||
        error.response?.data?.message ||
        error.response?.data?.reason ||
        error.message;
      console.error(err);
      throw new Error(err);
    } else {
      throw error;
    }
  }
};
