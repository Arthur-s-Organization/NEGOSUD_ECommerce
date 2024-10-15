"use client";

import { getCart, removeFromCart } from "@/services/cartService";
import { CartItem } from "@/services/scheme";
import { useEffect } from "react";

export default function PaymentSucess() {
  useEffect(() => {
    const clearCart = async () => {
      try {
        const cartData = await getCart();
        const cartItems = cartData.items;

        if (cartItems.length > 0) {
          await Promise.all(
            cartItems.map(async (cartItem: CartItem) => {
              await removeFromCart(cartItem.item.itemId);
            })
          );
          console.log("Panier vidé avec succès !");
        } else {
          console.log("Le panier est déjà vide.");
        }
      } catch (error) {
        console.error("Erreur lors du vidage du panier :", error);
      }
    };

    clearCart();
  }, []);
  return (
    <div className="px-8 py-6">
      <h1 className="text-3xl font-bold mb-10 font-heading text-primary text-center">
        Paiement effectué
      </h1>
    </div>
  );
}
