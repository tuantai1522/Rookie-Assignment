import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Modal from "@mui/material/Modal";
import ProductForm from "./ProductForm";

import AddCircleIcon from "@mui/icons-material/AddCircle";
import { Grid } from "@mui/material";

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 1000,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};

const AddProductForm = () => {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  return (
    <div>
      <Button variant="contained" color="secondary" onClick={handleOpen}>
        <Grid container alignItems="center" justifyContent="center" gap={1}>
          <AddCircleIcon />
          Add new product
        </Grid>
      </Button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <ProductForm />
        </Box>
      </Modal>
    </div>
  );
};

export default AddProductForm;
