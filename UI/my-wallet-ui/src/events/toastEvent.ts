import { ToastPayload } from "../contexts/ToastContext/toastTypes";


type ToastListener = (payload: ToastPayload) => void;

let listener: ToastListener | null = null;

export function registerToastListener(fn: ToastListener) {
    listener = fn;
}

export function emitToast(payload: ToastPayload) {
    listener?.(payload);
}

export function emitPersistentToast(payload: ToastPayload) {
    sessionStorage.setItem("pendingToast", JSON.stringify(payload));
}