import { Routes, Route, Navigate } from "react-router-dom";
import { Login } from "../pages/Login/Login";
import { Home } from "../pages/Home/Home";
import { Persons } from "../pages/Persons/Persons";

export function AppRoutes() {
    return (
        <Routes>
            {/* Rota inicial */}
            <Route path="/" element={<Navigate to="/login" />} />

            <Route path="/login" element={<Login />} />
            <Route path="/home" element={<Home />} />
            <Route path="/persons" element={<Persons />} />
        </Routes>
    );
}