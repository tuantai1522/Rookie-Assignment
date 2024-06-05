import CheckBoxCategoryGroup from "../../../ui/CheckBoxCategoryGroup";

interface Props {
  checked: string[];
  onChange: (event: any) => void;
}

const ProductType = ({ checked, onChange }: Props) => {
  return (
    <>
      <CheckBoxCategoryGroup
        title="Product categories"
        checked={checked}
        onChange={onChange}
      />
    </>
  );
};

export default ProductType;
