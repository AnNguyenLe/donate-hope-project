import { Box } from "@mui/material";
import PageHeader from "../../components/layout/PageHeader";
import { Outlet } from "react-router-dom";
import Footer from "../../components/layout/Footer";

export default function Root() {
    return (
        <Box>
            <PageHeader />
            <Outlet />
            <Footer />
        </Box>
    );
}
