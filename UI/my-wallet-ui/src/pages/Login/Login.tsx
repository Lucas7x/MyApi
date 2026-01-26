import { useNavigate } from 'react-router-dom';
import apiHandler from '../../apiHandler';
import { useState } from 'react';
import { LoginRequest, LoginResponse } from '../../types/Auth';
import { useToast } from '../../contexts/ToastContext';

export function Login() {
    const navigate = useNavigate();
    const { showToast } = useToast();

    const [ email, setEmail ] = useState("");
    const [ password, setPassword ] = useState("");
    const [ error, setError ] = useState("");

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

            showToast("Login realizado com sucesso!");
            navigate("/home");
        } catch (err: any) {
            if (err.response?.status === 401) {
                showToast("E-mail ou senha inválidos", "error");
            } else {
                showToast("Erro ao realizar login", "error");
            }
        }
    }

    return (
        <div style={{display: 'flex', height: '100vh', alignItems: 'center', justifyContent: 'center'}}>
            <form onSubmit={handleLogin}>
                <h2>Login</h2>
                { error && <p style={{ color: "red" }}> {error} </p> }

                <input type="email" 
                    placeholder='E-mail' 
                    value={email}
                    onChange={e => setEmail(e.target.value)}
                    required    
                />
                <br /><br />

                <input type="password" 
                    placeholder='Senha'
                    value={password}
                    onChange={e => setPassword(e.target.value)}
                    required 
                />
                <br /><br />

                <button type='submit'>Entrar</button>
            </form>
        </div>
    );
}