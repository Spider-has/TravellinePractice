import { CurrencyDTO, getCurrenciesApi, getCurrencyRatioApi, PriceDTO } from "../../shared";

export const getCurrenciesList = async (): Promise<CurrencyDTO[] | undefined> => {
  const response = await fetch(getCurrenciesApi, { method: 'GET' });
  if (response.ok) {
    const data = await response.json();
    return data;
  }
  return undefined;
};

export const getCurrenciesRatio = async (
  mainCode: string,
  secondaryCode: string,
  time: Date
): Promise<PriceDTO[] | undefined> => {
  time.setMinutes(time.getMinutes() - 1);
  const requestUrl = `${getCurrencyRatioApi}?PaymentCurrency=${mainCode}&PurchasedCurrency=${secondaryCode}&FromDateTime=${time.toISOString()}`;
  console.log(requestUrl);
  const response = await fetch(requestUrl, {
    method: 'GET'
  });
  if (response.ok) {
    const data = await response.json();
    console.log(data);
    return data;
  }
  return undefined;
};
