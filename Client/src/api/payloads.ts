export interface AuthUserPayload {
  login: string;
  password: string;
}

export interface AuthAdminPayload {
  username: string;
  password: string;
}

export interface RegisterUserPayload {
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  password: string;
}

export interface ChangePasswordPayload {
  oldPassword: string;
  newPassword: string;
}

export interface ChangeProfilePayload {
  email: string;
  firstName: string;
  lastName: string;
}

export interface UserDataResponse {
  userId: string;
  username: string;
  firstName: string;
  lastName: string;
  email: string;
  emailConfirmed: true;
  passwordReseted: true;
  registryDate: string;
  base64Avatar: string;
}

export interface AdminDataResponse {
  adminId: string;
  username: string;
  passwordReseted: boolean;
  registryDate: string;
}
