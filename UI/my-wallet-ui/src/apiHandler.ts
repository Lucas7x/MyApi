import axios from "axios";
import { emitToast } from "./events/toastEvent";

const apiHandler = axios.create({
    baseURL: 'https://localhost:7240',
});

//Interceptor de resposta
apiHandler.interceptors.response.use(
    response => response,
    error => {
        const status = error.response.status;

        if (status === 401) {
            window.location.href = "/login";

            // Limpar dados de autenticação
            localStorage.removeItem('token');
        }

        if (status === 500) {
            emitToast({
                message: "Erro interno. Tente novamente mais tarde.",
                type: "error",
            });
        }

        return Promise.reject(error);
    }
);


apiHandler.interceptors.request.use ( config => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});


export default apiHandler;

