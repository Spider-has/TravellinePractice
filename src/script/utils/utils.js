export const hideElement = (elem) => {
    if(!elem.classList.contains("hidden-element"))
        elem.classList.add("hidden-element");
}

export const showElement = (elem) => {
    if(elem.classList.contains("hidden-element"))
        elem.classList.remove("hidden-element");
}
