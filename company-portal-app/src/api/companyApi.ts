import axios from 'axios';
import { refreshToken } from './authApi';
import { API_URLS } from '../config/config';

const apiClient = axios.create();

// Request interceptor to include token
apiClient.interceptors.request.use((config) => {
    const token = localStorage.getItem('authToken');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

// Response interceptor to refresh token if necessary
apiClient.interceptors.response.use(
    response => response,
    async error => {
        const originalRequest = error.config;

        // If 401 error and the request has not already been retried
        if (error.response.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            try {
                const token = localStorage.getItem('authToken');
                const response = await refreshToken(token!);
                localStorage.setItem('authToken', response.data.token);
                axios.defaults.headers.common['Authorization'] = `Bearer ${response.data.token}`;
                return apiClient(originalRequest); 
            } catch (err) {
                console.error("Token refresh failed:", err);
            }
        }
        return Promise.reject(error);
    }
);

export const getAllCompanies = async () => apiClient.get(API_URLS.company.base);

export const getCompanyById = async (id: number) => apiClient.get(`${API_URLS.company.base}/${id}`);

export const createCompany = async (company: any) => apiClient.post(API_URLS.company.base, company);

export const updateCompany = async (id: number, company: any) => apiClient.put(`${API_URLS.company.base}/${id}`, company);

export const deleteCompany = async (id: number) => apiClient.delete(`${API_URLS.company.base}/${id}`);
