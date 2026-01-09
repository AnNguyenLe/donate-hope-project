import { useSelector } from "react-redux";
import AnonymousAccount from "./AnonymousAccount";
import SignedInAccount from "./SignedInAccount";

export default function UserAccount() {
	const appUser = useSelector((state) => state.appUser);
	const component =
		appUser && appUser.data ? (
			<SignedInAccount appUser={appUser.data} />
		) : (
			<AnonymousAccount />
		);
	return component;
}
