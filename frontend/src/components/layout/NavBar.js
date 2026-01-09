import { Toolbar, Typography, Box } from "@mui/material";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";

export default function NavBar() {
    const appUser = useSelector((state) => state.appUser);
    const protectedPages = [
        {
            label: "Chiến dịch",
            path: "/campaign",
        },
    ];
    const allowAnonymousPages = [
        {
            label: "Về chúng tôi",
            path: "/about-us",
        },
        {
            label: "Liên hệ",
            path: "/contact",
        },
    ];

    const pages =
        appUser && appUser.data
            ? [...protectedPages, ...allowAnonymousPages]
            : [...allowAnonymousPages];
    return (
        <Toolbar>
            <Typography
                variant="h6"
                sx={{
                    fontFamily: "Roboto,monospace,sans-serif",
                    fontWeight: 500,
                    color: "inherit",
                    textDecoration: "none",
                    display: "flex",
                    flexDirection: "row",
                    justifyContent: "space-around",
                    width: "30rem",
                }}
            >
                {pages.map((page) => (
                    <Box
                        key={page.label}
                        sx={{
                            color: "white",
                            borderRadius: "1rem",
                            padding: "0.5rem 0.8rem",
                            display: "inline-block",
                            textAlign: "center",
                            fontWeight: "bold",
                            cursor: "pointer !important",
                            transition: "all 0.3s ease",
                            ":hover": {
                                boxShadow: "0 5px 10px rgba(0, 0, 0, 0.3)",
                                transform: "scale(0.95)",
                            },
                        }}
                    >
                        <Link to={page.path}>{page.label}</Link>
                    </Box>
                ))}
            </Typography>
        </Toolbar>
    );
}
