import { createContext, useState, useEffect } from 'react';
import { userService } from '../services/api';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchUser = async () => {
            try {
                const currentUser = await userService.getCurrentUser();
                setUser(currentUser.isAuthenticated ? currentUser : null);
            } catch (error) {
                setUser(null);
            } finally {
                setLoading(false);
            }
        };
        fetchUser();
    }, []);

    const login = async (email, password, rememberMe) => {
        const result = await userService.login(email, password, rememberMe);
        if (result.isSuccess) {
            const currentUser = await userService.getCurrentUser();
            setUser(currentUser);
        }
        return result;
    };

    const logout = async () => {
        await userService.logout();
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, login, logout, loading }}>
            {children}
        </AuthContext.Provider>
    );
};