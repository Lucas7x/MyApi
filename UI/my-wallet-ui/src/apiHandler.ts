import axios from "axios";

const apiHandler = axios.create({
    baseURL: 'https://localhost:7240',
});

//Interceptor de resposta
apiHandler.interceptors.response.use(
    response => response,
    error => {
        if (error.response && error.response.status === 401) {
            alert('Sessão expirada. Faça login.');

            // Limpar dados de autenticação
            localStorage.removeItem('token');

            window.location.href = "/login";
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

