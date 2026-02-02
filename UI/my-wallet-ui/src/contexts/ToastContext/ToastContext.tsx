import React, { createContext, useContext, useState } from "react";
import { ToastContextData } from "./toastTypes";


export const ToastContext = createContext<ToastContextData | undefined>(
    undefined
);

export function useToast() {
    const context = useContext(ToastContext);

    if (!context) {
        throw new Error("useToast deve ser usado dentro de ToastProvider");
    }

    return context;
}