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
import { loadStripe } from '@stripe/stripe-js';
import axios from "axios";

const stripePromise = loadStripe(process.env.NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY!);

export default function Cart() {
  const [cart, setCart] = useState<Cart>([]);
  const [totalPrice, setTotalPrice] = useState<number>(0);
  const [loadingPayment, setLoadingPayment] = useState<boolean>(false);
  const [paymentError, setPaymentError] = useState<string | null>(null);

  const API_URL = "http://localhost:5165/api/Payment";

  const updateCartItemQuantity = async (itemId: string, newQuantity: number) => {
    try {
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
    } catch (error) {
      console.error("Erreur lors de la mise à jour de la quantité :", error);
    }
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
      const calculatedTotal = cart.reduce((total, cartItem) => {
        return total + cartItem.item.price * cartItem.quantity;
      }, 0);
      setTotalPrice(calculatedTotal);
    }
  }, [cart]);

  const initiatePayment = async (cart: Cart) => {
    setLoadingPayment(true);
    setPaymentError(null); // Réinitialisation des erreurs

    try {
      const response = await axios.post(API_URL, cart);

      if (response.data && response.data.id) {
        const stripe = await stripePromise;
        const { error } = await stripe!.redirectToCheckout({ sessionId: response.data.id });

        if (error) {
          setPaymentError("Erreur lors de la redirection vers Stripe Checkout : " + error.message);
        }
      } else {
        throw new Error("Erreur lors de la création de la session Stripe");
      }
    } catch (error) {
      console.error("Erreur lors de l'initiation du paiement :", error);
      setPaymentError("Une erreur est survenue lors de l'initiation du paiement. Veuillez réessayer.");
    } finally {
      setLoadingPayment(false);
    }
  };

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
              <CardTitle className="text-xl">Paiement</CardTitle>
            </CardHeader>
            <CardContent className="text-lg text-primary">
              Total : {totalPrice.toFixed(2)} €
            </CardContent>
            <CardFooter className="flex flex-col gap-2">
              <Button onClick={initiatePayment} disabled={loadingPayment}>
                {loadingPayment ? "Redirection vers Stripe..." : "Procéder au paiement"}
              </Button>
              {paymentError && <p className="text-red-500">{paymentError}</p>}
            </CardFooter>
          </Card>
        </div>
      )}
    </div>
  );
}
