import { useEffect, useState } from "react";
import type { Cart, CartItem } from "@/services/scheme";
import CartItemCard from "@/components/CartItem";
import { getCart, removeFromCart, updateCart } from "@/services/cartService";
import { Button } from "./ui/button";
import Link from "next/link";
import {
  Card,
  CardHeader,
  CardTitle,
  CardContent,
  CardFooter,
} from "./ui/card";
import { useRouter } from "next/navigation";

export default function Cart() {
  const [cart, setCart] = useState<Cart>([]);
  const [totalPrice, setTotalPrice] = useState<Number>(0);

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

  useEffect(() => {
    if (cart.length > 0) {
      setTotalPrice(
        cart.reduce((total, cartItem) => {
          console.log(
            `Item: ${cartItem.item.price}, Quantity: ${cartItem.quantity}`
          );
          return total + cartItem.item.price * cartItem.quantity;
        }, 0)
      );
    }
  }, [cart]);

  const router = useRouter();

  const handlePayment = () => {
    router.push("/cart/payment/success?validAccess=true");
  };
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
        <div className="flex justify-between">
          <div className="flex flex-col gap-6 max-w-3xl">
            {cart.map((cartItem: CartItem) => (
              <CartItemCard
                key={cartItem.item.itemId}
                cartItem={cartItem}
                updateCartItemQuantity={updateCartItemQuantity}
              />
            ))}
          </div>
          <Card className="text-center h-full">
            <CardHeader>
              <CardTitle className="text-xl ">Paiement</CardTitle>
            </CardHeader>
            <CardContent className="text-lg text-primary">
              Total : {totalPrice.toString()} €
            </CardContent>
            <CardFooter>
              <Button onClick={handlePayment}>Procéder au paiement</Button>
            </CardFooter>
          </Card>
        </div>
      )}
    </div>
  );
}
