import { createSlice } from "@reduxjs/toolkit";
import { fetchCampaigns, searchCampaigns } from "../thunks/campaigns/fetchCampaigns";

const campaignsSlice = createSlice({
	name: "campaigns",
	initialState: {
		isLoading: false,
		data: [],
		error: null,
	},
	extraReducers(builder) {
		builder.addCase(fetchCampaigns.pending, (state, action) => {
			state.isLoading = true;
		});
		builder.addCase(fetchCampaigns.fulfilled, (state, action) => {
			state.isLoading = false;
			state.data = action.payload;
			state.error = null;
		});
		builder.addCase(fetchCampaigns.rejected, (state, action) => {
			state.data = null;
			state.isLoading = false;
			state.error = action.error;
		});

		builder.addCase(searchCampaigns.pending, (state, action) => {
			state.isLoading = true;
		});
		builder.addCase(searchCampaigns.fulfilled, (state, action) => {
			state.isLoading = false;
			state.data = action.payload;
			state.error = null;
		});
		builder.addCase(searchCampaigns.rejected, (state, action) => {
			state.data = null;
			state.isLoading = false;
			state.error = action.error;
		});
	},
});

export const campaignsReducer = campaignsSlice.reducer;
