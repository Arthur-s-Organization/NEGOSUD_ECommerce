"use client";

import { getCart, removeFromCart } from "@/services/cartService";
import {
  createCustomerOrder,
  createCustomerOrderLine,
  updateCustomerOrder,
} from "@/services/customerService";
import { useSearchParams, useRouter } from "next/navigation";
import { useEffect, useRef } from "react";

export default function PaymentSucess() {
  const router = useRouter();
  const searchParams = useSearchParams();
  const validAccess = searchParams.get("validAccess");
  const isTriggered = useRef(false);

  useEffect(() => {
    if (validAccess !== "true") {
      router.push("/cart");
    }
  }, [validAccess, router]);

  const clearCartAndPlaceOrder = async () => {
    try {
      const userId = localStorage.getItem("userId");
      if (!userId) return;

      const { orderID } = await createCustomerOrder("0", userId);
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
      await updateCustomerOrder("1", userId, orderID);
    } catch (error) {
      console.error("Erreur lors du vidage du panier :", error);
    }
  };

  useEffect(() => {
    if (validAccess === "true" && !isTriggered.current) {
      isTriggered.current = true;
      clearCartAndPlaceOrder();
    }
  }, [validAccess]);
  return (
    <div className="px-8 py-6">
      <h1 className="text-3xl font-bold mb-10 font-heading text-primary text-center">
        Paiement effectué
      </h1>
    </div>
  );
}
