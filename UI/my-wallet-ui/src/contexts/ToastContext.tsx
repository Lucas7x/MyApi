import React, { createContext, useContext, useState } from "react";

type ToastType = "success" | "error";

interface Toast {
    message: string;
    type: ToastType;
}

interface ToastContextData {
    showToast: (message: string, type?: ToastType) => void;
}

const ToastContext = createContext<ToastContextData>(
    {} as ToastContextData
);

export function ToastProvider({ children }: { children: React.ReactNode }) {
    const [ toast, setToast ] = useState<Toast | null>(null);
    
    function showToast(message: string, type: ToastType = "success") {
        setToast({ message, type });
        setTimeout(() => {
            setToast(null)
        }, 3000);
    }

    return (
        <ToastContext.Provider value={{ showToast }}>
            {children}

            {toast && (
                <div 
                    className="toast-container position-fixed top-0 end-0 p-3" 
                    style={{ zIndex: 9999 }}
                >
                    <div
                        className={`toast show align-items-center text-bg-${toast.type} border-0`}
                    >
                        <div className="d-flex">
                            <div className="toast-body">{toast.message}</div>
                            <button
                                type="button"
                                className="btn-close btn-close-white me-2 m-auto"
                                onClick={() => setToast(null)}
                            />
                        </div>
                    </div>
                </div>
            )}
        </ToastContext.Provider>
    );
}

export function useToast() {
    return useContext(ToastContext);
}