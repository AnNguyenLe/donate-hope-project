import {
    Box,
    Button,
    Container,
    FormControlLabel,
    Grid2 as Grid,
    Switch,
    TextField,
    Typography,
} from "@mui/material";
import { Controller, FormProvider, useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import DatePickerField from "../shared/DatePickerField";
import axiosInstance from "../../utils/axiosInstance";

export default function CampaignCreate() {
    const navigate = useNavigate();
    const onSubmit = async (formData) => {
        const combinedProofUrl = [
            formData.link1,
            formData.link2,
            formData.link3,
            formData.link4,
        ]
            .filter((link) => link)
            .join(",");

        const finalFormData = {
            ...formData,
            proofsUrl: combinedProofUrl,
        };

        try {
            await axiosInstance.post("/campaign/create", finalFormData);
        } catch (error) {
            console.error("Error create new campaign:", error);
        }

        navigate("/campaign");
    };
    const methods = useForm();
    const {
        control,
        handleSubmit,
        formState: { errors },
    } = useForm();
    return (
        <Box sx={{ marginTop: "4rem" }}>
            <FormProvider {...methods}>
                <Container maxWidth="lg">
                    <Box
                        sx={{
                            padding: 6,
                            backgroundColor: "#e8f5e9",
                            borderRadius: 3,
                            boxShadow: 5,
                            mt: 4,
                        }}
                    >
                        <Typography
                            variant="h4"
                            align="center"
                            gutterBottom
                            sx={{ fontWeight: "bold", color: "#388e3c" }}
                        >
                            Tạo chiến dịch mới
                        </Typography>
                        <form onSubmit={handleSubmit(onSubmit)}>
                            <Grid container spacing={4}>
                                <Grid size={12}>
                                    <Controller
                                        name="title"
                                        control={control}
                                        rules={{
                                            required:
                                                "Xin vui lòng nhập tên chiến dịch",
                                        }}
                                        render={({ field }) => (
                                            <TextField
                                                {...field}
                                                label="Tên chiến dịch"
                                                variant="outlined"
                                                fullWidth
                                                required
                                                error={!!errors.title}
                                                helperText={
                                                    errors.title?.message
                                                }
                                                sx={{
                                                    bgcolor: "white",
                                                    borderRadius: 2,
                                                    boxShadow: 1,
                                                    mb: 2,
                                                }}
                                            />
                                        )}
                                    />
                                </Grid>

                                <Grid size={12}>
                                    <Controller
                                        name="subtitle"
                                        control={control}
                                        render={({ field }) => (
                                            <TextField
                                                {...field}
                                                label="Phụ đề"
                                                variant="outlined"
                                                fullWidth
                                                sx={{
                                                    bgcolor: "white",
                                                    borderRadius: 2,
                                                    boxShadow: 1,
                                                }}
                                            />
                                        )}
                                    />
                                </Grid>

                                <Grid size={12}>
                                    <Controller
                                        name="summary"
                                        control={control}
                                        render={({ field }) => (
                                            <TextField
                                                {...field}
                                                label="Tóm tắt chiến dịch"
                                                variant="outlined"
                                                fullWidth
                                                multiline
                                                rows={4}
                                                sx={{
                                                    bgcolor: "white",
                                                    borderRadius: 2,
                                                    boxShadow: 1,
                                                }}
                                            />
                                        )}
                                    />
                                </Grid>

                                <Grid size={12}>
                                    <Controller
                                        name="description"
                                        control={control}
                                        render={({ field }) => (
                                            <TextField
                                                {...field}
                                                label="Mô tả chi tiết chiến dịch"
                                                variant="outlined"
                                                fullWidth
                                                multiline
                                                rows={6}
                                                sx={{
                                                    bgcolor: "white",
                                                    borderRadius: 2,
                                                    boxShadow: 1,
                                                }}
                                            />
                                        )}
                                    />
                                </Grid>

                                <Grid container size={12}>
                                    <Grid size={8}>
                                        <Controller
                                            name="goalAmount"
                                            control={control}
                                            rules={{
                                                required:
                                                    "Xin vui lòng nhập mục tiêu quyên góp",
                                                valueAsNumber: true,
                                                min: {
                                                    value: 0,
                                                    message:
                                                        "Số tiền quyên góp phải lớn hơn 0",
                                                },
                                            }}
                                            render={({ field }) => (
                                                <TextField
                                                    {...field}
                                                    label="Mục tiêu quyên góp"
                                                    variant="outlined"
                                                    fullWidth
                                                    required
                                                    type="number"
                                                    error={!!errors.goalAmount}
                                                    helperText={
                                                        errors.goalAmount
                                                            ?.message
                                                    }
                                                    sx={{
                                                        bgcolor: "white",
                                                        borderRadius: 2,
                                                        boxShadow: 1,
                                                    }}
                                                />
                                            )}
                                        />
                                    </Grid>

                                    <Grid size={4}>
                                        <Controller
                                            name="unitOfMeasurement"
                                            control={control}
                                            render={({ field }) => (
                                                <TextField
                                                    {...field}
                                                    label="Đơn vị tiền tệ"
                                                    variant="outlined"
                                                    fullWidth
                                                    sx={{
                                                        bgcolor: "white",
                                                        borderRadius: 2,
                                                        boxShadow: 1,
                                                    }}
                                                />
                                            )}
                                        />
                                    </Grid>
                                </Grid>

                                <Grid container size={12}>
                                    <Grid size={6}>
                                        <DatePickerField
                                            control={control}
                                            name="expectingStartDate"
                                            label="Chọn ngày bắt đầu"
                                            rules={{
                                                required:
                                                    "Xin vui lòng nhập ngày bắt đầu quyên góp",
                                            }}
                                            errors={errors}
                                        />
                                    </Grid>

                                    <Grid size={6}>
                                        <DatePickerField
                                            control={control}
                                            name="expectingEndDate"
                                            label="Chọn ngày kết thúc"
                                            rules={{
                                                required:
                                                    "Xin vui lòng nhập ngày kết thúc quyên góp!",
                                            }}
                                            errors={errors}
                                        />
                                    </Grid>
                                </Grid>

                                <Grid size={12}>
                                    <Typography
                                        variant="h6"
                                        sx={{ mb: 2, color: "#777c77" }}
                                    >
                                        Proofs URL
                                    </Typography>
                                    <Grid size={12} marginBottom={1}>
                                        <Controller
                                            name="link1"
                                            control={control}
                                            render={({ field }) => (
                                                <TextField
                                                    {...field}
                                                    label="Link Google Drive"
                                                    variant="outlined"
                                                    fullWidth
                                                    sx={{
                                                        bgcolor: "white",
                                                        borderRadius: 2,
                                                    }}
                                                />
                                            )}
                                        />
                                    </Grid>
                                    <Grid size={12} marginBottom={1}>
                                        <Controller
                                            name="link2"
                                            control={control}
                                            render={({ field }) => (
                                                <TextField
                                                    {...field}
                                                    label="Link hình 1"
                                                    variant="outlined"
                                                    fullWidth
                                                    sx={{
                                                        bgcolor: "white",
                                                        borderRadius: 2,
                                                    }}
                                                />
                                            )}
                                        />
                                    </Grid>
                                    <Grid size={12} marginBottom={1}>
                                        <Controller
                                            name="link3"
                                            control={control}
                                            render={({ field }) => (
                                                <TextField
                                                    {...field}
                                                    label="Link hình 2"
                                                    variant="outlined"
                                                    fullWidth
                                                    sx={{
                                                        bgcolor: "white",
                                                        borderRadius: 2,
                                                    }}
                                                />
                                            )}
                                        />
                                    </Grid>
                                    <Grid size={12} marginBottom={1}>
                                        <Controller
                                            name="link4"
                                            control={control}
                                            render={({ field }) => (
                                                <TextField
                                                    {...field}
                                                    label="Link hình 3"
                                                    variant="outlined"
                                                    fullWidth
                                                    sx={{
                                                        bgcolor: "white",
                                                        borderRadius: 2,
                                                    }}
                                                />
                                            )}
                                        />
                                    </Grid>
                                </Grid>

                                <Grid item xs={12}>
                                    <Controller
                                        name="isPublished"
                                        control={control}
                                        render={({ field }) => (
                                            <FormControlLabel
                                                control={
                                                    <Switch
                                                        {...field}
                                                        checked={
                                                            field.value ?? false
                                                        }
                                                        color="primary"
                                                    />
                                                }
                                                label="Đã công bố"
                                            />
                                        )}
                                    />
                                </Grid>

                                <Grid size={12}>
                                    <Button
                                        variant="contained"
                                        color="primary"
                                        type="submit"
                                        fullWidth
                                        sx={{
                                            mt: 3,
                                            padding: "14px 24px",
                                            fontSize: "16px",
                                            borderRadius: 2,
                                            boxShadow: 3,
                                            "&:hover": {
                                                boxShadow: 6,
                                            },
                                        }}
                                    >
                                        Tạo chiến dịch
                                    </Button>
                                </Grid>
                            </Grid>
                        </form>
                    </Box>
                </Container>
            </FormProvider>
        </Box>
    );
}
