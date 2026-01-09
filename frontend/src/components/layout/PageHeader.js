import { AppBar, Box, Toolbar, Typography } from "@mui/material";
import { FaHeart } from "react-icons/fa6";
import NavBar from "./NavBar";
import UserAccount from "./UserAccount";
import { useNavigate } from "react-router-dom";

export default function PageHeader() {
	const navigate = useNavigate();

	return (
		<AppBar
			sx={{
				position: "static",
				backgroundColor: "#4CAF50",
				display: "flex",
				flexDirection: "row",
				justifyContent: "space-between",
			}}
		>
			<Toolbar sx={{ width: "50%" }}>
				<Typography
					variant='h5'
					sx={{
						fontFamily: "monospace",
						fontWeight: 700,
						letterSpacing: ".1rem",
						color: "inherit",
						textDecoration: "none",
						display: "flex",
						flexDirection: "row",
					}}
				>
					<div
						onClick={() => {
							navigate("/");
						}}
						style={{
							width: "250px",
							display: "flex",
							flexDirection: "row",
							alignItems: "center",
							justifyContent: "space-evenly",
							cursor: "pointer",
						}}
					>
						<FaHeart />
						<span>Donate Hope</span>
					</div>
					<NavBar />
				</Typography>
			</Toolbar>
			<Box sx={{ display: "flex", alignItems: "center" }}>
				<UserAccount />
			</Box>
		</AppBar>
	);
}
