export interface AuthUserPayload {
  login: string;
  password: string;
}

export interface RegisterUserPayload {
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  password: string;
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
