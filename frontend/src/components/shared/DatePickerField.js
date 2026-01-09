import React from "react";
import { Controller } from "react-hook-form";
import { TextField } from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";

const DatePickerField = ({
	control,
	name,
	defaultValue,
	label,
	rules,
	errors,
}) => {
	return (
		<LocalizationProvider dateAdapter={AdapterDayjs}>
			<Controller
				name={name}
				control={control}
				defaultValue={defaultValue || null} // Ensure defaultValue is not undefined
				rules={rules}
				render={({ field }) => {
					const handleChange = (newValue) => {
						// Ensure that the value is a valid dayjs object
						const formattedValue = newValue
							? dayjs(newValue).format("YYYY-MM-DD")
							: null;
						field.onChange(formattedValue); // Pass the formatted value to react-hook-form
					};

					return (
						<DatePicker
							{...field}
							value={field.value ? dayjs(field.value) : null} // Make sure value is a valid dayjs object
							onChange={handleChange}
							label={label}
							format='YYYY-MM-DD'
							renderInput={(params) => (
								<TextField
									{...params}
									error={!!errors?.[name]} // Display error message for this field
									helperText={errors?.[name]?.message}
								/>
							)}
						/>
					);
				}}
			/>
		</LocalizationProvider>
	);
};

export default DatePickerField;
