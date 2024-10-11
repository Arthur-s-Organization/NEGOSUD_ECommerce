// services/authService.ts

import axios from 'axios';

const API_URL = 'http://localhost:5165/api/Auth/';

export const login = async (username: string, password: string) => {
  const response = await axios.post(`${API_URL}login`, {
    username,
    password,
  });

  if (response.data.token) {
    // Enregistrez le token dans localStorage ou un cookie
    localStorage.setItem('token', response.data.token);
    return response.data.token;
  }

  throw new Error('Erreur lors de la connexion');
};

export const logout = () => {
  // Supprimez le token lors de la déconnexion
  localStorage.removeItem('token');
};