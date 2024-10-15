import { useEffect, useState } from "react";
import type { Cart, CartItem } from "@/services/scheme";
import CartItemCard from "@/components/CartItem";
import { getCart, removeFromCart, updateCart } from "@/services/cartService";
import { Button } from "./ui/button";
import Link from "next/link";

export default function Cart() {
  const [cart, setCart] = useState<Cart>([]);

  const updateCartItemQuantity = async (
    itemId: string,
    newQuantity: number
  ) => {
    if (newQuantity <= 0) {
      setCart((prevCart) =>
        prevCart.filter((cartItem) => cartItem.item.itemId !== itemId)
      );
      await removeFromCart(itemId);
      return;
    }
    setCart((prevCart) =>
      prevCart.map((cartItem) =>
        cartItem.item.itemId === itemId
          ? { ...cartItem, quantity: newQuantity }
          : cartItem
      )
    );
    await updateCart(itemId, newQuantity);
  };

  useEffect(() => {
    const loadItems = async () => {
      try {
        const cartData = await getCart();
        setCart(cartData.items);
      } catch (error) {
        console.error("Erreur lors du chargement du panier :", error);
        setCart([]);
      }
    };
    loadItems();
  }, []);

  return (
    <div>
      {cart.length === 0 ? (
        <div className="flex gap-3 w-fit items-center">
          <p className="text-lg">Votre panier est vide.</p>
          <Button variant="secondary">
            <Link href="/products">Consultez nos produits</Link>
          </Button>
        </div>
      ) : (
        <div className="flex flex-col gap-6">
          {cart.map((cartItem: CartItem) => (
            <CartItemCard
              key={cartItem.item.itemId}
              cartItem={cartItem}
              updateCartItemQuantity={updateCartItemQuantity}
            />
          ))}
        </div>
      )}
    </div>
  );
}
