import React, { useState } from "react";
import { Box, OutlinedInput, IconButton } from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";
import { fetchCampaigns, searchCampaigns } from "../../store";
import { useThunk } from "../../hooks/useThunk";

const CampaignSearchBar = () => {
    const [searchQuery, setSearchQuery] = useState("");
    const [doSearchCampaigns] = useThunk(searchCampaigns);

    const [doFetchCampaigns] = useThunk(fetchCampaigns);

    const handleChange = (event) => {
        const inputValue = event.target.value;
        setSearchQuery(inputValue);
        if (inputValue.trim() === "") {
            doFetchCampaigns();
        }
    };

    const handlePress = (event) => {
        if (event.key === "Enter") {
            if (searchQuery.trim() === "") {
                doFetchCampaigns();
            } else {
                doSearchCampaigns(searchQuery);
            }
        }
    };

    const handleSearchClick = () => {
        if (searchQuery.trim() === "") {
            doFetchCampaigns();
        } else {
            doSearchCampaigns(searchQuery);
        }
    };

    return (
        <Box
            sx={{
                display: "flex",
                alignItems: "center",
                margin: "12px auto",
            }}
        >
            <OutlinedInput
                value={searchQuery}
                onChange={handleChange}
                onKeyDown={handlePress}
                placeholder="Tìm chiến dịch..."
                sx={{
                    width: "90%",
                    borderRadius: 25,
                    border: "1px solid #ccc",
                    backgroundColor: "white",
                    "&:hover": { borderColor: "#888" },
                    "&.Mui-focused": { borderColor: "#3f51b5" },
                }}
            />
            <IconButton
                onClick={handleSearchClick}
                sx={{
                    ml: 1,
                    padding: 1.5,
                    borderRadius: "50%",
                    backgroundColor: "#1976d2",
                    "&:hover": {
                        color: "#fff",
                        backgroundColor: "#1565c0",
                        boxShadow: "2px 2px 4px rgba(0, 0, 0, 0.3)",
                        transition: "all 0.3s ease",
                    },
                }}
            >
                <SearchIcon sx={{ color: "#fff" }} />
            </IconButton>
        </Box>
    );
};

export default CampaignSearchBar;
