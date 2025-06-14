import { Box, InputAdornment, MenuItem, TextField } from '@mui/material';

type ValueCurrencySelectInputsAreaProps = {
  currencySymbol: string;
  currencyValue: string;
  currencyCode: string;
  setValue?: (value: string) => void;
  setNewCurrency: (currency: string) => void;
  currencyCodesList: string[];
};

export const ValueCurrencySelectInputsArea = (props: ValueCurrencySelectInputsAreaProps) => {
  return (
    <Box display={'flex'} flexDirection={'row'} alignItems={'baseline'} gap="30px">
      <TextField
        size="small"
        sx={{ maxWidth: `150px` }}
        variant={'standard'}
        onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
          if (props.setValue) props.setValue(event.target.value);
        }}
        value={props.currencyValue}
        slotProps={{
          input: {
            startAdornment: <InputAdornment position="start">{props.currencySymbol}</InputAdornment>
          }
        }}
      />
      <TextField
        onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
          props.setNewCurrency(event.target.value);
        }}
        value={props.currencyCode}
        size="small"
        select
        label="currency"
        defaultValue="EUR"
        helperText="Please select your currency"
      >
        {props.currencyCodesList.map((el) => (
          <MenuItem key={el} value={el}>
            {el}
          </MenuItem>
        ))}
      </TextField>
    </Box>
  );
};
