import { useEffect } from "react";
import { useToast } from "../contexts/ToastContext/ToastContext";
import { registerToastListener } from "../events/toastEvent";

export function ToastListener() {
    const {showToast} = useToast();

    useEffect(() => {
        registerToastListener(showToast);
    }, [showToast]);

    return null;
}