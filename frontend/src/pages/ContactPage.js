import React from "react";
import { Box, Typography, Link } from "@mui/material";

export default function ContactPage() {
	return (
		<Box sx={{ bgcolor: "#f9f9f9", minHeight: "100vh", px: 3, py: 6 }}>
			<Box
				sx={{
					textAlign: "center",
					py: 6,
					background: "linear-gradient(135deg, #4caf50, #81c784)",
					color: "white",
				}}
			>
				<Typography variant="h3" sx={{ fontWeight: "bold", mb: 2 }}>
					Liên Hệ Với Chúng Tôi
				</Typography>
				<Typography variant="h5">
					Chúng tôi luôn sẵn sàng lắng nghe và hỗ trợ bạn.
				</Typography>
			</Box>

			<Box sx={{ textAlign: "center", py: 4 }}>
				<Typography
					variant="h5"
					sx={{ fontWeight: "bold", mb: 3, color: "#2c3e50" }}
				>
					Địa chỉ văn phòng Donate Hope
				</Typography>
				<Box
					sx={{
						maxWidth: "800px",
						margin: "0 auto",
						border: "1px solid #ccc",
						borderRadius: "8px",
						overflow: "hidden",
					}}
				>
					<iframe
						src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3919.5074242227083!2d106.69709997573601!3d10.772394259266724!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752f3f56a3de55%3A0x7c6107f1253d69c9!2zMTIzIMSQLiBMw6ogTOG7o2ksIFBoxrDhu51uZyBC4bq_biBUaMOgbmgsIFF14bqtbiAxLCBI4buTIENow60gTWluaCwgVmnhu4d0IE5hbQ!5e0!3m2!1svi!2s!4v1734283094247!5m2!1svi!2s"
						width="100%"
						height="450"
						style={{ border: "0" }}
						allowFullScreen=""
						loading="lazy"
						title="Google Map"
					></iframe>
				</Box>

				{/* Link to Google Map */}
				<Typography variant="body2" sx={{ mt: 2 }}>
					Hoặc bạn có thể xem vị trí của chúng tôi trên{" "}
					<Link
						href="https://maps.app.goo.gl/bqNQRo8fk4tgX6FQ7"
						target="_blank"
						rel="noopener"
						sx={{ color: "#4CAF50", fontWeight: "bold" }}
					>
						Google Maps
					</Link>.
				</Typography>
			</Box>
		</Box>
	);
}
