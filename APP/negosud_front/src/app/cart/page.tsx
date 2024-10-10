// app/cart/page.tsx
"use client";

import { useEffect, useState } from "react";
import { SessionProvider, useSession } from "next-auth/react";
import Cart from "./Cart";

const CartPage = () => {
  return (
    <SessionProvider>
      <div>
        <h1>Mon Panier</h1>
        <Cart />
      </div>
    </SessionProvider>
  );
};

export default CartPage;
