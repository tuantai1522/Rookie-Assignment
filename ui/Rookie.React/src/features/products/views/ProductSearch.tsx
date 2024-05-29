import SearchBox from "../../../ui/SearchBox";

interface Props {
  textValue: string;
  onChange: (event: any) => void;
}
const ProductSearch = ({ textValue, onChange }: Props) => {
  return (
    <>
      <SearchBox
        textValue={textValue}
        title="Product Search"
        onChange={onChange}
      />
    </>
  );
};

export default ProductSearch;
