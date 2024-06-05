import {
  Checkbox,
  FormControlLabel,
  FormGroup,
  FormLabel,
  Paper,
} from "@mui/material";
import { useState } from "react";

interface Props {
  title: string;
  checked: string[];
  onChange: (event: any) => void;
}

const CheckBoxUserGroup = ({ title, checked, onChange }: Props) => {
  const [checkedItems, setCheckedItems] = useState(checked || []);

  const handleChecked = (value: string) => {
    const currentIndex = checkedItems.findIndex((item) => item === value);
    let newChecked: string[] = [];

    //chưa được chọn trước đó => thêm vào danh sách được chọn
    if (currentIndex === -1) newChecked = [...checkedItems, value];
    //đã chọn trước đó => bỏ chọn
    else newChecked = checkedItems.filter((i) => i !== value);

    setCheckedItems(newChecked);
    onChange(newChecked);
  };

  return (
    <>
      <Paper sx={{ mb: 2, p: 2 }}>
        <FormLabel component="legend">{title}</FormLabel>

        <FormGroup>
          <FormControlLabel
            key={"1"}
            value={"Customer"}
            control={
              <Checkbox
                checked={checkedItems.indexOf("Customer") !== -1}
                onClick={() => handleChecked("Customer")}
              />
            }
            label={"Customer"}
          />
          <FormControlLabel
            key={"2"}
            value={"Admin"}
            control={
              <Checkbox
                checked={checkedItems.indexOf("Admin") !== -1}
                onClick={() => handleChecked("Admin")}
              />
            }
            label={"Admin"}
          />
        </FormGroup>
      </Paper>
    </>
  );
};

export default CheckBoxUserGroup;
