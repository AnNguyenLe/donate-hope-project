import React, { useEffect, useState } from "react";
import {
    Box,
    Button,
    Card,
    CardContent,
    Grid2,
    Typography,
} from "@mui/material";
import { Link } from "react-router-dom";
import axiosInstance from "../utils/axiosInstance";
import CampaignCard from "../components/campaign/CampaignCard";

export default function HomePage() {
    const [campaigns, setCampaigns] = useState([]);

    useEffect(() => {
        const fetchCampaigns = async () => {
            try {
                const response = await axiosInstance.get(
                    "/campaign/landingpage"
                );
                setCampaigns(response.data);
            } catch (error) {
                console.error("Error fetching campaigns:", error);
            }
        };
        fetchCampaigns();
    }, []);

    return (
        <Box sx={{ bgcolor: "#f9f9f9", minHeight: "100vh" }}>
            {/* Hero Section */}
            <Box
                sx={{
                    display: "flex",
                    justifyContent: "center",
                    alignItems: "center",
                    flexDirection: "column",
                    textAlign: "center",
                    backgroundImage: 'url("hinh-thien-tai.jpg")',
                    backgroundSize: "cover",
                    backgroundPosition: "center",
                    backgroundRepeat: "no-repeat", // Prevents repeating the image
                    color: "white",
                    py: 8,
                }}
            >
                {/* Title and Description */}
                <Typography variant="h2" sx={{ fontWeight: "bold", mb: 2 }}>
                    Donate Hope
                </Typography>
                <Typography variant="h5" sx={{ mb: 4 }}>
                    Vì đồng bào thân yêu
                </Typography>
            </Box>

            {/* About Section */}
            <Box
                sx={{
                    px: 4,
                    py: 6,
                    textAlign: "center",
                    background: "linear-gradient(135deg, #f3f4f6, #ffffff)",
                    boxShadow: "0px 4px 12px rgba(0, 0, 0, 0.1)",
                    borderRadius: 2,
                }}
            >
                <Typography
                    variant="h4"
                    sx={{
                        fontWeight: "bold",
                        mb: 3,
                        color: "#2c3e50",
                        textTransform: "uppercase",
                    }}
                >
                    Về Donate Hope
                </Typography>
                <Typography
                    variant="body1"
                    sx={{
                        color: "#555",
                        maxWidth: "800px",
                        mx: "auto",
                        textAlign: "justify",
                        lineHeight: 1.8,
                        fontSize: "1.1rem",
                    }}
                >
                    <strong>Donate Hope - For our compatriots</strong> là ứng
                    dụng web phi lợi nhuận được thiết kế để hỗ trợ việc quyên
                    góp từ thiện cho các nạn nhân bị ảnh hưởng bởi thiên tai,
                    với mục tiêu đảm bảo tính công khai, minh bạch và hiệu quả
                    trong quản lý quyên góp và phân phối tài trợ.
                    <Box sx={{ mt: 2 }}>
                        {[
                            "Donate Hope cho phép tổ chức từ thiện tạo ra những chiến dịch khác nhau để huy động đóng góp từ các cá nhân và tổ chức, quản lý và cập nhật quá trình nhận quyên góp, cung cấp minh chứng phân phối tài trợ một cách tập trung, hiệu quả và công khai.",
                            "Thông tin chi tiết về các chiến dịch được đăng tải đầy đủ trên hệ thống để những người quan tâm có thể cập nhật nội dung liên quan.",
                            "Người dùng có thể theo dõi, tìm kiếm về quá trình và kết quả quyên góp, thực hiện đánh giá và bình luận về từng chiến dịch, hoặc xuất báo cáo chi tiết nếu có nhu cầu.",
                        ].map((text, index) => (
                            <Typography
                                key={index}
                                variant="body1"
                                sx={{
                                    display: "flex",
                                    alignItems: "start",
                                    gap: 1,
                                    mb: 2,
                                    fontSize: "1.1rem",
                                }}
                            >
                                <span
                                    style={{
                                        color: "#28a745",
                                        fontWeight: "bold",
                                    }}
                                >
                                    ✓
                                </span>
                                {text}
                            </Typography>
                        ))}
                    </Box>
                </Typography>

                <Button
                    variant="contained"
                    sx={{
                        mt: 4,
                        px: 4,
                        py: 1.5,
                        fontSize: "1rem",
                        fontWeight: "bold",
                        background: "#3498db",
                        color: "#fff",
                        borderRadius: "25px",
                        "&:hover": {
                            background: "#2980b9",
                        },
                    }}
                    component={Link}
                    to="/campaign"
                >
                    Xem Thêm Các Chiến dịch Từ Thiện
                </Button>
            </Box>

            {/* Features Section */}
            <Box sx={{ px: 3, py: 6, bgcolor: "#fff" }}>
                <Typography
                    variant="h4"
                    sx={{ fontWeight: "bold", textAlign: "center", mb: 4 }}
                >
                    Tính Năng Nổi Bật
                </Typography>
                <Grid2 container spacing={3} justifyContent="center">
                    {[
                        {
                            title: "Minh bạch",
                            description:
                                "Đảm bảo sự minh bạch trong quản lý quyên góp.",
                        },
                        {
                            title: "Hiệu quả",
                            description: "Quản lý phân phối tài trợ hiệu quả.",
                        },
                        {
                            title: "Dễ sử dụng",
                            description: "Giao diện thân thiện và dễ sử dụng.",
                        },
                    ].map((feature, index) => (
                        <Grid2 item xs={12} sm={4} key={index}>
                            <Card sx={{ textAlign: "center", p: 2 }}>
                                <CardContent>
                                    <Typography
                                        variant="h6"
                                        sx={{
                                            fontWeight: "bold",
                                            mb: 1,
                                            color: "#990000",
                                            fontSize: "1.5rem",
                                        }}
                                    >
                                        {feature.title}
                                    </Typography>
                                    <Typography
                                        variant="body2"
                                        sx={{ color: "#555", fontSize: "1rem" }}
                                    >
                                        {feature.description}
                                    </Typography>
                                </CardContent>
                            </Card>
                        </Grid2>
                    ))}
                </Grid2>
            </Box>

            {/* Campaigns Section */}
            <Box sx={{ px: 3, py: 6, bgcolor: "#f1f1f1" }}>
                <Typography
                    variant="h4"
                    sx={{ fontWeight: "bold", mb: 4, textAlign: "center" }}
                >
                    Các Chiến Dịch Nổi Bật
                </Typography>
                <Grid2 container spacing={3} justifyContent="center">
                    {campaigns.map((campaign) => (
                        <Grid2 item xs={12} sm={4} key={campaign.id}>
                            <CampaignCard campaign={campaign} />
                        </Grid2>
                    ))}
                </Grid2>
            </Box>
        </Box>
    );
}
