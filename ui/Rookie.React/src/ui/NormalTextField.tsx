import { TextField } from "@mui/material";
import { useFormContext } from "react-hook-form";

interface INormalTextField {
  label: string;
  name: string;
  defaultValue?: string | "";
  isReadOnly?: boolean;
  validate?: (value: string) => string | boolean;
}
const NormalTextField = ({
  label,
  name,
  defaultValue,
  isReadOnly,
  validate,
}: INormalTextField) => {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <>
      <TextField
        defaultValue={defaultValue}
        required
        {...register(name, {
          validate: validate ? validate : undefined,
        })}
        fullWidth
        label={label}
        InputLabelProps={{
          shrink: true,
        }}
        InputProps={{
          readOnly: isReadOnly ? true : false,
        }}
        error={!!errors[name]}
        helperText={errors[name]?.message as string}
      />
    </>
  );
};

export default NormalTextField;
