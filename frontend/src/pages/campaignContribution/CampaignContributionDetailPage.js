import React from "react";
import { Container } from "@mui/material";
import { useLocation } from "react-router-dom";
import PaymentMethodsWidget from "../../components/widget/PaymentMethodsWidget";
import DonationDetailWidget from "../../components/widget/DonationDetailWidget";

const CampaignContributionDetailPage = () => {
  const location = useLocation();
  const { amount, name, message, unitOfMeasurement, campaignId } =
    location.state || {};

  return (
    <Container maxWidth="md" sx={{ marginTop: 4 }}>
      <DonationDetailWidget
        name={name}
        amount={amount}
        unitOfMeasurement={unitOfMeasurement}
        message={message}
      />

      <PaymentMethodsWidget
        amount={amount}
        unitOfMeasurement={unitOfMeasurement}
        campaignId={campaignId}
        name={name}
        message={message}
      />
    </Container>
  );
};

export default CampaignContributionDetailPage;
