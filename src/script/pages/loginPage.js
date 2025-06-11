import { showElement, hideElement } from "../utils/utils.js";
import { userNameField } from "../header/header.js";
import { setUser } from "../utils/auth-utils.js";
import { switchPageWithAuthChecking, welcomePageName } from "../app/router.js";

const loginForm = document.getElementById("log-in-form");
const loginInput = document.getElementById('login-input');
const loginSubmitButton = document.getElementById('log-in-submit');
const loginFormInputErrorMessage = document.getElementById('input-error-message');


export const initLoginPage = () => {
    loginInput.addEventListener('change', onLoginInputChangeHandler)
    loginForm.addEventListener('submit', onLoginSubmitHandler)
}

const onLoginInputChangeHandler = (event) => {
    if(event.target.value.length > 0)
    {
        loginSubmitButton.disabled = false
    } else {
        loginSubmitButton.disabled = true
    }
}


const onLoginSubmitHandler = (event) => {
    event.preventDefault();
    const userName = loginInput.value
    if(userName.trim() != '')
    {
        setUser(userName)
        userNameField.innerHTML = userName
        hideElement(loginFormInputErrorMessage)
        switchPageWithAuthChecking(welcomePageName)
    } else
    {
        showElement(loginFormInputErrorMessage)
    }
}
