"use client";
import Image from "next/image";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "./ui/card";
import { OrderDetail } from "@/services/scheme";
import { useEffect, useState } from "react";
import { fetchItemImage } from "@/services/itemsService";

export default function OrderItemCard({
  orderItem,
}: {
  orderItem: OrderDetail;
}) {
  const [productImage, setProductImage] = useState<string | null>(null);

  useEffect(() => {
    const loadImage = async () => {
      try {
        const imageBlob = await fetchItemImage(orderItem.item.itemId);
        if (imageBlob) {
          const imageUrl = URL.createObjectURL(imageBlob);
          setProductImage(imageUrl);
        }
      } catch (error) {
        console.error("Erreur lors du chargement de l'image :", error);
      }
    };
    loadImage();
  }, [orderItem.item.itemId]);

  const boxes = Math.floor(orderItem.quantity / 6);
  const bottles = orderItem.quantity % 6;

  return (
    <Card className="border border-primary w-full items-center flex py-4">
      <CardHeader className="text-center">
        <CardTitle className="text-md ">{orderItem.item.name}</CardTitle>
      </CardHeader>
      <CardContent className="p-0">
        <Image
          src={productImage || "/image-placeholder.png"}
          alt={"item image"}
          width={207}
          height={300}
          className="w-1/2"
          unoptimized={true}
        />
      </CardContent>
      <CardFooter className="flex gap-10 w-full p-0">
        <p>Prix : {orderItem.item.price} €</p>
        <p className="flex items-center gap-2">
          Quantité : {orderItem.quantity}
        </p>
        {orderItem.item.category === "alcohol" && (
          <div>
            {boxes > 0 && <p>Carton(s) : {boxes} </p>}
            {bottles > 0 && <p>Bouteille(s) : {bottles} </p>}
          </div>
        )}
        <p>Total : {orderItem.item.price * orderItem.quantity} €</p>
      </CardFooter>
    </Card>
  );
}
