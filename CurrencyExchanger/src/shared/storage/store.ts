import { CurrencyDTO } from "../model/model"

const currencyKey = 'currencies-filters'

export type CurrencyPair = {
    fisrt: CurrencyDTO,
    second: CurrencyDTO,
}
export const addCurrencyPair = (pair: CurrencyPair) => {
    const data = localStorage.getItem(currencyKey);
    if(data)
    {
        const pairs = JSON.parse(data) as CurrencyPair[];
        const newPairs = [...pairs, pair];
        localStorage.setItem(currencyKey, JSON.stringify(newPairs));
        
    }
    else 
        localStorage.setItem(currencyKey, JSON.stringify([pair]));
}

export const getAllCurrencyPairs = () => {
     const data = localStorage.getItem(currencyKey);
     if(data)
        return JSON.parse(data);
    return []
}

export const clearCurrencyStore = () => {
    localStorage.removeItem(currencyKey);   
}