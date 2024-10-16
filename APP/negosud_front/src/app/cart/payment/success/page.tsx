"use client";

import { getCart, removeFromCart } from "@/services/cartService";
import {
  createCustomerOrder,
  createCustomerOrderLine,
} from "@/services/customerService";
import { CartItem } from "@/services/scheme";
import { useEffect, useRef } from "react";

export default function PaymentSucess() {
  const clearCartAndPlaceOrder = async () => {
    try {
      const userId = localStorage.getItem("userId");
      if (!userId) return;

      const { orderID } = await createCustomerOrder("1", userId);
      const { items: cartItems } = await getCart();

      if (cartItems.length === 0) {
        console.log("Le panier est déjà vide.");
        return;
      }
      for (const cartItem of cartItems) {
        const {
          item: { itemId },
          quantity,
        } = cartItem;

        await createCustomerOrderLine(orderID, itemId, quantity);
        await removeFromCart(itemId);
      }
      console.log("Panier vidé avec succès !");
    } catch (error) {
      console.error("Erreur lors du vidage du panier :", error);
    }
  };

  const isTriggered = useRef(false);

  useEffect(() => {
    if (isTriggered.current) return;
    isTriggered.current = true;

    clearCartAndPlaceOrder();
  }, []);
  return (
    <div className="px-8 py-6">
      <h1 className="text-3xl font-bold mb-10 font-heading text-primary text-center">
        Paiement effectué
      </h1>
    </div>
  );
}
