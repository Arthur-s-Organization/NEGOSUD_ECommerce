"use client";
import Cart from "@/components/Cart";

const CartPage = () => {
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
