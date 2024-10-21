import Image from "next/image";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "./ui/card";
import { Input } from "./ui/input";
import { useState, useEffect } from "react";
import { CartItem } from "@/services/scheme";
import { fetchItemImage } from "@/services/itemsService";

export default function CartItemCard({
  cartItem,
  updateCartItemQuantity,
}: {
  cartItem: CartItem;
  updateCartItemQuantity: (itemId: string, quantity: number) => void;
}) {
  const [quantity, setQuantity] = useState(cartItem.quantity);
  const [itemImage, setItemImage] = useState<string | null>(null);
  const boxes = Math.floor(cartItem.quantity / 6);
  const bottles = cartItem.quantity % 6;

  useEffect(() => {
    const loadImage = async () => {
      try {
        const imageBlob = await fetchItemImage(cartItem.item.itemId);
        if (imageBlob) {
          const imageUrl = URL.createObjectURL(imageBlob);
          setItemImage(imageUrl);
        }
      } catch (error) {
        console.error("Erreur lors du chargement de l'image :", error);
      }
    };
    loadImage();
  }, []);

  const handleQuantityChange = (value: string) => {
    const newQuantity = Number(value);
    setQuantity(newQuantity);
    updateCartItemQuantity(cartItem.item.itemId, newQuantity);
  };

  return (
    <Card className="border border-primary w-full items-center flex">
      <CardHeader className="text-center">
        <CardTitle className="text-md ">{cartItem.item.name}</CardTitle>
      </CardHeader>
      <CardContent className="p-0">
        <Image
          src={itemImage || "/image-placeholder.png"}
          alt={"item image"}
          width={207}
          height={300}
          className="w-1/2"
        />
      </CardContent>
      <CardFooter className="flex gap-10 w-full p-0">
        <p>Prix : {cartItem.item.price} €</p>
        <div className="flex items-center gap-2">
          Quantité :
          <Input
            type="number"
            value={quantity}
            onChange={(e) => handleQuantityChange(e.target.value)}
            className="w-fit"
            min={0}
          />
        </div>
        {cartItem.item.category === "alcohol" && (
          <div>
            {boxes > 0 && <p>Carton(s) : {boxes} </p>}
            {bottles > 0 && <p>Bouteille(s) : {bottles} </p>}
          </div>
        )}
        <p>Total : {cartItem.item.price * quantity} €</p>
      </CardFooter>
    </Card>
  );
}
