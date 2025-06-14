export type PriceDTO = {
  dateTime: string;
  paymentCurrencyCode: string;
  price: number;
  purchasedCurrencyCode: string;
};

export type CurrencyDTO ={
  code: string;
  description: string;
  name: string;
  symbol: string;
}

export type CurrencySelectData = {
  selectedCurrency: CurrencyDTO;
  value: string;
};