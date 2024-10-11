// app/cart/page.tsx
"use client";
import { SessionProvider, useSession } from "next-auth/react";
import Cart from "./Cart";

const CartPage = () => {
  return (
    <SessionProvider>
      <div className="px-8 py-6">
        <h1 className="text-3xl font-bold mb-10 font-heading text-primary text-center">
          Votre panier
        </h1>
        <Cart />
      </div>
    </SessionProvider>
  );
};

export default CartPage;
