import { Box, Menu, MenuItem, Toolbar, Typography } from "@mui/material";
import { useState } from "react";
import { Link } from "react-router-dom";

export default function AnonymousAccount() {
    const [anchorEl, setAnchorEl] = useState(null);
    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    const registerPages = [
        { path: "/signup", linkName: "Người quyên góp" },
        { path: "/charity/signup", linkName: "Tổ chức từ thiện" },
    ];

    return (
        <Toolbar>
            <Typography
                variant="h6"
                sx={{
                    fontFamily: "Roboto, monospace, sans-serif",
                    fontWeight: 500,
                    letterSpacing: ".1rem",
                    color: "inherit",
                    textDecoration: "none",
                    display: "flex",
                    flexDirection: "row",
                }}
            >
                <Box
                    sx={{
                        color: "white",
                        borderRadius: "1rem",
                        padding: "0.5rem 0.8rem",
                        display: "inline-block",
                        textAlign: "center",
                        cursor: "pointer",
                        fontWeight: "bold",
                        transition: "all 0.3s ease",
                        ":hover": {
                            boxShadow: "0 5px 10px rgba(0, 0, 0, 1)",
                            transform: "scale(0.9)",
                        },
                    }}
                >
                    <Link to="/signin">Đăng nhập</Link>
                </Box>
                <Box
                    onClick={handleClick}
                    sx={{
                        color: "white",
                        borderRadius: "1rem",
                        padding: "0.5rem 0.8rem",
                        display: "inline-block",
                        textAlign: "center",
                        cursor: "pointer",
                        fontWeight: "bold",
                        transition: "all 0.3s ease",
                        ":hover": {
                            boxShadow: "0 5px 10px rgba(0, 0, 0, 1)",
                            transform: "scale(0.9)",
                        },
                    }}
                >
                    Đăng ký
                </Box>
            </Typography>

            <Menu
                anchorEl={anchorEl}
                open={Boolean(anchorEl)}
                onClose={handleClose}
            >
                {registerPages.map((page) => (
                    <MenuItem
                        sx={{
                            fontFamily: "Roboto, sans-serif",
                            fontWeight: "bold",
                            py: "auto",
                            textAlign: "center",
                            width: "12rem",
                            letterSpacing: ".1rem",
                            backgroundColor: "white",
                            color: "#4CAF50",
                            display: "flex",

                            justifyContent: "center",
                            transition: "all 0.3s ease",
                            ":hover": {
                                backgroundColor: "#4CAF50",
                                color: "white",
                                boxShadow: "0 4px 8px rgba(0, 0, 0, 0.2)",
                                transform: "scale(1.05)",
                            },
                        }}
                        onClick={handleClose}
                    >
                        <Link to={page.path}>{page.linkName}</Link>
                    </MenuItem>
                ))}
            </Menu>
        </Toolbar>
    );
}
