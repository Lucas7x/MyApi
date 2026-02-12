import { Routes, Route, Navigate } from "react-router-dom";
import { Login } from "../pages/Login/Login";
import { Home } from "../pages/Home/Home";
import { Persons } from "../pages/Persons/Persons";
import { ProtectedRoute } from "./ProtectedRoutes";
import { UserRegister } from "../pages/Users/UserRegister";

export function AppRoutes() {
    return (
        <Routes>
            {/* Rota inicial */}
            <Route path="/" element={<Navigate to="/home" />} />

            <Route path="/login" element={<Login />} />
            <Route path="/users/register" element={<UserRegister />} />

            <Route path="/home" element={
                <ProtectedRoute>
                    <Home />
                </ProtectedRoute>
            } />
            <Route path="/persons" element={
                <ProtectedRoute>
                    <Persons />
                </ProtectedRoute>
            } />
        </Routes>
    );
}