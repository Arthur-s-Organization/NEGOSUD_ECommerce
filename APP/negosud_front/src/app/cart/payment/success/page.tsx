"use client";
import { getCart, removeFromCart } from "@/services/cartService";
import {
  createCustomerOrder,
  createCustomerOrderLine,
  updateCustomerOrder,
  getCustomerOrderById,
} from "@/services/customerService";
import { useSearchParams, useRouter } from "next/navigation";
import { useEffect, useRef, useState } from "react";
import OrderItemCard from "@/components/OrderItem";
import { Order, OrderDetail } from "@/services/scheme";

export default function PaymentSuccess() {
  const router = useRouter();
  const searchParams = useSearchParams();
  const validAccess = searchParams.get("validAccess");
  const isTriggered = useRef(false);
  const [order, setOrder] = useState<Order>();

  useEffect(() => {
    if (validAccess !== "true") {
      router.push("/cart");
    }
  }, [validAccess, router]);

  // When the payment is sucessful : clear the cart, create a customer order and the order lines
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

      const orderData = await getCustomerOrderById(orderID);
      setOrder(orderData);
    } catch (error) {
      console.error("Erreur lors du traitement de la commande :", error);
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
      {!order || order?.orderDetails.length === 0 ? (
        <div className="flex gap-3 w-fit items-center p-4">
          <p className="text-lg">
            Impossible de charger le contenu de la commande
          </p>
        </div>
      ) : (
        <div className="mx-auto max-w-3xl ">
          <h2 className="text-xl font-bold mb-10 font-heading text-primary text-center">
            Contenu de la commande {order.orderID}
          </h2>
          <div className="flex flex-col gap-6">
            {order?.orderDetails.map((orderLine: OrderDetail) => (
              <OrderItemCard
                key={orderLine.item.itemId}
                orderItem={orderLine}
              />
            ))}
          </div>
        </div>
      )}
    </div>
  );
}
