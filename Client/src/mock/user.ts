import { UserData } from "@/redux/userAccount/state";

export const MOCK_USER: UserData = {
  base64Avatar: undefined,
  email: "testuser@gmail.com",
  emailConfirmed: true,
  registryDate: new Date(2023, 4, 25),
  firstName: "FirstName",
  lastName: "LastName",
  username: "maxtesting",
  googleAuthEnabled: true,
};
