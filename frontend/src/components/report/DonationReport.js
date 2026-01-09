import React, { useState, useEffect } from "react";
import {
    Box,
    Typography,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Container,
    TextField,
    TablePagination,
    Button,
} from "@mui/material";
import axiosInstance from "../../utils/axiosInstance";
import * as XLSX from "xlsx";

const DonationReport = ({ campaignId }) => {
    const [donations, setDonations] = useState([]);
    const [filteredDonations, setFilteredDonations] = useState([]);
    const [filters, setFilters] = useState({
        donatorName: "",
        date: "",
        unitOfMeasurement: "",
        amount: "",
        message: "",
    });
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);

    useEffect(() => {
        const fetchDonations = async () => {
            try {
                const response = await axiosInstance.get(
                    `/campaign/${campaignId}/contribution/report`
                );
                setDonations(response.data);
                setFilteredDonations(response.data);
            } catch (error) {
                console.error("Error fetching donation report:", error);
            }
        };

        fetchDonations();
    }, [campaignId]);

    useEffect(() => {
        setFilteredDonations(
            donations.filter(
                (donation) =>
                    donation.donatorName
                        .toLowerCase()
                        .includes(filters.donatorName.toLowerCase()) &&
                    new Date(donation.createdAt)
                        .toLocaleDateString()
                        .includes(filters.date) &&
                    donation.unitOfMeasurement
                        .toLowerCase()
                        .includes(filters.unitOfMeasurement.toLowerCase()) &&
                    donation.amount.toString().includes(filters.amount) &&
                    (donation.message || "")
                        .toLowerCase()
                        .includes(filters.message.toLowerCase())
            )
        );
    }, [filters, donations]);

    const handleFilterChange = (e) => {
        const { name, value } = e.target;
        setFilters({
            ...filters,
            [name]: value,
        });
    };

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    const exportToExcel = () => {
        const exportData = filteredDonations.map((donation) => ({
            donatorName: donation.donatorName,
            dateOfDonation: new Date(donation.createdAt).toLocaleDateString(),
            unitOfMeasurement: donation.unitOfMeasurement,
            amount: donation.amount,
            message: donation.message || "N/A",
        }));

        const worksheet = XLSX.utils.json_to_sheet(exportData);
        const workbook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(workbook, worksheet, "DonationDetails");
        XLSX.writeFile(workbook, "DonationReport.xlsx");
    };

    if (donations.length === 0) {
        return <Typography>Chưa có ủng hộ cho chiến dịch này.</Typography>;
    }

    return (
        <Container>
            <Box sx={{ marginTop: 4 }}>
                <Box
                    sx={{
                        display: "flex",
                        justifyContent: "space-between",
                        alignItems: "center",
                        marginBottom: 2,
                        padding: 2,
                        borderRadius: 2,
                    }}
                >
                    <Typography
                        variant="h5"
                        color="primary.main"
                        fontWeight="bold"
                        sx={{
                            textTransform: "uppercase",
                            letterSpacing: 1.5,
                        }}
                    >
                        SAO KÊ QUYÊN GÓP
                    </Typography>
                    <Button
                        variant="contained"
                        onClick={exportToExcel}
                        sx={{
                            backgroundColor: "primary.main",
                            color: "white",
                            fontWeight: "bold",
                            paddingX: 3,
                            paddingY: 1,
                            boxShadow: 2,
                            transition: "all 0.1s ease",
                            "&:hover": {
                                backgroundColor: "primary.dark",
                                transform: "scale(1.05)",
                            },
                        }}
                    >
                        Kết xuất file Excel
                    </Button>
                </Box>
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                            <TableCell align="center">
                                    <TextField
                                        label="Người quyên góp"
                                        variant="outlined"
                                        size="small"
                                        name="donatorName"
                                        value={filters.donatorName}
                                        onChange={handleFilterChange}
                                        fullWidth
                                    />
                                </TableCell>
                                <TableCell align="center">
                                    <TextField
                                        label="Ngày quyên góp"
                                        variant="outlined"
                                        size="small"
                                        name="date"
                                        value={filters.date}
                                        onChange={handleFilterChange}
                                        fullWidth
                                    />
                                </TableCell>
                                <TableCell align="center">
                                    <TextField
                                        label="Đơn vị tiền tệ"
                                        variant="outlined"
                                        size="small"
                                        name="unitOfMeasurement"
                                        value={filters.unitOfMeasurement}
                                        onChange={handleFilterChange}
                                        fullWidth
                                    />
                                </TableCell>
                                <TableCell align="center">
                                    <TextField
                                        label="Số tiền quyên góp"
                                        variant="outlined"
                                        size="small"
                                        name="amount"
                                        value={filters.amount}
                                        onChange={handleFilterChange}
                                        fullWidth
                                    />
                                </TableCell>
                                <TableCell align="center">
                                    <TextField
                                        label="Tin nhắn"
                                        variant="outlined"
                                        size="small"
                                        name="message"
                                        value={filters.message}
                                        onChange={handleFilterChange}
                                        fullWidth
                                    />
                                </TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {filteredDonations
                                .slice(
                                    page * rowsPerPage,
                                    page * rowsPerPage + rowsPerPage
                                )
                                .map((donation, index) => (
                                    <TableRow
                                        key={donation.id}
                                        sx={{
                                            backgroundColor:
                                                index % 2 === 0
                                                    ? "white"
                                                    : "#e3f2fd",
                                            "&:hover": {
                                                backgroundColor: "#bbdefb",
                                            },
                                        }}
                                    >
                                        <TableCell align="center">
                                            {donation.donatorName}
                                        </TableCell>
                                        <TableCell align="center">
                                            {new Date(
                                                donation.createdAt
                                            ).toLocaleDateString("en-GB")}
                                        </TableCell>
                                        <TableCell align="center">
                                            {donation.unitOfMeasurement}
                                        </TableCell>
                                        <TableCell align="center">
                                            {donation.amount.toLocaleString()}
                                        </TableCell>
                                        <TableCell align="center">
                                            {donation.message || "Không có lời nhắn"}
                                        </TableCell>
                                    </TableRow>
                                ))}
                        </TableBody>
                    </Table>
                </TableContainer>
                <TablePagination
                    rowsPerPageOptions={[5, 10, 25]}
                    component="div"
                    count={filteredDonations.length}
                    rowsPerPage={rowsPerPage}
                    page={page}
                    onPageChange={handleChangePage}
                    onRowsPerPageChange={handleChangeRowsPerPage}
                />
            </Box>
        </Container>
    );
};

export default DonationReport;
