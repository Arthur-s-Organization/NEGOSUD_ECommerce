// Details of an order
"use client";
import OrderItemCard from "@/components/OrderItem";
import { getCustomerOrderById } from "@/services/customerService";
import { Order, OrderDetail } from "@/services/scheme";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react";

export default function OrderDetailList() {
  const { orderId } = useParams();
  const [order, setOrder] = useState<Order>();

  useEffect(() => {
    const loadItems = async () => {
      try {
        const orderData = await getCustomerOrderById(orderId.toString());
        setOrder(orderData);
      } catch (error) {
        console.error("Erreur lors du chargement de la commande :", error);
      }
    };
    loadItems();
  }, []);
  return (
    <div>
      {!order || order?.orderDetails.length === 0 ? (
        <div className="flex gap-3 w-fit items-center p-4">
          <p className="text-lg">
            Impossible de charger le contenu de la commande
          </p>
        </div>
      ) : (
        <div className="mx-auto max-w-3xl py-24">
          <h1 className="text-xl font-bold mb-10 font-heading text-primary text-center">
            Contenu de la commande {order.orderID}
          </h1>
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
