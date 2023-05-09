const JWT_TOKEN = "jwtToken";

export const localStorageTokenLoaded = (): boolean => {
  return !!localStorage.getItem(JWT_TOKEN);
};

export const getUserTokenFromStorage = (): string | null => {
  return sessionStorage.getItem(JWT_TOKEN) ?? localStorage.getItem(JWT_TOKEN);
};

export const setUserTokenToStorage = (token: string, useSessionStorage = false) => {
  const storage = useSessionStorage ? sessionStorage : localStorage;
  storage.setItem(JWT_TOKEN, token);
};

export const clearUserTokenFromStorage = (useSessionStorage = false) => {
  const storage = useSessionStorage ? sessionStorage : localStorage;
  storage.removeItem(JWT_TOKEN);
};
