import { useEffect } from "react";

import { useSelector } from "@/redux/hooks";
import { allDataSelector } from "@/redux/selectors";

export const useStoreStates = () => {
  const data = useSelector(allDataSelector);

  useEffect(() => {
    const interval = setInterval(() => {
      console.log(data);
    }, 5000);

    return () => clearInterval(interval);
  }, [data]);
};
