export enum ResponseStatuses {
  Pending,
  Failure,
  Success,
}

export interface UserData {
  username: string;
  emailConfirmed: boolean;
  firstName: string;
  lastName: string;
  email: string;
  googleAuthEnabled?: boolean;
  base64Avatar?: string;
  registryDate: Date;
}

export interface AdminData {
  adminId: string;
  username: string;
  passwordReseted: boolean;
  registryDate: Date;
}

export interface AccountUserState {
  userData?: UserData;
  adminData?: AdminData;
  status: ResponseStatuses | null;
  isAdmin: boolean;
}
