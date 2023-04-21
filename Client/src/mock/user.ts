import { UserData } from "@/redux/userAccount/state";

export const MOCK_USER: UserData = {
  avatar: undefined,
  email: "testuser@gmail.com",
  emailConfirmed: true,
  creationDate: new Date(2023, 4, 25),
  firstName: "FirstName",
  lastName: "LastName",
  username: "maxtesting",
};
