import { useNavigate } from 'react-router-dom';

export function Login() {
    const navigate = useNavigate();

    function handleLogin(e: React.FormEvent) {
        e.preventDefault();

        //futuramente validar email e senha
        navigate("/home");
    }

    return (
        <div style={{display: 'flex', height: '100vh', alignItems: 'center', justifyContent: 'center'}}>
            <form onSubmit={handleLogin}>
                <h2>Login</h2>

                <input type="email" placeholder='E-mail' />
                <br /><br />

                <input type="password" placeholder='Senha' />
                <br /><br />

                <button type='submit'>Entrar</button>
            </form>
        </div>
    );
}