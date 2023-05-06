import { useEffect } from "react";

import { useSelector } from "@/redux/hooks";
import { userAccountDataSelector } from "@/redux/selectors";

export const useStoreStates = () => {
  const data = useSelector(userAccountDataSelector);

  useEffect(() => {
    const interval = setInterval(() => {
      console.log(data);
    }, 5000);

    return () => clearInterval(interval);
  }, [data]);
};
