import React, { useState } from "react";
import {
    Box,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    FormControlLabel,
    Switch,
    TextField,
    Snackbar,
    Alert,
} from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";
import axiosInstance from "../../utils/axiosInstance";

export default function CampaignUpdate({ campaign, onUpdate }) {
    const [open, setOpen] = useState(false);
    const [updatedCampaign, setUpdatedCampaign] = useState({ ...campaign });
    const [snackbarOpen, setSnackbarOpen] = useState(false);
    const [snackbarMessage, setSnackbarMessage] = useState("");
    const [snackbarSeverity, setSnackbarSeverity] = useState("success");
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setUpdatedCampaign((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async () => {
        try {
            const mergedCampaign = { ...campaign, ...updatedCampaign };
            const response = await axiosInstance.put(
                `/campaign/${campaign.id}`,
                mergedCampaign
            );
            setSnackbarMessage("Cập nhật thông tin chiến dịch thành công!");
            setSnackbarSeverity("success");
            setSnackbarOpen(true);
            onUpdate(response.data);

            setTimeout(() => {
                handleClose();
                window.location.href = `/campaign/${campaign.id}`;
            }, 1000);

        } catch (error) {
            console.error("Cập nhật thông tin chiến dịch thất bại:", error);
            setSnackbarMessage(
                "Cập nhật thông tin chiến dịch thất bại. Vui lòng thử lại"
            );
            setSnackbarSeverity("error");
            setSnackbarOpen(true);
        }
    };

    return (
        <>
            <Button
                variant="contained"
                color="primary"
                onClick={handleOpen}
                sx={{ marginTop: 2 }}
            >
                Cập nhật chiến dịch
            </Button>

            <Dialog open={open} onClose={handleClose} maxWidth="md">
                <DialogTitle
                    variant="h5"
                    sx={{ fontWeight: "bold", color: "primary.main" }}
                    gutterBottom
                >
                    Cập nhật thông tin chiến dịch
                </DialogTitle>
                <DialogContent>
                    <TextField
                        fullWidth
                        label="Tên chiến dịch"
                        name="title"
                        value={updatedCampaign.title}
                        onChange={handleInputChange}
                        margin="normal"
                    />
                    <TextField
                        fullWidth
                        label="Phụ đề"
                        name="subtitle"
                        value={updatedCampaign.subtitle}
                        onChange={handleInputChange}
                        margin="normal"
                    />
                    <TextField
                        fullWidth
                        label="Mô tả chiến dịch"
                        name="description"
                        value={updatedCampaign.description}
                        onChange={handleInputChange}
                        multiline
                        rows={4}
                        margin="normal"
                    />
                    <TextField
                        fullWidth
                        label="Mục tiêu quyên góp"
                        name="goalAmount"
                        type="number"
                        value={updatedCampaign.goalAmount}
                        onChange={handleInputChange}
                        margin="normal"
                    />
                    <Box
                        sx={{
                            display: "flex",
                            marginTop: 2,
                            justifyContent: "space-between",
                        }}
                    >
                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                            <DatePicker
                                label="Ngày bắt đầu quyên góp"
                                value={
                                    updatedCampaign.expectingStartDate
                                        ? dayjs(
                                              updatedCampaign.expectingStartDate
                                          )
                                        : null
                                }
                                onChange={(newValue) => {
                                    setUpdatedCampaign((prev) => ({
                                        ...prev,
                                        expectingStartDate: newValue
                                            ? dayjs(newValue).format(
                                                  "YYYY-MM-DD"
                                              )
                                            : null,
                                    }));
                                }}
                                format="DD/MM/YYYY"
                                renderInput={(params) => (
                                    <TextField
                                        {...params}
                                        fullWidth
                                        margin="normal"
                                        error={
                                            !updatedCampaign.expectingStartDate
                                        }
                                        helperText={
                                            !updatedCampaign.expectingStartDate
                                                ? "Xin vui lòng nhập ngày bắt đầu quyên góp"
                                                : ""
                                        }
                                    />
                                )}
                            />
                        </LocalizationProvider>

                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                            <DatePicker
                                label="Ngày kết thúc chiến dịch"
                                value={
                                    updatedCampaign.expectingEndDate
                                        ? dayjs(
                                              updatedCampaign.expectingEndDate
                                          )
                                        : null
                                }
                                onChange={(newValue) => {
                                    setUpdatedCampaign((prev) => ({
                                        ...prev,
                                        expectingEndDate: newValue
                                            ? dayjs(newValue).format(
                                                  "YYYY-MM-DD"
                                              )
                                            : null,
                                    }));
                                }}
                                format="DD/MM/YYYY"
                                renderInput={(params) => (
                                    <TextField
                                        {...params}
                                        fullWidth
                                        margin="normal"
                                        error={
                                            !updatedCampaign.expectingStartDate
                                        }
                                        helperText={
                                            !updatedCampaign.expectingStartDate
                                                ? "Xin vui lòng nhập ngày kết thúc chiến dịch"
                                                : ""
                                        }
                                    />
                                )}
                            />
                        </LocalizationProvider>
                    </Box>
                    <TextField
                        fullWidth
                        label="Tổng chi tiêu"
                        name="spendingAmount"
                        value={updatedCampaign.spendingAmount}
                        onChange={handleInputChange}
                        margin="normal"
                    />
                    <TextField
                        fullWidth
                        label="Link hình ảnh chiến dịch"
                        name="proofsUrl"
                        value={updatedCampaign.proofsUrl.split(",").join("\n")}
                        multiline
                        rows={5}
                        onChange={(e) =>
                            setUpdatedCampaign((prev) => ({
                                ...prev,
                                proofsUrl: e.target.value.split("\n").join(","),
                            }))
                        }
                        margin="normal"
                        helperText="Nhập mỗi URL trên một dòng mới"
                    />
                    <FormControlLabel
                        control={
                            <Switch
                                checked={updatedCampaign.isPublished}
                                onChange={(e) =>
                                    setUpdatedCampaign((prev) => ({
                                        ...prev,
                                        isPublished: e.target.checked,
                                    }))
                                }
                            />
                        }
                        label="Đã công bố"
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="secondary">
                        Hủy
                    </Button>
                    <Button
                        onClick={handleSubmit}
                        variant="contained"
                        color="primary"
                    >
                        Lưu
                    </Button>
                </DialogActions>
            </Dialog>
            <Snackbar
                open={snackbarOpen}
                autoHideDuration={6000}
                onClose={() => setSnackbarOpen(false)}
            >
                <Alert
                    onClose={() => setSnackbarOpen(false)}
                    severity={snackbarSeverity}
                    sx={{ width: "100%" }}
                >
                    {snackbarMessage}
                </Alert>
            </Snackbar>
        </>
    );
}
