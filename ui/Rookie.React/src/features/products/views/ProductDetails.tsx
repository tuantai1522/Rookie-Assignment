import Typography from "@mui/material/Typography";
import { useParams } from "react-router-dom";
import { useGetProductQuery } from "../../../services/products/apiProducts";
import {
  Grid,
  CardMedia,
  Divider,
  TableContainer,
  Table,
  TableBody,
  TableRow,
  TableCell,
  TableHead,
  Checkbox,
  TextField,
  Button,
} from "@mui/material";
import { formatCurrency } from "../../../utils/helper";

import { useState, useEffect, ChangeEvent } from "react";
import { useAddImageMutation } from "../../../services/images/apiImages";
import { toast } from "react-toastify";
import DeleteImageForm from "./DeleteImageForm";
import { useUpdateMainImageMutation } from "../../../services/mainImages/apiMainImages";

const ProductDetails = () => {
  const [file, setFile] = useState<File | null>(null);

  const { id } = useParams();

  if (!id) return;

  const { data: product, isFetching, refetch } = useGetProductQuery(id);

  const [mainImageUrl, setMainImageUrl] = useState<string>("");

  const [addImage, { isLoading: isAdding }] = useAddImageMutation();
  const [updateMainImage] = useUpdateMainImageMutation();

  useEffect(() => {
    if (product && product.mainImageUrl) {
      setMainImageUrl(product.mainImageUrl);
    }
  }, [product]);

  const handleMainImage = async (imageId: string, imageUrl: string) => {
    try {
      const formData = new FormData();
      formData.append("ProductId", product?.id ?? "id");
      formData.append("ImageId", imageId);

      await updateMainImage(formData)
        .unwrap()
        .then(() => {
          toast.success("Changing main image successfully");
          setMainImageUrl(imageUrl);
          refetch();
        })
        .catch((error) => {
          toast.error(error.data.error);
        });
    } catch (err) {
      console.log(err);
    }
  };

  //Adding new image
  const handleAddImage = async () => {
    try {
      if (!file) {
        toast.error("Please choose one image.");
        return;
      }

      const formData = new FormData();
      formData.append("ProductId", product?.id ?? "id");
      formData.append("FileImage", file);

      await addImage(formData)
        .unwrap()
        .then(() => {
          setFile(null);
          toast.success("Adding new image successfully");
          refetch();
        })
        .catch((error) => {
          toast.error(error.data.error);
        });
    } catch (err) {
      console.log(err);
    }
  };
  const handleChooseImage = (e: ChangeEvent<HTMLInputElement>) => {
    const selectedFile = e.target.files?.[0]; // Get the selected file
    setFile(selectedFile || null); // Update the file state
  };

  if (isFetching) return;

  return (
    <Grid container gap={2} alignItems="center" justifyContent="space-between">
      <Grid item xs={5}>
        <CardMedia
          sx={{
            height: 280,
            backgroundSize: "cover",
            backgroundPosition: "center",
            bgcolor: "primary.light",
          }}
          image={product?.mainImageUrl}
          title={product?.productName}
        />
      </Grid>
      <Grid item xs={6}>
        <Typography variant="h3">{}</Typography>
        <Divider sx={{ mb: 2 }} />
        <Typography variant="h4" color="secondary">
          {formatCurrency(product?.price || -1)}
        </Typography>
        <TableContainer>
          <Table>
            <TableBody sx={{ fontSize: "1.1em" }}>
              <TableRow>
                <TableCell>Name</TableCell>
                <TableCell>{product?.productName}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Description</TableCell>
                <TableCell>{product?.description}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Type</TableCell>
                <TableCell>{product?.categoryName}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Quantity in stock</TableCell>
                <TableCell>{product?.quantityInStock}</TableCell>
              </TableRow>
            </TableBody>
          </Table>
        </TableContainer>
      </Grid>
      <Grid container item xs={5} alignItems="center" justifyContent="center">
        <Typography sx={{ marginTop: "2rem" }} variant="h5">
          List images
        </Typography>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell align="center">Image</TableCell>
              <TableCell align="center">Is Main Image</TableCell>
              <TableCell align="center">Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {product &&
              product.imageUrls.map((image, idx) => (
                <TableRow key={idx}>
                  <TableCell align="center">
                    <CardMedia
                      sx={{
                        height: 100,
                        backgroundSize: "cover",
                        backgroundPosition: "center",
                        bgcolor: "primary.light",
                      }}
                      image={image.url}
                      title={image.imageId}
                    />
                  </TableCell>
                  <TableCell align="center">
                    <Checkbox
                      checked={image.url === mainImageUrl}
                      color="success"
                      onChange={() => handleMainImage(image.imageId, image.url)}
                    />
                  </TableCell>
                  <TableCell align="center">
                    <DeleteImageForm
                      imageId={image.imageId}
                      productId={product.id}
                    />
                  </TableCell>
                </TableRow>
              ))}
          </TableBody>
        </Table>
      </Grid>
      <Grid
        container
        item
        xs={5}
        alignItems="center"
        justifyContent="center"
        gap={2}
      >
        <Typography variant="h5">Adding new image</Typography>
        <TextField type="file" fullWidth onChange={handleChooseImage} />
        <Button
          onClick={handleAddImage}
          variant="contained"
          disabled={isAdding}
        >
          Submit
        </Button>
      </Grid>
    </Grid>
  );
};

export default ProductDetails;
