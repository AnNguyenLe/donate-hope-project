import {
	createBrowserRouter,
	Navigate,
	RouterProvider,
} from "react-router-dom";
import LoginPage from "./pages/user/LoginPage";
import Root from "./pages/layout/Root";
import HomePage from "./pages/HomePage";
import RegisterPage from "./pages/user/RegisterPage";
import CampaignsListPage from "./pages/campaign/CampaignsListPage";
import CampaignDetailPage from "./pages/campaign/CampaignDetailPage";
import CampaignCreate from "./components/campaign/CampaignCreate";
import CampaignContributionDetailPage from "./pages/campaignContribution/CampaignContributionDetailPage";
import { useEffect } from "react";
import { useSelector } from "react-redux";
import CharityRegisterPage from "./pages/user/CharityRegisterPage";
import AboutUsPage from "./pages/AboutUsPage";
import ContactPage from "./pages/ContactPage";

const ProtectedRoute = ({ children }) => {
	const appUser = useSelector((state) => state.appUser);

	if (!appUser?.data) {
		return <Navigate to='/signin' replace />;
	}

	return children;
};

const router = createBrowserRouter([
	{
		path: "/",
		element: <Root />,
		children: [
			{
				index: true,
				element: <HomePage />,
			},
			{
				path: "/signin",
				element: <LoginPage />,
			},
			{
				path: "/signup",
				element: <RegisterPage />,
			},
			{
				path: "/about-us",
				element: <AboutUsPage />,
			},
			{
				path: "/contact",
				element: <ContactPage />,
			},
			{
				path: "/charity/signup",
				element: <CharityRegisterPage />,
			},
			{
				path: "/signout",
				element: <LoginPage />,
			},
			{
				path: "/campaign/:id",
				element: (
					<ProtectedRoute>
						<CampaignDetailPage />
					</ProtectedRoute>
				),
			},
			{
				path: "/campaign",
				element: (
					<ProtectedRoute>
						<CampaignsListPage />
					</ProtectedRoute>
				),
			},
			{
				path: "/campaign/create",
				element: (
					<ProtectedRoute>
						<CampaignCreate />
					</ProtectedRoute>
				),
			},
			{
				path: "/campaign/:id/contribute",
				element: (
					<ProtectedRoute>
						<CampaignContributionDetailPage />
					</ProtectedRoute>
				),
			},
			{
				path: "/",
				element: (
					<ProtectedRoute>
						<HomePage />
					</ProtectedRoute>
				),
			},
		],
	},
]);

const App = () => {
	useEffect(() => {
		document.title = "Donate Hope";
	}, []);
	return <RouterProvider router={router} />;
};

export default App;
