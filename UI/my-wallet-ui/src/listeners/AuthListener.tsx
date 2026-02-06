import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export function AuthListener() {
    const navigate = useNavigate();

    useEffect(() => {
        const handler = () => {
            navigate("/login");
        };

        window.addEventListener("auth-expired", handler);

        return () => {
            window.removeEventListener("auth-expired", handler);
        }
    }, [navigate]);

    return null;
}