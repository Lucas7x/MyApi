import { JSX } from "react";
import { Navigate } from "react-router-dom";
import { emitPersistentToast } from "../events/toastEvent";

interface ProtectedRouteProps {
    children: JSX.Element;
}

export function ProtectedRoute({ children }: ProtectedRouteProps) {
    const token = localStorage.getItem("token");

    if (!token) {
        emitPersistentToast({
            message: "Faça login para continuar", 
            type: "warning"
        });

        return <Navigate
            to="/login" 
            replace
        />;
    }

    return children;
}