import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import { baseUrlV1 } from "../../../constants/api";
import axiosInstance from "../../../utils/axiosInstance";

const signInUser = createAsyncThunk(
	"user/signIn",
	async (formData, thunkAPI) => {
		try {
			const res = await axios.post(`${baseUrlV1}/account/login`, formData);
			return res.data;
		} catch (error) {
			return thunkAPI.rejectWithValue(error.response.data);
		}
	}
);

const signUpUser = createAsyncThunk(
	"user/signUp",
	async (formData, thunkAPI) => {
		try {
			const res = await axiosInstance.post("/account/register", formData);
			return res.data;
		} catch (error) {
			return thunkAPI.rejectWithValue(error.response.data);
		}
	}
);

const signUpCharity = createAsyncThunk(
	"charity/signUp",
	async (formData, thunkAPI) => {
		try {
			const res = await axiosInstance.post(
				"/charity/account/register",
				formData
			);
			return res.data;
		} catch (error) {
			return thunkAPI.rejectWithValue(error.response.data);
		}
	}
);

export { signInUser, signUpUser, signUpCharity };
