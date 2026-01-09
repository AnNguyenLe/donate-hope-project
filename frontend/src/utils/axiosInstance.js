import axios from "axios";
import { store } from "../store";
import { baseUrlV1 } from "../constants/api";

const axiosInstance = axios.create({
	baseURL: baseUrlV1,
});

axiosInstance.interceptors.request.use(
	(config) => {
		const state = store.getState();
		const token = state.appUser?.data?.accessToken;
		if (token) {
			config.headers.Authorization = `Bearer ${token}`;
		}
		return config;
	},
	(error) => {
		return Promise.reject(error);
	}
);

export default axiosInstance;
