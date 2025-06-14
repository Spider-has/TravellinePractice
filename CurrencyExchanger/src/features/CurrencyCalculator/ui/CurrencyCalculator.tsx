import { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import {
  addCurrencyPair,
  clearCurrencyStore,
  CurrencyDTO,
  CurrencyPair,
  CurrencySelectData,
  getAllCurrencyPairs,
  ValueCurrencySelectInputsArea
} from '../../../shared';
import { CurrenciesInfoArea } from './currencyInfoArea/CurrencyInfoArea';
import { Box, Button, CircularProgress, Stack, Typography } from '@mui/material';
import { getCurrenciesList, getCurrenciesRatio } from '../api';
import './CurrencyCalculator.css';
import { emptyCurrency, initialCurrency, TimeOptions } from '../consts';

const LoadingData = () => {
  return (
    <Box display={'flex'} flexDirection={'column'} alignItems={'center'} gap="20px">
      <Typography variant="h4">LOADING...</Typography>
      <CircularProgress size={60} />
    </Box>
  );
};

type ErrorMessageProps = {
  message: string;
};
const ErrorMessage = (props: ErrorMessageProps) => {
  return (
    <Typography variant="h4" color="error">
      Похоже, возникла ошибка: {props.message}
    </Typography>
  );
};

export const CurrencyCalculator = () => {
  const [currencyList, setCurrencyList] = useState<CurrencyDTO[]>([]);
  const [selectedMainCurrency, setSelectedMainCurrency] = useState<CurrencySelectData>(initialCurrency);
  const [selectedSecondaryCurrency, setSelectedSecondaryCurrency] = useState<CurrencyDTO>(emptyCurrency);
  const [price, setPrice] = useState(0);

  const [dataLoading, setLoading] = useState(false);
  const [loadingError, setError] = useState<string | null>();

  const time = useRef<Date>(new Date());

  const onCurrencyChange = useCallback(async (mainCurr: string, secondaryCurr: string) => {
    const response = await getCurrenciesRatio(mainCurr, secondaryCurr, time.current);
    if (response) {
      const actualPrice = response[response.length - 1].price;
      setPrice(actualPrice);
    }
  }, []);

  useEffect(() => {
    const setCurrencies = async () => {
      setLoading(true);
      try {
        const data = await getCurrenciesList();
        if (data && data.length > 1) {
          setSelectedMainCurrency({ selectedCurrency: data[0], value: '1' });
          setSelectedSecondaryCurrency(data[1]);
          setCurrencyList(data);
          onCurrencyChange(data[0].code, data[1].code);
        }
      } catch (err) {
        setError('Could not get data from server');
        console.error(err);
      }

      setLoading(false);
    };
    setCurrencies();
  }, [onCurrencyChange]);

  const currentValuesPrice = useMemo(() => {
    return price * Number(selectedMainCurrency.value);
  }, [price, selectedMainCurrency.value]);

  const [filters, setFilters] = useState<CurrencyPair[]>(getAllCurrencyPairs());

  return (
    <>
      <header>
        {filters.map((el) => (
          <Button
            size="small"
            variant={'contained'}
            key={el.fisrt.code + el.second.code}
            onClick={() => {
              setSelectedMainCurrency({ ...selectedMainCurrency, selectedCurrency: el.fisrt });
              setSelectedSecondaryCurrency(el.second);
              onCurrencyChange(el.fisrt.code, el.second.code);
            }}
          >{`${el.fisrt.code}/${el.second.code}`}</Button>
        ))}
        <Button
          variant={'contained'}
          onClick={() => {
            clearCurrencyStore();
            setFilters([]);
          }}
          color="error"
        >
          Clear Filters
        </Button>
      </header>
      <section className="currency-exchanger-area">
        {dataLoading && <LoadingData />}
        {!dataLoading && loadingError && <ErrorMessage message={loadingError} />}
        {!dataLoading && loadingError == null && (
          <>
            <Typography variant={'subtitle1'}>
              {selectedMainCurrency.value} {selectedMainCurrency.selectedCurrency.name} is
            </Typography>
            <Box display="flex" flexDirection={'row'} justifyContent={'space-between'}>
              <Typography variant={'h2'}>
                {currentValuesPrice} {selectedSecondaryCurrency.name}
              </Typography>
              <Button
                onClick={() => {
                  const pair = { fisrt: selectedMainCurrency.selectedCurrency, second: selectedSecondaryCurrency };
                  const exists = filters.find((el) => el.fisrt == pair.fisrt && el.second == pair.second);
                  if (!exists) {
                    setFilters([...filters, pair]);
                    addCurrencyPair(pair);
                  }
                }}
                size="small"
                variant={'contained'}
              >
                Save Filter
              </Button>
            </Box>
            <Box display="flex" flexDirection={'row'} gap="40px">
              <Stack display="flex" flexDirection="column" gap="20px">
                <Typography variant={'subtitle2'}>{time.current.toLocaleDateString('en-US', TimeOptions)}</Typography>
                <ValueCurrencySelectInputsArea
                  currencySymbol={selectedMainCurrency.selectedCurrency.symbol}
                  currencyValue={selectedMainCurrency.value}
                  currencyCode={selectedMainCurrency.selectedCurrency.code}
                  setValue={(value: string) => {
                    setSelectedMainCurrency({ ...selectedMainCurrency, value: value });
                  }}
                  setNewCurrency={(currencyCode: string) => {
                    const currency = currencyList.find((el) => el.code == currencyCode);
                    if (currency) {
                      setSelectedMainCurrency({ ...selectedMainCurrency, selectedCurrency: currency });
                      onCurrencyChange(currencyCode, selectedSecondaryCurrency.code);
                    }
                  }}
                  currencyCodesList={currencyList.map((el) => el.code)}
                />
                <ValueCurrencySelectInputsArea
                  currencySymbol={selectedSecondaryCurrency.symbol}
                  currencyValue={currentValuesPrice.toString()}
                  currencyCode={selectedSecondaryCurrency.code}
                  setNewCurrency={(currencyCode: string) => {
                    const currency = currencyList.find((el) => el.code == currencyCode);
                    if (currency) {
                      setSelectedSecondaryCurrency(currency);
                      onCurrencyChange(selectedMainCurrency.selectedCurrency.code, currencyCode);
                    }
                  }}
                  currencyCodesList={currencyList.map((el) => el.code)}
                />
              </Stack>
            </Box>
            <CurrenciesInfoArea
              mainCurrency={selectedMainCurrency.selectedCurrency}
              secondaryCurrency={selectedSecondaryCurrency}
            />
          </>
        )}
      </section>
    </>
  );
};
