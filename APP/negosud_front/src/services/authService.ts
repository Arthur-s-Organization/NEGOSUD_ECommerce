// services/authService.ts

import axios from "@/app/main";
import Cookies from "js-cookie";

const API_URL = "http://localhost:5165/api/Auth/";

export const login = async (username: string, password: string) => {
  const response = await axios.post(
    `${API_URL}login`,
    {
      username,
      password,
    },
    { withCredentials: true }
  );

  if (response.data.token) {
    localStorage.setItem("userId", response.data.userId);
    return response.data.token;
  }

  throw new Error("Erreur lors de la connexion");
};

export const logout = async () => {
  const response = await axios.post(
    `${API_URL}logout`,
  );

  if (response.status === 200) {
    localStorage.removeItem("userId");
    return;
  }

  throw new Error("Erreur lors de la déconnexion");
};
