// services/cartService.ts

import axios from 'axios';

const API_URL = 'http://localhost:5165/api/cart'; // Assurez-vous que cela correspond à votre endpoint

export const addToCart = async (itemId: string, quantity: number) => {
  const response = await axios.post(`${API_URL}/add`, {
    itemId,
    quantity,
  });
  return response.data; // Retourne le panier mis à jour
};

export const removeFromCart = async (itemId: string) => {
  const response = await axios.delete(`${API_URL}/remove`, {
    data: { itemId }, // Utilisez "data" pour envoyer le corps de la requête
  });
  return response.data; // Retourne le panier mis à jour
};

export const getCart = async () => {
  const response = await axios.get(API_URL);
  return response.data; // Retourne le panier actuel
};

export const updateCart = async (itemId: string, quantity: number) => {
    const response = await axios.put(`${API_URL}/update`, {
      itemId,
      quantity,
    });
    return response.data; // Retourne le panier mis à jour
  };
