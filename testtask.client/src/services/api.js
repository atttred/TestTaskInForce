import axios from 'axios';

const API_URL = 'http://localhost:5125';

const api = axios.create({
    baseURL: API_URL,
    withCredentials: true,
})

export const aboutService = {
    getAboutContent: async () => {
        const response = await api.get('/About');
        return response.data;
    },
    updateAboutContent: async (content) => {
        const response = await api.put('/About', { content });
        return response.data;
    }
}

export const shortUrlService = {
    getAllUrls: async () => {
        const response = await api.get('/ShortUrl');
        return response.data;
    },
    createShortUrl: async (originalUrl) => {
        const response = await api.post('/ShortUrl', { originalUrl });
        return response.data;
    },
    getUrlDetails: async (shortCode) => {
        const response = await api.get(`/ShortUrl/${shortCode}`);
        return response.data;
    },
    deleteUrl: async (shortCode) => {
        const response = await api.delete(`/ShortUrl/${shortCode}`);
        return response.data;
    },
    redirectToOriginal: async (shortCode) => {
        window.location.href = `${API_URL}/ShortUrl/redirect/${shortCode}`;
    },
};

export const userService = {
    register: async (email, username, password, rememberMe) => {
        const response = await api.post('/User/register', { email, username, password, rememberMe });
        return response.data;
    },
    login: async (email, password, rememberMe) => {
        const response = await api.post('/User/login', { email, password, rememberMe });
        return response.data;
    },
    logout: async () => {
        const response = await api.post('/User/logout');
        return response.data;
    },
    getCurrentUser: async () => {
        const response = await api.get('/User/current');
        return response.data;
    },
};