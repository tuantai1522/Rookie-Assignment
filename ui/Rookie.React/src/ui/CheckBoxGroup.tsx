import {
  Checkbox,
  FormControlLabel,
  FormGroup,
  FormLabel,
  Paper,
  Typography,
} from "@mui/material";
import { useState } from "react";
import { useGetAllCategoriesQuery } from "../services/categories/apiCategories";

interface Props {
  title: string;
  checked: string[];
  onChange: (event: any) => void;
}

const CheckBoxGroup = ({ title, checked, onChange }: Props) => {
  const { data, isFetching } = useGetAllCategoriesQuery();

  const [checkedItems, setCheckedItems] = useState(checked || []);

  if (isFetching) return <Typography>Loading</Typography>;

  if (data == null) return <Typography>There is nothing to show</Typography>;

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
          {data.map((category: Category) => (
            <FormControlLabel
              key={category.id}
              value={category.name}
              control={
                <Checkbox
                  checked={checkedItems.indexOf(category.name) !== -1}
                  onClick={() => handleChecked(category.name)}
                />
              }
              label={category.name}
            />
          ))}
        </FormGroup>
      </Paper>
    </>
  );
};

export default CheckBoxGroup;
