// services/cartService.ts

import axios from "@/app/main";

const API_URL = "http://localhost:5165/api/Cart"; // Assurez-vous que cela correspond à votre endpoint

export const addToCart = async (itemId: string, quantity: number) => {
  const response = await axios.post(`http://localhost:5165/api/Cart/add`, {
    itemId,
    quantity,
  });
  console.log("add : ", response.data);
  return response.data;
};

export const removeFromCart = async (itemId: string) => {
  const response = await axios.delete(`${API_URL}/remove`, {
    data: { itemId },
  });
  return response.data;
};

export const getCart = async () => {
  try {
    const response = await axios.get(API_URL);
    console.log("get : ", response.data);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération du panier :", error);
    return [];
  }
};

export const updateCart = async (itemId: string, quantity: number) => {
  const response = await axios.put(`${API_URL}/update`, {
    itemId,
    quantity,
  });
  return response.data;
};
