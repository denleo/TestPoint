import { FormikErrors } from "formik";
import * as yup from "yup";

import { SignUpUserFormValues } from "../common";

const lettersNumbers = (str: string) => /^[a-z0-9]*$/i.test(str);
const onlyLetters = (str: string) => /^[a-z]*$/i.test(str);

export const validationSchema = yup.object({
  email: yup.string().email("Invalid email").required("Required"),
  password: yup
    .string()
    .required("Required")
    .min(10, "At least 10 characters")
    .matches(/[A-Z]/, "1 UPPERCASE")
    .matches(/[a-z]/, "1 lowercase")
    .matches(/[0-9]/, "1 d1g1t"),
  repeatPassword: yup
    .string()
    .required("Required")
    .oneOf([yup.ref("password"), ""], "Passwords must match"),
});

export const validateForm = ({
  username,
  firstName,
  lastName,
}: SignUpUserFormValues) => {
  const errors: FormikErrors<SignUpUserFormValues> = {};

  // validate username
  const usernameCharacters = username.length >= 3 && username.length <= 10;
  const usernameLettersNumbers = lettersNumbers(username);

  if (!usernameCharacters)
    errors.username = "Between 3 and 10 characters long.";
  if (!usernameLettersNumbers)
    errors.username = "Only letters and numbers, no special characters.";

  // validate names
  const firstNameCharacters = firstName.length > 0;
  const firstNameLettersNumbers = onlyLetters(firstName);

  if (!firstNameCharacters) errors.firstName = "Can't be empty.";
  if (!firstNameLettersNumbers)
    errors.firstName = "Only letters, no numbers and special characters.";

  const lastNameCharacters = lastName.length > 0;
  const lastNameLettersNumbers = onlyLetters(lastName);

  if (!lastNameCharacters) errors.lastName = "Can't be empty.";
  if (!lastNameLettersNumbers)
    errors.lastName = "Only letters, no numbers and special characters.";

  return errors;
};
