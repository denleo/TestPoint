/* eslint-disable no-restricted-imports */
import { useRef } from "react";

import { FieldHelperProps, FieldHookConfig, useField } from "formik";

export type FormikHelperPropsFixed<T> = Pick<
  FieldHelperProps<T>,
  "setTouched" | "setValue"
> &
  Pick<FieldHelperProps<string | null>, "setError">; // is | null fine here ?

/**
 * Formik helpers are not stable, always busting deps and creating re-rerenders. So use this workaround.
 *
 * @param props
 * @see https://github.com/jaredpalmer/formik/issues/2268#issuecomment-602135640
 */
export function useFieldStable<T>(props: string | FieldHookConfig<T>) {
  const [field, meta, helpers] = useField<T>(props);

  // On every render save newest helpers to latestRef.
  const latestRef = useRef({} as FormikHelperPropsFixed<T>);
  latestRef.current.setValue = helpers.setValue;
  latestRef.current.setTouched = helpers.setTouched;
  latestRef.current.setError = helpers.setError as any; // Lib types problem. See https://github.com/jaredpalmer/formik/issues/2634

  // On the first render create new function which will never change but call newest helper function.
  const stableRef = useRef<FormikHelperPropsFixed<T>>({
    setValue: (...args) => latestRef.current.setValue(...args),
    setTouched: (...args) => latestRef.current.setTouched(...args),
    setError: (...args) => latestRef.current.setError(...args),
  });

  return [field, meta, stableRef.current] as const;
}
