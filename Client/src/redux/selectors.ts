import { AccountUserState } from "./userAccount/state";

export const userAccountDataSelector = (state: { userAccount: AccountUserState }) => state.userAccount;

export const userAccountNameSelector = (state: { userAccount: AccountUserState }) => state.userAccount.data?.username;

export const adminNameSelector = (state: { userAccount: AccountUserState }) => state.userAccount.adminData?.username;

export const isAdminSelector = (state: { userAccount: AccountUserState }) => state.userAccount.isAdmin;
