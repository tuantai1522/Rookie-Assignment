import CheckBoxGroup from "../../../ui/CheckBoxGroup";

interface Props {
  checked: string[];
  onChange: (event: any) => void;
}

const ProductType = ({ checked, onChange }: Props) => {
  return (
    <>
      <CheckBoxGroup
        title="Product categories"
        checked={checked}
        onChange={onChange}
      />
    </>
  );
};

export default ProductType;
