import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";

import DeleteIcon from "@mui/icons-material/Delete";
import { Button, Grid, IconButton } from "@mui/material";
import { useState } from "react";
import { toast } from "react-toastify";
import { useDeleteImageMutation } from "../../../services/images/apiImages";
import { useGetProductQuery } from "../../../services/products/apiProducts";

interface Props {
  imageId: string;
  productId: string;
}
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

const DeleteImageForm = ({ imageId, productId }: Props) => {
  const [open, setOpen] = useState(false);

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const [deleteImage, { isLoading }] = useDeleteImageMutation();
  const { refetch } = useGetProductQuery(productId);

  const handleSubmit = async () => {
    try {
      await deleteImage(imageId)
        .unwrap()
        .then(() => {
          toast.success("Delete image successfully");
          refetch();
          handleClose();
        })
        .catch((error) => {
          toast.error(error.data.error);
        });
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <div>
      <IconButton onClick={handleOpen} color="primary">
        <DeleteIcon />
      </IconButton>
      <Modal open={open} onClose={handleClose}>
        <Box sx={style}>
          <Typography variant="h4">Do you want to delete this image</Typography>

          <Grid
            container
            sx={{ marginTop: "2rem" }}
            gap={2}
            alignItems="center"
            justifyContent="flex-end"
          >
            <Grid item>
              <Button
                variant="contained"
                color="success"
                onClick={handleSubmit}
                disabled={isLoading}
              >
                Yes
              </Button>
            </Grid>
            <Grid item>
              <Button
                disabled={isLoading}
                variant="contained"
                color="error"
                onClick={handleClose}
              >
                No
              </Button>
            </Grid>
          </Grid>
        </Box>
      </Modal>
    </div>
  );
};

export default DeleteImageForm;
