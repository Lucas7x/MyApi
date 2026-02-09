import { useLocation, useNavigate } from 'react-router-dom';
import apiHandler from '../../apiHandler';
import { useEffect, useState } from 'react';
import { LoginRequest, LoginResponse } from '../../types/Auth';
import { useToast } from '../../contexts/ToastContext/ToastContext';
import '../../styles/global.css';
import '../../styles/login.css';

export function Login() {
    const navigate = useNavigate();
    const { showToast } = useToast();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    useEffect(() => {
        const storedToast = sessionStorage.getItem("pendingToast");

        if (storedToast) {
            showToast(JSON.parse(storedToast));
            sessionStorage.removeItem("pendingToast");
            return;
        }
    }, [showToast]);

    async function handleLogin(e: React.FormEvent) {
        e.preventDefault();
        setError("");

        const payload: LoginRequest = {
            email: email,
            password: password
        };

        try {
            const response = await apiHandler.post<LoginResponse>("/users/login", payload);

            localStorage.setItem("token", response.data.token);

            showToast({
                message: "Login realizado com sucesso!",
                type: "success"
            });
            navigate("/home");
        } catch (err: any) {
            if (err.response?.status === 401) {
                showToast({
                    message: "E-mail ou senha inválidos",
                    type: "error"
                });
            } else {
                showToast({
                    message: "Erro ao realizar login",
                    type: "error"
                });
            }
        }
    }

    return (
        <div
            className='login-page'
        >
            <form
                className="auth-form"
                onSubmit={handleLogin}
            >
                <h2>Login</h2>
                {error && <p style={{ color: "red" }}> {error} </p>}

                <div className="input-field">
                    <label htmlFor="email">E-mail</label>
                    <input type="email"
                        id='email'
                        placeholder='digite seu e-mail'
                        value={email}
                        onChange={e => setEmail(e.target.value)}
                        required
                    />
                </div>

                <div className="input-field">
                    <label htmlFor="password">Senha</label>
                    <input type="password"
                        id='password'
                        placeholder='Senha'
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        required
                    />
                </div>

                <button type='submit'>Entrar</button>

                <p className='signup-link'>Não tem conta? 
                    <a href="/signup"
                        onClick={() => navigate('/signup')}
                    > Cadastre-se</a>
                </p>
            </form>
        </div>
    );
}