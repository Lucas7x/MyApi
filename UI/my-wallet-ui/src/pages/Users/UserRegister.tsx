import { useNavigate } from "react-router-dom";
import { useToast } from "../../contexts/ToastContext/ToastContext";
import { useState } from "react";
import { UserRegisterRequest } from "../../types/UserTypes";
import apiHandler from "../../apiHandler";
import "../../styles/global.css";
import "../../styles/userRegister.css";


export function UserRegister() {
    type Errors = {
        Name?: string;
        Email?: string;
        Phone?: string;
        Password?: string;
    }
    
    const navigate = useNavigate();
    const { showToast } = useToast();
    const [errors, setErrors] = useState<Errors>({});
    const [name, setName] = useState("");
    const [phone, setPhone] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [loading, setLoading] = useState(false);
    
    async function handleUserRegister(e: React.FormEvent) {
        e.preventDefault();

        setErrors({});
        setLoading(true);

        if (password !== confirmPassword) {
            showToast({
                message: "As senhas não coincidem",
                type: "error"
            });
            setLoading(false);
            return;
        }

        const payload:  UserRegisterRequest = {
            name: name,
            email: email,
            phone: phone,
            password: password
        }

        try {
            await apiHandler.post("/users/register", payload);
            
            showToast({
                message: "Registro realizado com sucesso! Faça login para continuar.",
                type: "success"
            });

            navigate("/login");
        } catch (err: any) {
            if (err.response?.status === 400) {
                setErrors(err.response.data.errors);
            } else if (err.response?.status === 409) {
                showToast({
                    message: "Este e-mail já está em uso",
                    type: "error"
                });
            } else {
                showToast({
                    message: "Erro interno do servidor. Tente novamente mais tarde.",
                    type: "error"
                });
            }
        } 
    }

    return (
        <div className="register-user-page">
            <form className="register-user-form"
                onSubmit={handleUserRegister}
            >
                <h2>Cadastre-se</h2>

                <div className="input-field">
                    <label htmlFor="name">Nome</label>
                    <input type="text"
                        id="name"
                        className={`${errors.Name ? "is-invalid" : ""}`}
                        placeholder="digite seu nome"
                        value={name}
                        onChange={e => setName(e.target.value)}
                        required
                    />
                    {
                        errors.Name && (
                            <div className='custom-invalid-feedback'>
                                {errors.Name[0]}
                            </div>
                        )
                    }
                </div>

                <div className="input-field">
                    <label htmlFor="email">E-mail</label>
                    <input type="email"
                        id="email"
                        className={`${errors.Email ? "is-invalid" : ""}`}
                        placeholder="digite seu e-mail"
                        value={email}
                        onChange={e => setEmail(e.target.value)}
                        required
                    />
                    {
                        errors.Email && (
                            <div className='custom-invalid-feedback'>
                                {errors.Email[0]}
                            </div>
                        )
                    }
                </div>

                <div className="input-field">
                    <label htmlFor="phone">Telefone</label>
                    <input type="text"
                        id="phone"
                        className={`${errors.Phone ? "is-invalid" : ""}`}
                        placeholder="(00)00000-0000"
                        value={phone}
                        onChange={e => setPhone(e.target.value)}
                    />
                    {
                        errors.Phone && (
                            <div className='custom-invalid-feedback'>
                                {errors.Phone[0]}
                            </div>
                        )
                    }
                </div>

                <div className="input-field">
                    <label htmlFor="password">Senha</label>
                    <input type="password"
                        id="password"
                        className={`${errors.Password ? "is-invalid" : ""}`}
                        placeholder="digite sua senha"
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        required
                    />
                    {
                        errors.Password && (
                            <div className='custom-invalid-feedback'>
                                {errors.Password[0]}
                            </div>
                        )
                    }
                </div>

                <div className="input-field">
                    <label htmlFor="confirmPassword">Confirmar Senha</label>
                    <input type="password"
                        id="confirmPassword"
                        placeholder="confirme a senha digitada"
                        value={confirmPassword}
                        onChange={e => setConfirmPassword(e.target.value)}
                        required
                    />
                </div>

                <button type="submit">Cadastrar</button>
                <button type="button"
                    className="cancel-button"
                    onClick={() => navigate("/login")}
                >
                    Cancelar
                </button>
            </form>
        </div>
    )
}