import { createAsyncThunk } from "@reduxjs/toolkit";
import axiosInstance from "../../../utils/axiosInstance";

const fetchCampaigns = createAsyncThunk("campaigns/fetch", async () => {
	const response = await axiosInstance.get("/campaign");
	return response.data;
});

const searchCampaigns = createAsyncThunk(
	"campaigns/search",
	async (keyword) => {
		const response = await axiosInstance.get(
			`/campaign/search?keyword=${keyword}`
		);
		return response.data;
	}
);

export { fetchCampaigns, searchCampaigns };
