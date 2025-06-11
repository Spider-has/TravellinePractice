import { switchPage, logInPageName, switchPageWithAuthChecking, welcomePageName } from "../app/router.js";
import { getUserName, logout } from "../utils/auth-utils.js";


export const loginButton = document.getElementById("log-in-button");
export const loggedInUserArea = document.getElementById("logged-in-user-area");
export const logoutButton = document.getElementById("log-out-button");
export const userNameField = document.getElementById("user-name-field");

export const initHeader = () => {
    userNameField.innerHTML = getUserName();
    logoutButton.addEventListener('click', onLogoutHandler)
    loginButton.addEventListener('click', onLoginHandler)
}

const onLogoutHandler = () => {
    logout();
    switchPageWithAuthChecking(welcomePageName);
}

const onLoginHandler = () => {
    switchPage(logInPageName)
}