import React from "react";
import {
    Container,
    Typography,
    Box,
    Grid2,
    LinearProgress,
    Rating,
    Card,
    CardContent,
} from "@mui/material";
import { green } from "@mui/material/colors";
import AccessTimeIcon from "@mui/icons-material/AccessTime";
import StarIcon from "@mui/icons-material/Star";
import AdsClickIcon from "@mui/icons-material/AdsClick";
import SavingsIcon from "@mui/icons-material/Savings";
import MonetizationOnIcon from "@mui/icons-material/MonetizationOn";
import LinkIcon from "@mui/icons-material/Link";
import CampaignPhotos from "./CampaignPhotos";
import CampaignUpdate from "./CampaignUpdate";
import { useSelector } from "react-redux";
import { jwtDecode } from "jwt-decode";

export default function CampaignDetail({ campaign, proofUrls }) {
    const handleCampaignUpdate = (updatedCampaign) => {
        Object.assign(campaign, updatedCampaign);
    };

    const userAccessToken = useSelector(
        (state) => state.appUser?.data?.accessToken
    );
    const decodedToken = jwtDecode(userAccessToken);
    const userUuid = decodedToken?.sub;

    const showUpdateButton = userUuid === campaign.userId;

    return (
        <Container
            maxWidth="lg"
            sx={{
                backgroundColor: "#f5f5f5",
                padding: 3,
                borderRadius: 2,
                background: "url(/bg-detail-green.jpg)",
            }}
        >
            <Box
                sx={{
                    backgroundColor: green[500],
                    padding: 3,
                    borderRadius: 2,
                    marginBottom: 3,
                }}
            >
                <Typography
                    variant="h4"
                    sx={{
                        color: "#fff",
                        fontWeight: "bold",
                        textAlign: "center",
                    }}
                >
                    {campaign.title}
                </Typography>
                <Typography
                    variant="h6"
                    sx={{ color: "#fff", marginTop: 1, textAlign: "center" }}
                >
                    {campaign.subtitle}
                </Typography>
            </Box>

            <Typography
                variant="body2"
                sx={{
                    marginBottom: 2,
                    marginTop: 1,
                    paddingLeft: 1,
                    textAlign: "justify",
                    lineHeight: 1.8,
                }}
            >
                {campaign.description}
            </Typography>

            <Card sx={{ marginBottom: 2, borderRadius: 2 }}>
                <CardContent>
                    <Typography variant="h5" sx={{ fontWeight: "bold" }}>
                        Mục Tiêu Quyên Góp
                    </Typography>
                    <LinearProgress
                        variant="determinate"
                        value={
                            (campaign.achievedAmount / campaign.goalAmount) *
                            100
                        }
                        sx={{ marginTop: 2, height: 10, borderRadius: 5 }}
                        color="success"
                    />
                    <Box sx={{ marginTop: 1 }}>
                        <Box
                            sx={{
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "space-around",
                                my: 3,
                            }}
                        >
                            <Box sx={{ display: "flex", alignItems: "center" }}>
                                <AdsClickIcon
                                    sx={{
                                        marginRight: 1,
                                        color: green[500],
                                    }}
                                />
                                <Typography variant="h6">
                                    Mục tiêu quyên góp:{" "}
                                    {campaign.goalAmount.toLocaleString()}{" "}
                                    {campaign.unitOfMeasurement}
                                </Typography>
                            </Box>
                            <Box sx={{ display: "flex", alignItems: "center" }}>
                                <SavingsIcon
                                    sx={{
                                        marginRight: 1,
                                        color: green[500],
                                    }}
                                />
                                <Typography variant="h6" color="textSecondary">
                                    Đã quyên góp được:{" "}
                                    {campaign.achievedAmount.toLocaleString()}{" "}
                                    {campaign.unitOfMeasurement || "0"} |
                                    Status:{" "}
                                    <Box
                                        component="span"
                                        sx={{
                                            color:
                                                campaign.goalStatus ===
                                                "Completed"
                                                    ? "success.main"
                                                    : campaign.goalStatus ===
                                                      "InProgress"
                                                    ? "info.main"
                                                    : "text.disabled",
                                        }}
                                    >
                                        {campaign.goalStatus}
                                    </Box>
                                </Typography>
                            </Box>
                        </Box>
                    </Box>
                </CardContent>
            </Card>
            <Grid2 container spacing={3} justifyContent="space-around">
                <Grid2 item xs={12} sm={6}>
                    <Box
                        sx={{
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "space-around",
                            my: 3,
                        }}
                    >
                        <Box sx={{ display: "flex", alignItems: "center" }}>
                            <AccessTimeIcon
                                sx={{ marginRight: 1, color: green[500] }}
                            />
                            <Typography
                                variant="body2"
                                color="textSecondary"
                                marginRight={2}
                            >
                                <strong>Ngày dự kiến bắt đầu:</strong>{" "}
                                {new Date(
                                    campaign.expectingStartDate
                                ).toLocaleDateString("en-GB")}
                            </Typography>
                        </Box>
                        <Box sx={{ display: "flex", alignItems: "center" }}>
                            <AccessTimeIcon
                                sx={{ marginRight: 1, color: green[500] }}
                            />
                            <Typography variant="body2" color="textSecondary">
                                <strong>Ngày dự kiến kết thúc:</strong>{" "}
                                {new Date(
                                    campaign.expectingEndDate
                                ).toLocaleDateString("en-GB")}
                            </Typography>
                        </Box>
                    </Box>
                </Grid2>

                <Grid2 item xs={12} sm={6}>
                    <Box
                        sx={{
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "space-around",
                            my: 3,
                        }}
                    >
                        <Box sx={{ display: "flex", alignItems: "center" }}>
                            <StarIcon
                                sx={{ marginRight: 1, color: "warning.main" }}
                            />
                            <Typography
                                variant="body2"
                                color="textSecondary"
                                marginRight={2}
                            >
                                <strong>Số lượt đánh giá:</strong>{" "}
                                {campaign.numberOfRatings}
                            </Typography>
                        </Box>
                        <Box sx={{ display: "flex", alignItems: "center" }}>
                            <StarIcon
                                sx={{ marginRight: 1, color: "warning.main" }}
                            />
                            <Typography variant="body2" color="textSecondary">
                                <strong>Điểm đánh giá trung bình:</strong>
                            </Typography>
                            <Rating
                                name="average-rating"
                                value={campaign.averageRatingPoint}
                                precision={0.1}
                                readOnly
                            />
                        </Box>
                    </Box>
                </Grid2>
            </Grid2>

            <Card
                sx={{
                    marginTop: 3,
                    padding: 3,
                    borderRadius: 2,
                    backgroundColor: "#fff",
                }}
            >
                <Box sx={{ display: "flex", alignItems: "center" }}>
                    <MonetizationOnIcon
                        sx={{ marginRight: 1, color: green[500] }}
                    />
                    <Typography
                        variant="h5"
                        fontWeight="bold"
                        sx={{ fontSize: "1.5rem", marginRight: 2 }}
                    >
                        Tổng Chi Tiêu
                    </Typography>
                    <Typography
                        variant="h5"
                        fontWeight="bold"
                        color="textSecondary"
                        sx={{ marginTop: 0 }}
                    >
                        {campaign.spendingAmount.toLocaleString()}{" "}
                        {campaign.unitOfMeasurement}
                    </Typography>
                </Box>
            </Card>

            {campaign.proofsUrl && (
                <Box marginTop={3}>
                    <Typography
                        variant="body2"
                        color="textSecondary"
                        sx={{ display: "flex", alignItems: "center" }}
                    >
                        <LinkIcon sx={{ marginRight: 1, color: green[500] }} />{" "}
                        <strong>Hình ảnh chiến dịch:</strong>{" "}
                        <a
                            href={proofUrls[0]}
                            target="_blank"
                            rel="noopener noreferrer"
                            style={{
                                marginLeft: 5,
                                textDecoration: "none",
                                color: green[700],
                                fontWeight: "bold",
                                transition: "color 0.3s ease",
                            }}
                            onMouseOver={(e) =>
                                (e.currentTarget.style.color = green[900])
                            }
                            onMouseOut={(e) =>
                                (e.currentTarget.style.color = green[700])
                            }
                        >
                            Ấn vào đây
                        </a>
                    </Typography>

                    <CampaignPhotos
                        photoUrls={proofUrls.slice(
                            1,
                            proofUrls.length > 4 ? 4 : proofUrls.length
                        )}
                    />
                </Box>
            )}
            {showUpdateButton && (
                <CampaignUpdate
                    campaign={campaign}
                    onUpdate={handleCampaignUpdate}
                />
            )}
        </Container>
    );
}
