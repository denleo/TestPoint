import { AccountUserState } from "./userAccount/state";

export const userAccountDataSelector = (state: { userAccount: AccountUserState }) => state.userAccount;

export const userAccountNameSelector = (state: { userAccount: AccountUserState }) => state.userAccount.data?.username;

export const isAdminSelector = (state: { userAccount: AccountUserState }) => state.userAccount.isAdmin;
