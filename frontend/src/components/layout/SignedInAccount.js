import { Box, Toolbar, Typography, Menu, MenuItem } from "@mui/material";
import { useState } from "react";
import axiosInstance from "../../utils/axiosInstance";
import { useDispatch } from "react-redux";
import { signOutUser } from "../../store";

export default function SignedInAccount({ appUser }) {
	const dispatch = useDispatch();

	const [anchorEl, setAnchorEl] = useState(null);
	const handleClick = (event) => {
		setAnchorEl(event.currentTarget);
	};
	const handleClose = () => {
		setAnchorEl(null);
	};
	const handleSignOut = () => {
		onSignOut();
		handleClose();
	};

	const onSignOut = async () => {
		try {
			await axiosInstance.get("/account/logout");
			localStorage.removeItem("appUser");
			dispatch(signOutUser());
			window.location.reload();
		} catch (error) {
			console.error("Logout error:", error);
		}
	};

	return (
		<Toolbar>
			<Typography
				variant='h6'
				sx={{
					fontFamily: "Roboto, sans-serif",
					fontWeight: 500,
					letterSpacing: ".1rem",
					color: "inherit",
					textDecoration: "none",
					display: "flex",
					flexDirection: "row",
				}}
			>
				<Box
					onClick={handleClick}
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
					Chào, {`${appUser.firstName} ${appUser.lastName}`}
				</Box>
			</Typography>

			<Menu
				anchorEl={anchorEl}
				open={Boolean(anchorEl)}
				onClose={handleClose}
				sx={{
					transition: "opacity 0.3s ease",
					opacity: anchorEl ? 1 : 0,
					transform: anchorEl ? "scale(1)" : "scale(0.9)",
					transformOrigin: "top right",
				}}
			>
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
					onClick={handleSignOut}
				>
					Đăng xuất
				</MenuItem>
			</Menu>
		</Toolbar>
	);
}
