import { configureStore } from "@reduxjs/toolkit";

import { appUserReducer, signOutUser, resetError } from "./slices/appUserSlice";
import { campaignsReducer } from "./slices/campaignsSlice";

const store = configureStore({
	reducer: {
		appUser: appUserReducer,
		campaigns: campaignsReducer,
	},
});

export { store };

export * from "./thunks/appUser/auth";
export * from "./thunks/campaigns/fetchCampaigns";

export { signOutUser, resetError };
