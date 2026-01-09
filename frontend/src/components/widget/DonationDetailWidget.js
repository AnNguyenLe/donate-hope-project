import React from "react";

import { 
    Box,
    Typography 
} from "@mui/material";

const DonationDetailWidget = ({ name, amount, unitOfMeasurement, message }) => {
    return (
        <Box
        sx={{
          backgroundColor: "background.paper",
          borderRadius: 2,
          boxShadow: 3,
          padding: 3,
          marginBottom: 2,
        }}
      >
        <Typography
          variant="h5"
          gutterBottom
          sx={{
            fontWeight: "bold",
            color: "primary.main",
            textAlign: "center",
          }}
        >
          Donation Details
        </Typography>
        <Typography
          variant="body1"
          sx={{ marginBottom: 2, fontStyle: "italic", textAlign: "center" }}
        >
          Thank you for your generous donation!
        </Typography>
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            gap: 1.5,
            padding: 2,
            backgroundColor: "background.default",
            border: "1px solid #9AD221",
            borderRadius: 2,
            boxShadow: 1,
            marginTop: 2,
          }}
        >
          {[
            { label: "Donator:", value: name || "Unknown" },
            {
              label: "Donation Amount:",
              value: `${amount ? amount.toLocaleString() : "0"} ${unitOfMeasurement}`,
              style: {
                fontWeight: "bold",
                color: "#990000",
                textTransform: "uppercase",
              },
            },
            {
              label: "Message:",
              value: message || "No message provided",
              style: { fontStyle: "italic" },
            },
          ].map(({ label, value, style = {} }) => (
            <Box
              key={label}
              sx={{
                display: "flex",
                alignItems: "center",
                width: "100%",
              }}
            >
              <Typography
                variant="body2"
                sx={{
                  fontWeight: "bold",
                  color: "text.secondary",
                  minWidth: "150px",
                }}
              >
                {label}
              </Typography>
              <Typography variant="body1" sx={{ ...style }}>
                {value}
              </Typography>
            </Box>
          ))}
        </Box>
      </Box>
    );
};

export default DonationDetailWidget;
