import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { Box, Input, Button, Typography, TextField, Grid } from "@mui/material";
import { Stepper, Step, StepLabel } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";

import DatePickerField from "../../components/shared/DatePickerField";
import { useDispatch, useSelector } from "react-redux";
import { resetError, signUpCharity } from "../../store";

function CharityRegisterPage() {
    const [activeStep, setActiveStep] = useState(0);
    const [showPassword] = useState(false);
    const {
        register,
        handleSubmit,
        getValues,
        control,
        formState: { errors, isValid },
        trigger,
    } = useForm({ mode: "onChange", reValidateMode: "onChange" });
    const dispatch = useDispatch();
    const accessToken = useSelector(
        (state) => state.appUser?.data?.accessToken
    );
    const navigate = useNavigate();

    useEffect(() => {
        if (accessToken) {
            navigate("/");
        }
    }, [accessToken, navigate]);

    const steps = [
        "Đại diện pháp lý",
        "Thông tin tổ chức từ thiện",
        "Thông tin đăng nhập",
    ];

    const onSubmit = (data) => {
        dispatch(signUpCharity(data));
    };

    const handleNext = async () => {
        const isStepValid = await trigger();
        if (isStepValid) {
            setActiveStep((prevStep) => prevStep + 1);
        }
    };

    const handleBack = () => setActiveStep((prevStep) => prevStep - 1);

    const backendError = useSelector((state) => state.appUser?.error);

    const [responseError, setResponseError] = useState(null);

    useEffect(() => {
        if (backendError) {
            setResponseError(backendError);
        } else {
            setResponseError(null);
        }
    }, [backendError]);

    useEffect(() => {
        dispatch(resetError());
    }, [dispatch]);

    const renderStepContent = () => {
        switch (activeStep) {
            case 0:
                return (
                    <Box
                        sx={{
                            display: "flex",
                            justifyContent: "center",
                            alignItems: "center",
                        }}
                    >
                        <Grid
                            item
                            xs={12}
                            md={6}
                            sx={{
                                display: "flex",
                                justifyContent: "center",
                                alignItems: "center",
                                backgroundColor: "white",
                            }}
                        >
                            <Box
                                sx={{
                                    width: "100%",
                                    maxWidth: "700px",
                                    textAlign: "center",
                                }}
                            >
                                <Typography
                                    variant="h5"
                                    fontWeight="bold"
                                    my={4}
                                    color="primary.main"
                                >
                                    Đại diện pháp lý
                                </Typography>
                                <Box display="flex" gap={2} mb={2}>
                                    <TextField
                                        label="Tên"
                                        fullWidth
                                        type="text"
                                        {...register("repFirstName", {
                                            required: "Xin vui lòng nhập tên",
                                        })}
                                        variant="filled"
                                        InputProps={{
                                            disableUnderline: true,
                                        }}
                                        error={!!errors.repFirstName}
                                        helperText={
                                            errors.repFirstName?.message
                                        }
                                    />
                                    <TextField
                                        label="Họ và tên lót"
                                        fullWidth
                                        {...register("repLastName", {
                                            required:
                                                "Xin vui lòng nhập họ và tên lót",
                                        })}
                                        variant="filled"
                                        InputProps={{
                                            disableUnderline: true,
                                        }}
                                        error={!!errors.repLastName}
                                        helperText={errors.repLastName?.message}
                                    />
                                </Box>
                                <Box
                                    display="flex"
                                    flexDirection="column"
                                    mb={2}
                                >
                                    <Box
                                        display="flex"
                                        justifyContent="space-between"
                                        alignItems="center"
                                        mb={1}
                                    >
                                        <Box
                                            variant="body1"
                                            width="240px"
                                            height="60px"
                                            display="flex"
                                            textAlign="start"
                                            alignItems="center"
                                            justifyContent="flex-start"
                                            borderRadius={1}
                                            sx={{
                                                backgroundColor: "#F0F0F0",
                                            }}
                                        >
                                            <Typography
                                                sx={{ ml: 1, color: "gray" }}
                                            >
                                                Ngày sinh
                                            </Typography>
                                        </Box>
                                        <DatePickerField
                                            control={control}
                                            name="repDateOfBirth"
                                            label="Chọn ngày sinh"
                                            rules={{
                                                required:
                                                    "Xin vui lòng nhập ngày sinh",
                                            }}
                                            errors={errors}
                                        />
                                    </Box>
                                    {errors.repDateOfBirth && (
                                        <Typography
                                            color="error"
                                            sx={{
                                                textAlign: "end",
                                                fontSize: "0.75rem",
                                            }}
                                        >
                                            {errors.repDateOfBirth.message}
                                        </Typography>
                                    )}
                                </Box>

                                <Box display="flex" mb={2}>
                                    <TextField
                                        label="Email"
                                        fullWidth
                                        variant="filled"
                                        type="email"
                                        {...register("repEmail", {
                                            required: "Xin vui lòng nhập Email",
                                            pattern: {
                                                value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                                                message:
                                                    "Địa chỉ email không hợp lệ",
                                            },
                                        })}
                                        InputProps={{
                                            disableUnderline: true,
                                        }}
                                        error={!!errors.repEmail}
                                        helperText={errors.repEmail?.message}
                                    />
                                </Box>
                            </Box>
                        </Grid>
                    </Box>
                );
            case 1:
                return (
                    <Box
                        sx={{
                            display: "flex",
                            justifyContent: "center",
                            alignItems: "center",
                        }}
                    >
                        <Grid
                            item
                            xs={12}
                            md={6}
                            sx={{
                                display: "flex",
                                justifyContent: "center",
                                alignItems: "center",
                                backgroundColor: "white",
                            }}
                        >
                            <Box
                                sx={{
                                    width: "90%",
                                    maxWidth: "500px",
                                    textAlign: "center",
                                }}
                            >
                                <Typography
                                    variant="h5"
                                    fontWeight="bold"
                                    my={4}
                                >
                                    Tên tổ chức
                                </Typography>
                                <Box display="flex" gap={2} mb={2}>
                                    <TextField
                                        label="Tên"
                                        fullWidth
                                        type="text"
                                        {...register("orgName", {
                                            required:
                                                "Xin vui lòng nhập tên tổ chức",
                                        })}
                                        variant="filled"
                                        InputProps={{
                                            disableUnderline: true,
                                        }}
                                        error={!!errors.orgName}
                                        helperText={errors.orgName?.message}
                                    />
                                </Box>

                                <Box display="flex" mb={2}>
                                    <TextField
                                        label="Địa chỉ"
                                        fullWidth
                                        type="text"
                                        {...register("orgAddress", {
                                            required:
                                                "Xin vui lòng nhập địa chỉ",
                                        })}
                                        variant="filled"
                                        InputProps={{
                                            disableUnderline: true,
                                        }}
                                        error={!!errors.orgAddress}
                                        helperText={errors.orgAddress?.message}
                                    />
                                </Box>

                                <Box display="flex" mb={2}>
                                    <TextField
                                        label="Số điện thoại"
                                        fullWidth
                                        type="tel"
                                        {...register("orgPhone", {
                                            required:
                                                "Xin vui lòng nhập số điện thoại",
                                        })}
                                        variant="filled"
                                        InputProps={{
                                            disableUnderline: true,
                                        }}
                                        error={!!errors.orgPhone}
                                        helperText={errors.orgPhone?.message}
                                    />
                                </Box>

                                <Box display="flex" mb={2}>
                                    <TextField
                                        label="Email (bắt buộc)"
                                        fullWidth
                                        variant="filled"
                                        type="email"
                                        {...register("orgEmail", {
                                            required: "Xin vui lòng nhập Email",
                                            pattern: {
                                                value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                                                message:
                                                    "Địa chỉ email không hợp lệ",
                                            },
                                        })}
                                        InputProps={{
                                            disableUnderline: true,
                                        }}
                                        error={!!errors.orgEmail}
                                        helperText={errors.orgEmail?.message}
                                    />
                                </Box>
                            </Box>
                        </Grid>
                    </Box>
                );
            case 2:
                return (
                    <Box
                        sx={{
                            display: "flex",
                            justifyContent: "center",
                            alignItems: "center",
                        }}
                    >
                        <Box
                            sx={{
                                width: "100%",
                                maxWidth: 400,
                                margin: "1rem auto",
                                padding: 4,
                                background: "white",
                                borderRadius: "1rem",
                                boxShadow: 3,
                                textAlign: "center",
                            }}
                        >
                            <Typography
                                variant="h5"
                                sx={{
                                    fontWeight: "bold",
                                    marginBottom: 3,
                                    color: "primary.main",
                                }}
                            >
                                Đăng nhập
                            </Typography>
                            {responseError && (
                                <Box>
                                    <Typography
                                        variant="body2"
                                        color="error"
                                        align="center"
                                        sx={{
                                            marginBottom: "1",
                                            fontWeight: "bold",
                                            fontSize: "1rem",
                                            lineHeight: "1.5",
                                            wordWrap: "break-word",
                                        }}
                                    >
                                        {responseError.title}
                                    </Typography>
                                    <Typography
                                        variant="body2"
                                        color="error"
                                        align="center"
                                        sx={{
                                            marginBottom: "16px",
                                            fontSize: "0.875rem",
                                            lineHeight: "1.5",
                                            wordWrap: "break-word",
                                        }}
                                    >
                                        {responseError.detail}
                                    </Typography>
                                </Box>
                            )}
                            <Box sx={{ marginBottom: 2, textAlign: "left" }}>
                                <Typography variant="body1">
                                    Đăng nhập bằng Email
                                </Typography>
                                <Input
                                    sx={{
                                        width: "100%",
                                        padding: "12px",
                                        borderRadius: "1rem",
                                        marginTop: 1,
                                        border: "1px solid #ccc",
                                        transition:
                                            "border 0.3s ease-in-out, box-shadow 0.3s ease-in-out",
                                        ":focus": {
                                            borderColor: "#4CAF50",
                                            boxShadow:
                                                "0 0 5px rgba(76, 175, 80, 0.5)",
                                        },
                                        ":before": {
                                            display: "none",
                                        },
                                        ":after": {
                                            display: "none",
                                        },
                                        ":hover": {
                                            borderColor: "#4CAF50",
                                        },
                                    }}
                                    type="email"
                                    value={getValues("orgEmail")}
                                    disabled
                                />
                            </Box>

                            <Box sx={{ marginBottom: 2, textAlign: "left" }}>
                                <Typography variant="body1">
                                    Mật khẩu
                                </Typography>
                                <Input
                                    sx={{
                                        width: "100%",
                                        padding: "12px",
                                        borderRadius: "1rem",
                                        marginTop: 1,
                                        border: "1px solid #ccc",
                                        transition:
                                            "border 0.3s ease-in-out, box-shadow 0.3s ease-in-out",
                                        ":focus": {
                                            borderColor: "#4CAF50",
                                            boxShadow:
                                                "0 0 5px rgba(76, 175, 80, 0.5)",
                                        },
                                        ":before": {
                                            display: "none",
                                        },
                                        ":after": {
                                            display: "none",
                                        },
                                        ":hover": {
                                            borderColor: "#4CAF50",
                                        },
                                    }}
                                    type={showPassword ? "text" : "password"}
                                    {...register("password", {
                                        required: "Xin vui lòng nhập mật khẩu",
                                        minLength: 6,
                                    })}
                                />
                                {errors.password && (
                                    <Typography
                                        color="error"
                                        sx={{
                                            textAlign: "end",
                                            fontSize: "0.75rem",
                                        }}
                                    >
                                        {errors.password.message}
                                    </Typography>
                                )}
                            </Box>
                            <Box sx={{ marginBottom: 2, textAlign: "left" }}>
                                <Typography variant="body1">
                                    Xác nhận mật khẩu
                                </Typography>
                                <Input
                                    sx={{
                                        width: "100%",
                                        padding: "12px",
                                        borderRadius: "1rem",
                                        marginTop: 1,
                                        border: "1px solid #ccc",
                                        transition:
                                            "border 0.3s ease-in-out, box-shadow 0.3s ease-in-out",
                                        ":focus": {
                                            borderColor: "#4CAF50",
                                            boxShadow:
                                                "0 0 5px rgba(76, 175, 80, 0.5)",
                                        },
                                        ":before": {
                                            display: "none",
                                        },
                                        ":after": {
                                            display: "none",
                                        },
                                        ":hover": {
                                            borderColor: "#4CAF50",
                                        },
                                    }}
                                    type="password"
                                    {...register("confirmPassword", {
                                        required:
                                            "Xin vui lòng nhập lại mật khẩu",
                                        validate: (value) =>
                                            value === getValues("password") ||
                                            "Mật khẩu không chính xác",
                                    })}
                                />
                                {errors.confirmPassword && (
                                    <Typography
                                        color="error"
                                        sx={{
                                            textAlign: "end",
                                            fontSize: "0.75rem",
                                        }}
                                    >
                                        {errors.confirmPassword.message}
                                    </Typography>
                                )}
                            </Box>
                            <Box
                                className="flex justify-between mt-12"
                                sx={{
                                    gap: 2,
                                    display:
                                        activeStep === steps.length - 1
                                            ? "flex"
                                            : "block",
                                    justifyContent:
                                        activeStep === steps.length - 1
                                            ? "space-between"
                                            : "flex-start",
                                }}
                            >
                                <Button
                                    disabled={activeStep === 0}
                                    onClick={handleBack}
                                    variant="outlined"
                                    color="primary"
                                    sx={{
                                        width: "48%",
                                        padding: "12px",
                                        fontWeight: "bold",
                                        borderRadius: "1rem",
                                        color: "primary.main",
                                        "&:hover": {
                                            boxShadow:
                                                "0 4px 8px rgba(0, 0, 0, 0.2)",
                                        },
                                        height: "56px",
                                        marginTop: 3,
                                    }}
                                >
                                    Quay lại
                                </Button>

                                {activeStep === steps.length - 1 && (
                                    <Button
                                        type="submit"
                                        onClick={handleSubmit(onSubmit)}
                                        sx={{
                                            width: "48%",
                                            padding: "12px",
                                            fontWeight: "bold",
                                            borderRadius: "1rem",
                                            backgroundColor: "primary.main",
                                            color: "white",
                                            "&:hover": {
                                                backgroundColor: "primary.dark",
                                                boxShadow:
                                                    "0 4px 8px rgba(0, 0, 0, 0.2)",
                                            },
                                            marginTop: 3,
                                        }}
                                    >
                                        XÁC NHẬN
                                    </Button>
                                )}
                            </Box>
                            <Typography variant="body1" mt={2}>
                                Bạn đã có tài khoản?{" "}
                                <Link
                                    to="/signin"
                                    style={{
                                        color: "#9c27b0",
                                        fontWeight: "bold",
                                        textDecoration: "none",
                                    }}
                                >
                                    Đăng nhập
                                </Link>
                            </Typography>
                        </Box>
                    </Box>
                );
            default:
                return null;
        }
    };

    return (
        <Grid
            container
            sx={{
                background: "white",
                overflow: "hidden",
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
            }}
        >
            <Box
                sx={{
                    display: "flex",
                    flexDirection: "column",
                    justifyContent: "center",
                    alignItems: "center",
                    width: "100%",
                }}
            >
                <Typography
                    sx={{
                        color: "primary.main",
                        fontWeight: "bold",
                        fontSize: "2rem",
                        marginTop: 3,
                        marginBottom: 3,
                    }}
                >
                    Đăng ký tổ chức từ thiện
                </Typography>
                <Stepper
                    activeStep={activeStep}
                    alternativeLabel
                    sx={{ width: "80%", marginTop: 2 }}
                >
                    {steps.map((label, index) => (
                        <Step key={label}>
                            <StepLabel>{label}</StepLabel>
                        </Step>
                    ))}
                </Stepper>

                <form
                    onSubmit={handleSubmit(onSubmit)}
                    style={{ width: "50%" }}
                    className="pt-3"
                >
                    {renderStepContent()}

                    <Box className="flex justify-between mb-12">
                        {activeStep !== 2 && (
                            <Button
                                disabled={activeStep === 0}
                                onClick={handleBack}
                                variant="outlined"
                                color="primary"
                                sx={{
                                    width: "30%",

                                    padding: "12px",
                                    fontWeight: "bold",
                                    borderRadius: "1rem",
                                    color: "primary.main",
                                    "&:hover": {
                                        boxShadow:
                                            "0 4px 8px rgba(0, 0, 0, 0.2)",
                                    },
                                }}
                            >
                                Quay lại
                            </Button>
                        )}
                        {activeStep !== 2 && (
                            <Button
                                variant="contained"
                                color="primary"
                                onClick={handleNext}
                                sx={{
                                    width: "30%",
                                    padding: "12px",
                                    fontWeight: "bold",
                                    borderRadius: "1rem",
                                    backgroundColor: "primary.main",
                                    color: "white",
                                    "&:hover": {
                                        backgroundColor: "primary.dark",
                                        boxShadow:
                                            "0 4px 8px rgba(0, 0, 0, 0.2)",
                                    },
                                }}
                                disabled={
                                    !isValid || Object.keys(errors).length > 0
                                }
                            >
                                Tiếp theo
                            </Button>
                        )}
                    </Box>
                </form>
            </Box>
        </Grid>
    );
}

export default CharityRegisterPage;
