import {
  Checkbox,
  FormControlLabel,
  FormGroup,
  FormLabel,
  Paper,
} from "@mui/material";

interface Props {
  options: { value: string; name: string }[];
  title: string;
  item: string;
  onChange: (event: any) => void;
}

const RadioGroup = ({ options, title, item, onChange }: Props) => {
  return (
    <>
      <Paper sx={{ mb: 2, p: 2 }}>
        <FormLabel component="legend">{title}</FormLabel>

        <FormGroup>
          {options.map((option) => (
            <FormControlLabel
              key={option.value}
              value={option.value}
              control={
                <Checkbox
                  checked={item === option.value} // Kiểm tra xem checkbox này có được chọn không
                  onChange={onChange}
                />
              }
              label={option.name}
            />
          ))}
        </FormGroup>
      </Paper>
    </>
  );
};

export default RadioGroup;
