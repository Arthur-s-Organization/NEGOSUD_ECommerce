import { useEffect, useState } from "react";
import type { Cart, CartItem } from "@/services/scheme";
import CartItemCard from "@/components/CartItem";
import { getCart, removeFromCart, updateCart } from "@/services/cartService";
import { Button } from "./ui/button";
import Link from "next/link";
import { Card, CardHeader, CardTitle, CardContent } from "./ui/card";
import axios from "axios";
import { CardElement, useElements, useStripe } from "@stripe/react-stripe-js";
import { useRouter } from "next/navigation";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from "./ui/dialog";

export default function Cart() {
  const stripe = useStripe();
  const elements = useElements();
  const router = useRouter();
  const [cart, setCart] = useState<Cart>([]);
  const [totalPrice, setTotalPrice] = useState<number>(0);
  const [loadingPayment, setLoadingPayment] = useState<boolean>(false);
  const [paymentError, setPaymentError] = useState<string | null>(null);
  const [showDeleteConfirmation, setShowDeleteConfirmation] =
    useState<boolean>(false);
  const [pendingDeleteItemId, setPendingDeleteItemId] = useState<string | null>(
    null
  );

  const API_URL = "/api/create-payment-intent";

  const updateCartItemQuantity = async (
    itemId: string,
    newQuantity: number
  ) => {
    try {
      if (newQuantity <= 0) {
        setPendingDeleteItemId(itemId);
        setShowDeleteConfirmation(true);
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

  const handleConfirmDelete = async () => {
    if (pendingDeleteItemId) {
      setCart((prevCart) =>
        prevCart.filter(
          (cartItem) => cartItem.item.itemId !== pendingDeleteItemId
        )
      );
      await removeFromCart(pendingDeleteItemId);
      setPendingDeleteItemId(null);
    }
    setShowDeleteConfirmation(false);
  };

  const handleCancelDelete = () => {
    setPendingDeleteItemId(null);
    setShowDeleteConfirmation(false);
    window.location.reload();
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

  const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const cardElement = elements?.getElement(CardElement);

    try {
      if (!stripe || !cardElement) {
        setPaymentError("Erreur avec Stripe ou les éléments de la carte.");
        return;
      }

      setLoadingPayment(true);
      setPaymentError(null);

      const { data } = await axios.post(API_URL, {
        data: { amount: totalPrice * 100 },
      });
      const clientSecret = data.client_secret;

      const { error, paymentIntent } = await stripe.confirmCardPayment(
        clientSecret,
        {
          payment_method: { card: cardElement },
        }
      );

      if (error) {
        setPaymentError(
          "Erreur lors de la confirmation du paiement : " + error.message
        );
      } else if (paymentIntent && paymentIntent.status === "succeeded") {
        console.log("Paiement réussi !");
        router.push("/cart/payment/success?validAccess=true");
      }
    } catch (error) {
      console.error("Erreur lors de l'initiation du paiement :", error);
      setPaymentError(
        "Une erreur est survenue lors de l'initiation du paiement. Veuillez réessayer."
      );
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
          <Card className="text-center h-full w-1/4">
            <CardHeader>
              <CardTitle className="text-xl">Paiement</CardTitle>
            </CardHeader>
            <CardContent className="text-lg text-primary">
              Total : {totalPrice.toFixed(2)} €
              <form onSubmit={onSubmit}>
                <CardElement />
                <Button type="submit" disabled={loadingPayment}>
                  {loadingPayment
                    ? "Redirection vers Stripe..."
                    : "Procéder au paiement"}
                </Button>
              </form>
              {paymentError && <p className="text-red-500">{paymentError}</p>}
            </CardContent>
          </Card>
        </div>
      )}

      {showDeleteConfirmation && (
        <Dialog
          open={showDeleteConfirmation}
          onOpenChange={setShowDeleteConfirmation}
        >
          <DialogContent>
            <DialogHeader>
              <DialogTitle>Supprimer l'objet du panier</DialogTitle>
            </DialogHeader>
            <p>
              Êtes-vous sûr de vouloir supprimer cet objet de votre panier ?
            </p>
            <DialogFooter>
              <Button onClick={handleConfirmDelete}>Oui, supprimer</Button>
              <Button variant="secondary" onClick={handleCancelDelete}>
                Non, conserver
              </Button>
            </DialogFooter>
          </DialogContent>
        </Dialog>
      )}
    </div>
  );
}
