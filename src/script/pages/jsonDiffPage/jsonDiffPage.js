import { JsonComparator } from "./jsonComparator.js";
import { hideElement, showElement } from "../../utils/utils.js";

const jsonForm = document.getElementById('json-compare-form');
const oldJsonTextArea = document.getElementById("old-json");
const newJsonTextArea = document.getElementById("new-json");
const resultDiv = document.getElementById('json-compare-result')
const oldJsonSuggestionBlock = document.getElementById("old-json-suggestions")
const newJsonSuggestionBlock = document.getElementById("new-json-suggestions")

const emptyFieldError = "Поле не должно быть пустым"
const incorrectJson = "Неверный формат ввода JSON объекта"

const defaultText = "Show defference"

const button = document.getElementById("json-submit-button")

const emptynessFieldCheck = (value, suggestion) => {
    if (value.trim() == '')
    {   
        showElement(suggestion)
        suggestion.innerHTML = emptyFieldError
        return false
    }
    hideElement(suggestion)
    return true
}

const jsonParsingCheck = (value, suggestion) => {
    try {
        hideElement(suggestion)
        return JSON.parse(value);
    } catch (err) {
        showElement(suggestion)
        suggestion.innerHTML = incorrectJson
        return undefined
    }
}

const jsonDiffFormSubmitHandler = async (event) => {
    event.preventDefault()

    const oldValue = oldJsonTextArea.value
    const newValue = newJsonTextArea.value
    
    let oldFieldCheck = emptynessFieldCheck(oldValue, oldJsonSuggestionBlock)
    let newFieldCheck = emptynessFieldCheck(newValue, newJsonSuggestionBlock)

    let oldJson;
    let newJson;

    if(oldFieldCheck)
        oldJson = jsonParsingCheck(oldValue, oldJsonSuggestionBlock);
    if(newFieldCheck)
        newJson = jsonParsingCheck(newValue, newJsonSuggestionBlock);

    if(oldJson && newJson)
    {    
        button.innerText = 'loading...'
        button.disabled = true;

        const result = await JsonComparator.create(oldJson, newJson)

        button.innerText = defaultText;
        console.log(result)
        resultDiv.innerHTML = JSON.stringify(result, null, 2)

        showElement(resultDiv)
        button.disabled = false;
    }
}


export const initJsonDiffPage = () => {
    jsonForm.addEventListener('submit', jsonDiffFormSubmitHandler)
}