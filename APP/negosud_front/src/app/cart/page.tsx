// app/cart/page.tsx
"use client";
import { useRouter } from "next/navigation";
import Cart from "./Cart";
import { useEffect } from "react";

const CartPage = () => {
  const router = useRouter();
  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      // Redirection si l'utilisateur n'est pas connecté
      router.push("/auth/signin");
    }
  }, []);
  return (
    <div className="px-8 py-6">
      <h1 className="text-3xl font-bold mb-10 font-heading text-primary text-center">
        Votre panier
      </h1>
      <Cart />
    </div>
  );
};

export default CartPage;
