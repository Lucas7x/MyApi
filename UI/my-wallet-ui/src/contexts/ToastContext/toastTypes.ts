export type ToastType = "success" | "error" | "warning" | "info";


export type ToastPayload = {
    message: string;
    type: ToastType;
}

export interface ToastContextData {
    showToast: (payload: ToastPayload) => void;
}