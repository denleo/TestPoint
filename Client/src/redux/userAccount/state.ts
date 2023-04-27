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
  avatar?: string;
  creationDate: Date;
}

export interface AccountUserState {
  data: UserData;
  status: ResponseStatuses | null;
  isAdmin: boolean;
}
