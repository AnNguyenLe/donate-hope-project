import React, { useState } from "react";
import { Box, Typography, Button, Divider } from "@mui/material";
import { useNavigate } from "react-router-dom";
import BankTransferIcon from "../../assets/icons/bank-transfer-icon.png";
import EWalletIcon from "../../assets/icons/momo-icon.png";
import CashIcon from "../../assets/icons/cash-icon.png";
import BankTransferQRCode from "../../assets/qrCodes/bank-transfer-qr-code.png";
import EWalletQRCode from "../../assets/qrCodes/momo-qr-code.png";
import axiosInstance from "../../utils/axiosInstance";
import AlertComponent from "../../pages/campaignContribution/AlertCampaignContribution";

const PaymentMethodsWidget = ({ amount, unitOfMeasurement, name, message, campaignId }) => {
    const [selectedMethod, setSelectedMethod] = useState(null);
    const [showAlert, setShowAlert] = useState(false);
    const [alertMessage, setAlertMessage] = useState("");
    const [alertSeverity, setAlertSeverity] = useState("success");
    const navigate = useNavigate();

  const onSubmit = async () => {
    const data = {
      amount: amount,
      unitOfMeasurement: unitOfMeasurement,
      contributionMethod: selectedMethod,
      donatorName: name,
      message: message,
      campaignId: campaignId,
    };

        try {
            await axiosInstance.post("/campaign-contribution/create", data);
            setAlertMessage(
                `Cảm ơn bạn đã quyên góp ${amount.toLocaleString()} ${unitOfMeasurement.toUpperCase()}`
            );
            setAlertSeverity("success");
            setShowAlert(true);
        } catch (error) {
            setAlertMessage(
                "Đã có lỗi xảy ra trong quá trình quyên góp của bạn. Xin vui lòng thử lại!"
            );
            setAlertSeverity("error");
            setShowAlert(true);
        }
        setTimeout(() => {
            navigate(`/campaign/${campaignId}`);
        }, 1000);
    };

    const renderCampaignContributionDetails = () => {
        switch (selectedMethod) {
            case "EWallet":
                return (
                    <Box
                        sx={{
                            textAlign: "center",
                            mt: 3,
                            p: 2,
                            backgroundColor: "background.paper",
                        }}
                    >
                        <Typography
                            variant="h6"
                            sx={{
                                fontWeight: "bold",
                                color: "primary.main",
                                mb: 2,
                            }}
                        >
                            Scan QR Code to Donate
                        </Typography>
                        <img
                            src={EWalletQRCode}
                            alt="E-Wallet QR Code"
                            style={{
                                maxWidth: "200px",
                                margin: "10px auto",
                                display: "block",
                                border: "1px solid #ccc",
                                borderRadius: "8px",
                            }}
                        />
                    </Box>
                );

            case "BankTransfer":
                return (
                    <Box
                        sx={{
                            textAlign: "center",
                            mt: 3,
                            p: 2,
                            backgroundColor: "background.paper",
                        }}
                    >
                        <Typography
                            variant="h6"
                            sx={{
                                fontWeight: "bold",
                                color: "primary.main",
                                mb: 2,
                            }}
                        >
                            Scan QR Code to Donate
                        </Typography>
                        <img
                            src={BankTransferQRCode}
                            alt="BankTransfer QR Code"
                            style={{
                                maxWidth: "200px",
                                margin: "10px auto",
                                display: "block",
                                border: "1px solid #ccc",
                                borderRadius: "8px",
                            }}
                        />
                    </Box>
                );

            case "Cash":
                return (
                    <Box
                        sx={{
                            mt: 3,
                            p: 2,
                            backgroundColor: "background.paper",
                        }}
                    >
                        <Typography
                            variant="h6"
                            sx={{
                                fontWeight: "bold",
                                color: "primary.main",
                                mb: 2,
                            }}
                        >
                            Cash Payment Details
                        </Typography>
                        <Typography variant="body1" sx={{ mb: 1 }}>
                            <strong>Address:</strong> Ho Chi Minh City
                        </Typography>
                        <Typography variant="body1">
                            <strong>Phone:</strong> 028 123 456
                        </Typography>
                    </Box>
                );

            default:
                return (
                    <Box
                        sx={{
                            mt: 3,
                            p: 2,
                            textAlign: "center",
                            backgroundColor: "background.default",
                            borderRadius: 2,
                            borderColor: "text.secondary",
                        }}
                    >
                        <Typography
                            variant="body2"
                            sx={{
                                color: "text.secondary",
                                fontStyle: "italic",
                            }}
                        >
                            Please select a payment method to see the details.
                        </Typography>
                    </Box>
                );
        }
    };
    return (
        <Box
            sx={{
                backgroundColor: "background.paper",
                borderRadius: 2,
                boxShadow: 3,
                padding: 3,
                textAlign: "center",
            }}
        >
            <Typography
                variant="h5"
                gutterBottom
                sx={{
                    fontWeight: "bold",
                    color: "primary.main",
                    marginBottom: 3,
                }}
            >
                Payment Method
            </Typography>
            <Box
                sx={{
                    display: "flex",
                    justifyContent: "space-between",
                    mb: 2,
                }}
            >
                {[
                    { method: "EWallet", label: "Momo", icon: EWalletIcon },
                    {
                        method: "BankTransfer",
                        label: "Bank Transfer",
                        icon: BankTransferIcon,
                    },
                    { method: "Cash", label: "Cash", icon: CashIcon },
                ].map(({ method, label, icon }) => (
                    <Button
                        key={method}
                        onClick={() => setSelectedMethod(method)}
                        sx={{
                            display: "flex",
                            flexDirection: "column",
                            alignItems: "center",
                            width: "220px",
                            height: "120px",
                            padding: 2,
                            borderRadius: 2,
                            boxShadow: selectedMethod === method ? 6 : 1,
                            backgroundColor:
                                selectedMethod === method
                                    ? "#45ad4e"
                                    : "background.default",
                            color:
                                selectedMethod === method
                                    ? "primary.contrastText"
                                    : "text.primary",
                            "&:hover": {
                                backgroundColor: "#78d35b",
                                color: "white",
                            },
                        }}
                    >
                        <img
                            src={icon}
                            alt={`${label} Icon`}
                            style={{
                                width: "70px",
                                height: "70px",
                                marginBottom: "10px",
                            }}
                        />
                        <Typography variant="body1">{label}</Typography>
                    </Button>
                ))}
            </Box>
            <Box sx={{ marginTop: 2, borderRadius: 2 }}>
                {renderCampaignContributionDetails()}
            </Box>

            <Divider sx={{ marginBottom: 2 }}></Divider>

            <Box justifyItems="center">
                <Button
                    variant="contained"
                    color="primary"
                    disabled={!selectedMethod}
                    onClick={onSubmit}
                    sx={{
                        mt: 1,
                        width: "30%",
                        padding: "10px",
                        fontWeight: "bold",
                    }}
                >
                    Confirm Payment
                </Button>
            </Box>
            {showAlert && (
                <AlertComponent
                    message={alertMessage}
                    severity={alertSeverity}
                />
            )}
        </Box>
    );
};

export default PaymentMethodsWidget;
