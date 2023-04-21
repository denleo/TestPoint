export interface LoginUserFormValues {
  username: string;
  password: string;
  rememberMe: boolean;
}

export interface SignUpUserFormValues {
  username: string;
  firstName: string;
  lastName: string;
  password: string;
  repeatPassword: string;
  email: string;
}

export enum LOGIN_TAB {
  USER = 1,
  ADMIN = -1,
}

export enum START_PAGE_STEPS {
  LOGIN = "login",
  SIGN_UP = "sign up",
}

export enum SIGN_UP_STEPS {
  USERNAME = 0,
  CREDENTIALS = 1,
  NAMES = 2,
}
