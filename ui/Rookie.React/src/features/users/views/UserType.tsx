import CheckBoxUserGroup from "../../../ui/CheckBoxUserGroup";

interface Props {
  checked: string[];
  onChange: (event: any) => void;
}

const UserType = ({ checked, onChange }: Props) => {
  return (
    <>
      <CheckBoxUserGroup
        title="User roles"
        checked={checked}
        onChange={onChange}
      />
    </>
  );
};

export default UserType;
