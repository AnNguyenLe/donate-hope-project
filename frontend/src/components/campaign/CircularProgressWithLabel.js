import React from "react";
import { CircularProgress, Box, Typography } from "@mui/material";

function CircularProgressWithLabel({ value }) {
    const status =
        value >= 100
            ? "Hoàn thành mục tiêu quyên góp!"
            : "Chưa đạt mục tiêu quyên góp";

    return (
        <Box
            sx={{
                position: "relative",
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
            }}
        >
            <Box sx={{ position: "relative", display: "inline-flex" }}>
                <CircularProgress
                    variant="determinate"
                    value={value}
                    sx={{ color: value >= 100 ? "green" : "primary.main" }}
                    thickness={5}
                />
                <Box
                    sx={{
                        top: 0,
                        left: 0,
                        bottom: 0,
                        right: 0,
                        position: "absolute",
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "center",
                    }}
                >
                    <Typography
                        variant="caption"
                        component="div"
                        color="text.secondary"
                    >
                        {`${Math.round(value)}%`}
                    </Typography>
                </Box>
            </Box>

            <Typography
                variant="body2"
                sx={{
                    marginTop: 1,
                    color: value >= 100 ? "green" : "text.secondary",
                }}
            >
                {status}
            </Typography>
        </Box>
    );
}

export default CircularProgressWithLabel;
