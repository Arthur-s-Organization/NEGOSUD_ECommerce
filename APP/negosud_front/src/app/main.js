import axiosInstance from "axios";
import Cookies from "js-cookie";

const baseURL = "http://localhost:5165/api";

const axios = axiosInstance.create({
  baseURL,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "*",
  },
  withCredentials: true,
});

axios.interceptors.request.use(
  async (config) => {
    const token = Cookies.get("negosudToken");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default axios;
