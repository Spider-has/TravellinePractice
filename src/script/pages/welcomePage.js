import { switchPageWithAuthChecking, jsonDiffPageName } from "../app/router.js";

export const loggedInUserStartButton = document.getElementById("logged-in-user-start-button");

const onStartHandler =() => {
    switchPageWithAuthChecking(jsonDiffPageName);
}

export const initWelcomePage = () => {
    loggedInUserStartButton.addEventListener('click', onStartHandler)
}
