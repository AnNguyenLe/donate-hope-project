import React from "react";
import { Box, Typography, Grid2 as Grid, Card, CardContent } from "@mui/material";

export default function AboutUsPage() {
	return (
		<Box sx={{ bgcolor: "#f9f9f9", minHeight: "100vh", px: 3, py: 6 }}>
			{/* Hero Section */}
			<Box
				sx={{
					textAlign: "center",
					py: 6,
					background: "linear-gradient(135deg, #4caf50, #81c784)",
					color: "white",
				}}
			>
				<Typography variant="h2" sx={{ fontWeight: "bold", mb: 2 }}>
					Donate Hope
				</Typography>
				<Typography variant="h5">
					Kết nối lòng nhân ái, kiến tạo sự đổi thay.
				</Typography>
			</Box>

			{/* Mission and Vision Section */}
			<Box sx={{ py: 6, textAlign: "center", px: 4 }}>
				<Typography
					variant="h4"
					sx={{ fontWeight: "bold", mb: 3, color: "#2c3e50" }}
				>
					Sứ Mệnh & Tầm Nhìn Của Chúng Tôi
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
					<strong>Donate Hope</strong> cam kết cung cấp một nền tảng minh bạch
					và hiệu quả để quyên góp cứu trợ thiên tai. Sứ mệnh của chúng tôi
					là kết nối các nhà hảo tâm với những người cần giúp đỡ, đảm bảo rằng
					mọi đóng góp đều mang lại hiệu quả và ý nghĩa. Chúng tôi hình dung
					một thế giới nơi lòng nhân ái và sự hào phóng giúp các cộng đồng
					tái thiết và phát triển.
				</Typography>
			</Box>

			{/* Our Team Section */}
			<Box sx={{ py: 6, bgcolor: "#fff", px: 3 }}>
				<Typography
					variant="h4"
					sx={{ fontWeight: "bold", mb: 3, textAlign: "center" }}
				>
					Gặp Gỡ Đội Ngũ Của Chúng Tôi
				</Typography>
				<Grid container spacing={3} justifyContent="space-evenly">
					{[
						{
							name: "Nguyễn Lê Thiên Ân",
							studentId: "22880007",
							sologan: "Dẫn Lối Tầm Nhìn, Kiến Tạo Tương Lai",
						},
						{
							name: "Vũ Huy Phương",
							studentId: "22880264",
							sologan: "Tận Tâm Vận Hành, Vượt Mọi Thách Thức",
						},
						{
							name: "Võ Hoàng Oanh",
							studentId: "22880118",
							sologan: "Xây Dựng Cộng Đồng, Kết Nối Yêu Thương",
						},
						{
							name: "Trần Cung Bắc",
							studentId: "22880207",
							sologan: "Lan Tỏa Giá Trị, Gắn Kết Nhân Văn",
						},
					].map((member, index) => (
						<Grid item xs={12} sm={4} key={index}>
							<Card sx={{ textAlign: "center", p: 2, width: 400 }}>
								<CardContent>
									<Typography
										variant="h6"
										sx={{
											fontWeight: "bold",
											color: "#990000",
											mb: 1,
										}}
									>
										{member.name}
									</Typography>
									<Typography variant="subtitle1" sx={{ mb: 1, fontWeight: "bold" }}>
										{member.studentId}
									</Typography>
									<Typography variant="subtitle2" sx={{ mb: 1 }}>
										{member.sologan}
									</Typography>
								</CardContent>
							</Card>
						</Grid>
					))}
				</Grid>
			</Box>

			{/* Values Section */}
			<Box
				sx={{
					py: 6,
					textAlign: "center",
					px: 4,
					background: "linear-gradient(135deg, #f3f4f6, #ffffff)",
				}}
			>
				<Typography
					variant="h4"
					sx={{ fontWeight: "bold", mb: 3, color: "#2c3e50" }}
				>
					Giá Trị Cốt Lõi Của Chúng Tôi
				</Typography>
				<Grid container spacing={3} justifyContent="center">
					{[
						{
							title: "Minh Bạch",
							description:
								"Chúng tôi ưu tiên sự minh bạch và trách nhiệm trong mọi hoạt động của mình.",
						},
						{
							title: "Nhân Ái",
							description:
								"Lòng trắc ẩn thúc đẩy những nỗ lực của chúng tôi nhằm hỗ trợ những người cần giúp đỡ.",
						},
						{
							title: "Hiệu Quả",
							description:
								"Chúng tôi nỗ lực để đảm bảo rằng mọi đóng góp đều mang lại tác động thực sự.",
						},
					].map((value, index) => (
						<Grid item xs={12} sm={4} key={index}>
							<Card sx={{ textAlign: "center", p: 2 }}>
								<CardContent>
									<Typography
										variant="h6"
										sx={{
											fontWeight: "bold",
											mb: 1,
											color: "#4CAF50",
										}}
									>
										{value.title}
									</Typography>
									<Typography
										variant="body2"
										sx={{ color: "#555" }}
									>
										{value.description}
									</Typography>
								</CardContent>
							</Card>
						</Grid>
					))}
				</Grid>
			</Box>
		</Box>
	);
}
