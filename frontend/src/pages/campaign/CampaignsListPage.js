import { useEffect } from "react";
import { useSelector } from "react-redux";
import { useThunk } from "../../hooks/useThunk";
import { fetchCampaigns } from "../../store/thunks/campaigns/fetchCampaigns";
import { Skeleton, Container, Grid2, Box } from "@mui/material";
import CampaignCard from "../../components/campaign/CampaignCard";
import { Link, useNavigate } from "react-router-dom";
import SearchBar from "../../components/search/CampaignSearchBar";

export default function CampaignsListPage() {
  const [doFetchCampaigns, isLoadingCampaigns, loadingCampaignsError] =
    useThunk(fetchCampaigns);

  useEffect(() => {
    doFetchCampaigns();
  }, [doFetchCampaigns]);

  const campaigns = useSelector((state) => state.campaigns);
  const isCharityAppUser = useSelector(
    (state) => state.appUser.data.isCharityOrg
  );

  const navigate = useNavigate();

  let content;
  if (isLoadingCampaigns) {
    content = <Skeleton variant="wave" />;
  } else if (loadingCampaignsError) {
    content = <div>Error fetching data...</div>;
  } else if (campaigns.data?.length) {
    content = (
      <Container maxWidth="xl" sx={{ marginTop: "3rem" }}>
        <Grid2 container spacing={2}>
          {campaigns.data.map((campaign) => (
            <Grid2 item xs={12} sm={6} md={3} key={campaign.Id}>
              <CampaignCard
                campaign={campaign}
                goToDetailPage={() => {
                  navigate(`/campaign/${campaign.id}`);
                }}
              />
            </Grid2>
          ))}
        </Grid2>
      </Container>
    );
  } else {
    content = (
      <Container maxWidth="xl" sx={{ marginTop: "3rem" }}>
        <Grid2 container spacing={2}>
          <Grid2 item xs={12} sm={6} md={3}>
            Không tìm thấy chiến dịch
          </Grid2>
        </Grid2>
      </Container>
    );
  }

  return (
    <Box>
      <Box
        sx={{
          position: "relative",
          top: "1.8rem",
          left: "6.8rem",
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          width: "88%",
          boxSizing: "border-box",
        }}
      >
        {isCharityAppUser ? (
          <Link to="/campaign/create">
            <Box
              sx={{
                display: "inline-block",
                padding: "10px 20px",
                borderRadius: "4px",
                backgroundColor: "primary.main",
                color: "white",
                textAlign: "center",
                cursor: "pointer",
                fontWeight: "bold",
                fontSize: "16px",
                boxShadow: 2,
                "&:hover": {
                  backgroundColor: "primary.dark",
                  boxShadow: 6,
                },
                "&:active": {
                  backgroundColor: "primary.light",
                },
              }}
            >
              Tạo chiến dịch từ thiện
            </Box>
          </Link>
        ) : (
          <Box
            sx={{
              display: "inline-block",
              padding: "10px 20px",
              borderRadius: "4px",
              backgroundColor: "grey.400",
              color: "rgba(255, 255, 255, 0.5)",
              textAlign: "center",
              cursor: "not-allowed",
              fontWeight: "bold",
              fontSize: "16px",
            }}
          >
            Tạo chiến dịch từ thiện
          </Box>
        )}
        <Box
          sx={{
            width: "40%",
          }}
        >
          <SearchBar />
        </Box>
      </Box>

      <Box>{content}</Box>
    </Box>
  );
}
