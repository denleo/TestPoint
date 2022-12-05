import React from "react";

import { Button, Typography } from "@mui/material";

import { useDispatch, useSelector } from "@/redux/hooks";
import { userAccountDataSelector } from "@/redux/selectors";
import { setUserData } from "@/redux/userAccount/reducer";

const Max = () => {
  const dispatch = useDispatch();
  const data = useSelector(userAccountDataSelector);

  return (
    <>
      <Typography variant="body2">{JSON.stringify(data)}</Typography>
      <Button
        variant="contained"
        onClick={() => {
          dispatch(
            setUserData({
              avatar: "avatar",
              email: "gmail",
              emailConfirmed: true,
              firstName: "max",
              lastName: "rojkov",
              userName: "maxRojkov",
            })
          );
        }}
      >
        SetData
      </Button>
    </>
  );
};

export default Max;
