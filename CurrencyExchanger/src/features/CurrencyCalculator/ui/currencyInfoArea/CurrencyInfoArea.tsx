import { Box, Divider, Stack, Typography } from '@mui/material';
import { CurrencyDTO } from '../../../../shared';
import { useState } from 'react';
import { ArrowUpward } from '@mui/icons-material';

type CurrenciesInfoArea = {
  mainCurrency: CurrencyDTO;
  secondaryCurrency: CurrencyDTO;
};

const CurrencyInfoBox = (props: CurrencyDTO) => {
  return (
    <Box display={'flex'} flexDirection={'column'} gap="10px">
      <Typography variant={'h6'} fontWeight={700}>
        {props.name}-{props.code}-{props.symbol}
      </Typography>
      <Typography variant={'body1'}>{props.description}</Typography>
    </Box>
  );
};

export const CurrenciesInfoArea = (props: CurrenciesInfoArea) => {
  const [showInfo, setShow] = useState(false);

  return (
    <>
      <Divider>
        <Box
          display={'flex'}
          flexDirection={'row'}
          alignItems={'center'}
          gap="5px"
          onClick={() => setShow(!showInfo)}
          sx={{ borderRadius: `25px`, backgroundColor: '#F2F3F5', padding: `10px 20px` }}
        >
          <Typography variant="body2">
            {props.mainCurrency.code}/{props.secondaryCurrency.code} about
          </Typography>
          <ArrowUpward />
        </Box>
      </Divider>
      {showInfo && (
        <Stack display="flex" flexDirection={'column'} gap="25px">
          <CurrencyInfoBox {...props.mainCurrency} />
          <CurrencyInfoBox {...props.secondaryCurrency} />
        </Stack>
      )}
    </>
  );
};
