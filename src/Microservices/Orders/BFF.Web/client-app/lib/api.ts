import axios from 'axios';
import appConfigData from '../app.config.json';

// Create axios instance with default config
const api = axios.create({
  baseURL: appConfigData.config.ordersBffUrl,
  withCredentials: true,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add response interceptor for error handling
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      // Redirect to silent login if unauthorized
      window.location.href = `${appConfigData.config.ordersBffUrl}/api/auth/silent-login?returnUrl=${encodeURIComponent(window.location.pathname)}`;
    }
    return Promise.reject(error);
  },
);

export default api;