import { Box, TextField, Typography, Grid, Button } from "@mui/material";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import DatePickerField from "../../components/shared/DatePickerField";
import { useDispatch, useSelector } from "react-redux";
import { resetError, signUpUser } from "../../store";
import { useEffect, useState } from "react";

function RegisterPage() {
    const {
        register,
        handleSubmit,
        control,
        getValues,
        formState: { errors },
    } = useForm();
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const accessToken = useSelector(
        (state) => state.appUser?.data?.accessToken
    );

    useEffect(() => {
        if (accessToken) {
            navigate("/");
        }
    }, [accessToken, navigate]);

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

    const onSubmit = (formData) => {
        console.log(formData);
        dispatch(signUpUser(formData));
    };
    return (
        <Grid
            container
            sx={{
                height: "100vh",
                background: "white",
                overflow: "hidden",
            }}
        >
            {/* Bên trái */}
            <Grid
                item
                xs={12}
                md={6}
                sx={{
                    backgroundImage: "url(/img-signin-form.jpg)",
                    backgroundSize: "cover",
                    backgroundPosition: "center",
                    backgroundRepeat: "no-repeat",
                    position: "relative",
                }}
            ></Grid>
            {/* Bên phải */}
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
                    component="form"
                    onSubmit={handleSubmit(onSubmit)}
                    sx={{
                        width: "90%",
                        maxWidth: "500px",
                        textAlign: "center",
                    }}
                >
                    <Typography variant="h4" fontWeight="bold" mb={4}>
                        Đăng ký tài khoản
                    </Typography>
                    {responseError && (
                        <Box>
                            <Box
                                sx={{
                                    color: "red",
                                    textAlign: "center",
                                    marginBottom: "16px",
                                }}
                            >
                                {responseError.title}
                            </Box>
                            <Box
                                sx={{
                                    color: "red",
                                    textAlign: "center",
                                    marginBottom: "16px",
                                }}
                            >
                                {responseError.detail}
                            </Box>
                        </Box>
                    )}

                    <Box display="flex" gap={2} mb={2}>
                        <TextField
                            label="Tên"
                            fullWidth
                            {...register("firstName", {
                                required: "Xin vui lòng nhập tên",
                            })}
                            variant="filled"
                            InputProps={{
                                disableUnderline: true,
                            }}
                            error={!!errors.firstName}
                            helperText={errors.firstName?.message}
                        />
                        <TextField
                            label="Họ và tên lót"
                            fullWidth
                            {...register("lastName", {
                                required: "Xin vui lòng nhập họ và tên lót",
                            })}
                            variant="filled"
                            InputProps={{
                                disableUnderline: true,
                            }}
                            error={!!errors.lastName}
                            helperText={errors.lastName?.message}
                        />
                    </Box>
                    <Box
                        display="flex"
                        justifyContent="space-between"
                        alignItems="center"
                        mb={2}
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
                            <Typography sx={{ ml: 1, color: "gray" }}>
                                Ngày sinh
                            </Typography>
                        </Box>
                        <DatePickerField
                            control={control}
                            name="dateOfBirth"
                            label="Chọn ngày sinh"
                            rules={{ required: "Xin vui lòng nhập ngày sinh" }}
                            errors={errors}
                        />
                    </Box>

                    <Box display="flex" mb={2}>
                        <TextField
                            label="Email"
                            fullWidth
                            variant="filled"
                            type="email"
                            {...register("email", {
                                required: "Xin vui lòng nhập Email",
                            })}
                            InputProps={{
                                disableUnderline: true,
                            }}
                            error={!!errors.email}
                            helperText={errors.email?.message}
                        />
                    </Box>

                    <Box display="flex" mb={2}>
                        <TextField
                            label="Mật khẩu"
                            fullWidth
                            variant="filled"
                            type="password"
                            {...register("password", {
                                required: "Xin vui lòng nhập mật khẩu",
                                minLength: {
                                    value: 8,
                                    message: "Mật khẩu phải có ít nhất 8 ký tự",
                                },
                                pattern: {
                                    value: /(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_])/,
                                    message:
                                        "Mật khẩu phải bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt",
                                },
                            })}
                            InputProps={{
                                disableUnderline: true,
                            }}
                            error={!!errors.password}
                            helperText={errors.password?.message}
                        />
                    </Box>
                    <Box display="flex" mb={2}>
                        <TextField
                            label="Xác nhận mật khẩu"
                            fullWidth
                            variant="filled"
                            type="password"
                            {...register("confirmPassword", {
                                required: "Xin vui lòng nhập lại mật khẩu",
                                validate: (value) =>
                                    value === getValues("password") ||
                                    "Mật khẩu không khớp",
                            })}
                            InputProps={{
                                disableUnderline: true,
                            }}
                            error={!!errors.confirmPassword}
                            helperText={errors.confirmPassword?.message}
                        />
                    </Box>
                    <Button
                        type="submit"
                        fullWidth
                        sx={{
                            backgroundColor: "primary.main",
                            color: "white",
                            py: 1.5,
                        }}
                    >
                        <strong>Đăng ký</strong>
                    </Button>
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
            </Grid>
        </Grid>
    );
}

export default RegisterPage;
