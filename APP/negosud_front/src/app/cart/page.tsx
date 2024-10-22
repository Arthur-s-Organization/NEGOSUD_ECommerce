"use client";
import Cart from "@/components/Cart";
import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";

const stripePromise = loadStripe(
  process.env.NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY!
);

const CartPage = () => {
  return (
    <div className="px-8 py-6">
      <h1 className="text-3xl font-bold mb-10 font-heading text-primary text-center">
        Votre panier
      </h1>
      <Elements stripe={stripePromise}>
        <Cart />
      </Elements>
    </div>
  );
};

export default CartPage;
