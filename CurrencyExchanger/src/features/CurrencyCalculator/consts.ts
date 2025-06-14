import { CurrencyDTO, CurrencySelectData } from "../../shared";

export const emptyCurrency: CurrencyDTO = {
    code: '',
    description: '',
    name: '',
    symbol: ''
}

export const initialCurrency: CurrencySelectData = {
  selectedCurrency: emptyCurrency,
  value: ''
};

export const TimeOptions: Intl.DateTimeFormatOptions = {
  year: 'numeric',
  month: 'long',
  day: 'numeric',
  weekday: 'long',
  hour: 'numeric',
  minute: 'numeric'
};
