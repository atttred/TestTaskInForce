import { useState, useContext } from 'react';
import { userService } from '../../services/api';
import { AuthContext } from '../../context/AuthContext';

const Auth = () => {
    const { user, login, logout } = useContext(AuthContext);
    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [rememberMe, setRememberMe] = useState(false);
    const [message, setMessage] = useState('');

    const handleRegister = async () => {
        try {
            const result = await userService.register(email, username, password, rememberMe);
            setMessage(result.message);
        } catch (error) {
            setMessage(error.response?.data?.message || 'Registration failed');
        }
    };

    const handleLogin = async () => {
        try {
            const result = await login(email, password, rememberMe);
            setMessage(result.message);
        } catch (error) {
            setMessage(error.response?.data?.message || 'Login failed');
        }
    };

    const handleLogout = async () => {
        try {
            await logout();
            setMessage('Logged out successfully');
        } catch (error) {
            setMessage('Logout failed');
        }
    };

    if (user) {
        return (
            <div>
                <h1>Welcome, {user.userName}</h1>
                <button onClick={handleLogout}>Logout</button>
                <p>{message}</p>
            </div>
        );
    }

    return (
        <div>
            <h1>Auth</h1>
            <input value={email} onChange={(e) => setEmail(e.target.value)} placeholder="Email" />
            <input value={username} onChange={(e) => setUsername(e.target.value)} placeholder="Username" />
            <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Password" />
            <label>
                <input type="checkbox" checked={rememberMe} onChange={(e) => setRememberMe(e.target.checked)} />
                Remember Me
            </label>
            <button onClick={handleRegister}>Register</button>
            <button onClick={handleLogin}>Login</button>
            <p>{message}</p>
        </div>
    );
};

export default Auth;