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
import axios from "axios";

export default function Cart() {
  const [cart, setCart] = useState<Cart>([]);
  const [totalPrice, setTotalPrice] = useState<number>(0);
  const [loadingPayment, setLoadingPayment] = useState<boolean>(false);

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
          return total + cartItem.item.price * cartItem.quantity;
        }, 0)
      );
    }
  }, [cart]);

  const initiatePayment = async (cart: Cart) => {
    setLoadingPayment(true);
    try {
      const response = await axios.post(
        "http://localhost:5165/api/payment",
        cart,
        {
          headers: { "Content-Type": "application/json" },
        }
      );

      if (response.data && response.data.id) {
        // Redirection vers Stripe Checkout
        window.location.href = `https://checkout.stripe.com/pay/${response.data.id}`;
      } else {
        console.error("Erreur lors de la création de la session Stripe");
      }
    } catch (error) {
      console.error("Erreur lors de l'initiation du paiement :", error);
    } finally {
      setLoadingPayment(false);
    }
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
              <CardTitle className="text-xl">Paiement</CardTitle>
            </CardHeader>
            <CardContent className="text-lg text-primary">
              Total : {totalPrice.toFixed(2)} €
            </CardContent>
            <CardFooter>
              <Button
                onClick={() => initiatePayment(cart)}
                disabled={loadingPayment}
              >
                {loadingPayment
                  ? "Redirection vers Stripe..."
                  : "Procéder au paiement"}
              </Button>
            </CardFooter>
          </Card>
        </div>
      )}
    </div>
  );
}
