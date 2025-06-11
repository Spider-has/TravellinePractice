const loginStorageKey = "auth-session"

export const isUserLoggedIn = () => {
    return !!localStorage.getItem(loginStorageKey)
}

export const setUser = (username) => {
    localStorage.setItem(loginStorageKey, username)
}

export const getUserName = () => localStorage.getItem(loginStorageKey)

export const logout = () => {
    localStorage.removeItem(loginStorageKey)
}