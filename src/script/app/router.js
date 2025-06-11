import { isUserLoggedIn } from "../utils/auth-utils.js";
import { hideElement, showElement } from "../utils/utils.js";
import { loginButton, loggedInUserArea } from "../header/header.js";
import { loggedInUserStartButton } from "../pages/welcomePage.js";

import { initWelcomePage } from "../pages/welcomePage.js";
import { initHeader } from "../header/header.js";
import { initLoginPage } from "../pages/loginPage.js";
import { initJsonDiffPage } from "../pages/jsonDiffPage/jsonDiffPage.js"; 

export const welcomePageName = "welcome-page";
export const logInPageName = "log-in-page";
export const jsonDiffPageName = "json-diff-page";

const welcomePage = document.getElementById(welcomePageName)
const logInPage = document.getElementById(logInPageName)
const jsonDiffPage = document.getElementById(jsonDiffPageName)

const pages = [
    {
        name: welcomePageName,
        pageElement: welcomePage
    },
    {
        name: logInPageName,
        pageElement: logInPage
    },
    {
        name: jsonDiffPageName,
        pageElement: jsonDiffPage
    }
]

export const switchPage = (pageName) => {
    pages.forEach((el) => {
        if (el.name == pageName)
            showElement(el.pageElement);
        else 
            hideElement(el.pageElement);
    } )
}

export const switchPageWithAuthChecking = (pageName) => {
    switchPage(pageName);
    userAuthCheck()
}

const userAuthCheck = () => {
    if(isUserLoggedIn())
    {
        hideElement(loginButton);
        showElement(loggedInUserArea);
        showElement(loggedInUserStartButton);
    } else 
    {
        hideElement(loggedInUserArea);
        hideElement(loggedInUserStartButton);
        showElement(loginButton);
    }
}

export const initRouter = () => {
    initWelcomePage()
    initHeader()
    initLoginPage()
    initJsonDiffPage()

    userAuthCheck();
    switchPage(welcomePageName);
}