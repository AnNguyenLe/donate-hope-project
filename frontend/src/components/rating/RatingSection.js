import React, { useState, useEffect } from "react";
import {
    Box,
    Container,
    Typography,
    Button,
    Rating,
    TextField,
} from "@mui/material";
import axiosInstance from "../../utils/axiosInstance";
import { useSelector } from "react-redux";

const RatingSection = ({ campaignId }) => {
    const [userRating, setUserRating] = useState(0);
    const [feedback, setFeedback] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [errorMessage, setErrorMessage] = useState(null);
    const [successMessage, setSuccessMessage] = useState(null);

    const appUser = useSelector((state) => state.appUser.data);

    useEffect(() => {
        const savedMessage = localStorage.getItem("ratingSuccessMessage");
        if (savedMessage) {
            setSuccessMessage(savedMessage);
            localStorage.removeItem("ratingSuccessMessage");
        }
    }, []);

    const handleRatingSubmit = async () => {
        if (userRating === 0) {
            setSuccessMessage("");
            setErrorMessage("Vui lòng chọn điểm đánh giá trước khi gửi.");
            return;
        }
        setIsSubmitting(true);
        try {
            const response = await axiosInstance.post(
                "/campaign-rating/create",
                {
                    ratingPoint: userRating,
                    feedback,
                    campaignId,
                    createdBy: appUser.id,
                }
            );
            if (response.status === 200 || response.status === 201) {
                localStorage.setItem(
                    "ratingSuccessMessage",
                    "Đánh giá của bạn đã được gửi thành công!"
                );
                setSuccessMessage("Đánh giá của bạn đã được gửi thành công!");
                setErrorMessage(null);
                window.location.reload();
            } else {
                throw new Error(
                    "Đã xảy ra lỗi không mong muốn khi gửi đánh giá."
                );
            }
        } catch (error) {
            if (error.response) {
                const { status, data } = error.response;
                if (status === 400) {
                    setErrorMessage(
                        data.message ||
                            "Bạn đã đánh giá chiến dịch này trước đó."
                    );
                } else if (status === 500) {
                    setErrorMessage(
                        "Server đang gặp sự cố. Vui lòng thử lại sau."
                    );
                } else {
                    setErrorMessage(data.message || "Không thể gửi đánh giá.");
                }
            } else {
                setErrorMessage(
                    "Không thể kết nối đến server. Vui lòng kiểm tra mạng."
                );
            }
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <Container maxWidth="lg" className="mt-6">
            <Box>
                {successMessage && (
                    <Typography color="success.main" mb={2}>
                        {successMessage}
                    </Typography>
                )}
                {errorMessage && (
                    <Typography color="error" mb={2}>
                        {errorMessage}
                    </Typography>
                )}
                <Typography variant="h6">Đánh giá của bạn:</Typography>
                <Rating
                    name="user-rating"
                    value={userRating}
                    onChange={(event, newValue) => setUserRating(newValue)}
                    aria-label="Rate this campaign"
                />
                <TextField
                    label="Phản hồi của bạn"
                    multiline
                    rows={2}
                    value={feedback}
                    onChange={(e) => setFeedback(e.target.value)}
                    fullWidth
                    margin="normal"
                />
                <Button
                    variant="contained"
                    onClick={handleRatingSubmit}
                    disabled={isSubmitting}
                    aria-label="Submit your rating"
                >
                    {isSubmitting ? "Submitting..." : "Gửi đánh giá"}
                </Button>
            </Box>
        </Container>
    );
};

export default RatingSection;
