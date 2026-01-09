import { Box, Typography } from "@mui/material";

export default function Footer() {
    return (
        <Box
            sx={{
                bgcolor: "#4CAF50",
                color: "white",
                py: 1,
                textAlign: "center",
            }}
        >
            <Typography variant="body1">
                © 2024 Donate Hope. All Rights Reserved.
            </Typography>
        </Box>
    );
}
