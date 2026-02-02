import { useEffect } from "react";
import { useToast } from "./ToastContext";
import { registerToastListener } from "../../events/toastEvent";

export function ToastListener() {
    const {showToast} = useToast();

    useEffect(() => {
        registerToastListener(showToast);
    }, [showToast]);

    return null;
}