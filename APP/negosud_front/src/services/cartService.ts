import axios from "@/app/main";
import { getSupplierById } from "./supplierService";
import { getAlcoholFamilyById } from "./alcoholFamilyService";
import { CartItem, zCart, zCartItem } from "./scheme";

const API_URL = "http://localhost:5165/api/Cart";

export const addToCart = async (itemId: string, quantity: number) => {
  const response = await axios.post(`${API_URL}/add`, {
    itemId,
    quantity,
  });
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
    const cartItems = response.data;

    // Pour chaque item du panier, on récupère supplier et alcoholFamily en parallèle
    const enrichedCartItems = await Promise.all(
      cartItems.items.map(async (cartItem: { item: any, quantity: number }) => {
        const { item } = cartItem;

        const supplier = await getSupplierById(item.supplierId); // Récupérer le supplier
        const alcoholFamily = await getAlcoholFamilyById(item.alcoholFamilyId); // Récupérer la alcoholFamily

        // Enrichir l'objet `item` avec les données récupérées
        const enrichedItem = {
          ...item,
          supplier, // Ajouter l'objet supplier
          alcoholFamily, // Ajouter l'objet alcoholFamily
        };

        return {
          item: enrichedItem, // Remplacer l'item par l'item enrichi
          quantity: cartItem.quantity, // Conserver la quantité
        };
      })
    );

    return zCart.parse(enrichedCartItems) ;
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
