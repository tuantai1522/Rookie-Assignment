import { Visibility, VisibilityOff } from "@mui/icons-material";
import { IconButton, InputAdornment, TextField } from "@mui/material";
import { useState } from "react";
import { useFormContext } from "react-hook-form";

interface IPasswordTextField {
  name: string;
  label: string;
  defaultValue?: string;
}
const PasswordTextField = ({
  name,
  label,
  defaultValue,
}: IPasswordTextField) => {
  const [visible, setVisible] = useState(false);

  const handleClick = () => setVisible(!visible);
  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <>
      <TextField
        type={visible ? "text" : "password"}
        defaultValue={defaultValue}
        required
        {...register(name)}
        fullWidth
        label={label}
        InputLabelProps={{
          shrink: true,
        }}
        error={!!errors[name]}
        helperText={errors[name]?.message as string}
        InputProps={{
          endAdornment: (
            <InputAdornment position="end">
              <IconButton onClick={handleClick}>
                {visible ? <VisibilityOff /> : <Visibility />}
              </IconButton>
            </InputAdornment>
          ),
        }}
      />
    </>
  );
};

export default PasswordTextField;
