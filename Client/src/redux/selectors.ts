import { AccountUserState, UserData } from "./userAccount/state";

export const allDataSelector = (state: { userAccount: AccountUserState }) => state.userAccount;

export const userDataSelector = (state: { userAccount: AccountUserState }): UserData | null => {
  if (!state.userAccount.userData) return null;

  return {
    ...state.userAccount.userData,
    registryDate: new Date(state.userAccount.userData.registryDate),
  };
};

export const userAccountNameSelector = (state: { userAccount: AccountUserState }) =>
  state.userAccount.userData?.username;

export const adminNameSelector = (state: { userAccount: AccountUserState }) => state.userAccount.adminData?.username;

export const userAvatarSelector = (state: { userAccount: AccountUserState }) =>
  state.userAccount.userData?.base64Avatar;

export const isAdminSelector = (state: { userAccount: AccountUserState }) => state.userAccount.isAdmin;

export const isEmailConfirmedSelector = (state: { userAccount: AccountUserState }) =>
  state.userAccount.userData?.emailConfirmed;

export const isGoogleAuthenticatedSelector = (state: { userAccount: AccountUserState }) =>
  state.userAccount.userData?.googleAuthenticated;
