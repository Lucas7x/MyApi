import React, { useCallback, useState } from "react";
import { ToastPayload, ToastType } from "./toastTypes";
import { ToastContext } from "./ToastContext";
import "./toast.css";

type Toast = ToastPayload & {
    id: number;
}

interface Props {
    children: React.ReactNode;
}

export function ToastProvider({ children }: Props) {
    const [toasts, setToasts] = useState<Toast[]>([]);

    const showToast = useCallback((payload: ToastPayload) => {
        const id = Date.now();
        
        setToasts(prev => [...prev, { ...payload, id }]);
        setTimeout(() => {
            setToasts(prev => prev.filter(toast => toast.id !== id));
        }, 3000);
    }, []);

    return (
        <ToastContext.Provider value={{ showToast }}>
            {children}

            {/* Renderização visual dos toasts */}
            <div className="toast-container">
                {toasts.map(toast => {
                    return <div
                        key={toast.id}
                        className={`app-toast ${toast.type ?? "info"}`}
                    >
                        {toast.message}
                    </div>
                })}
            </div>
        </ToastContext.Provider>
    );
}