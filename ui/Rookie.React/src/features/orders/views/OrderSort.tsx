import RadioGroup from "../../../ui/RadioGroup";

const sortOptions = [
  { value: "priceAsc", name: "Price - Low to High" },
  { value: "priceDesc", name: "Price - High to Low" },
];
interface Props {
  item: string;
  onChange: (event: any) => void;
}
const OrderSort = ({ item, onChange }: Props) => {
  return (
    <>
      <RadioGroup
        options={sortOptions}
        title={"Product Sorting"}
        item={item}
        onChange={onChange}
      />
    </>
  );
};

export default OrderSort;
