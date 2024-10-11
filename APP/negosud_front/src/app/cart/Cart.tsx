import { useEffect, useState } from "react";
import { Item } from "@/services/scheme";
import CartItemCard from "@/components/CartItem";
import { updateCart } from "@/services/cartService";

export type CartItem = {
  item: Item;
  quantity: number;
};

const initialCart: CartItem[] = [
  {
    item: {
      itemId: "6c2589a8-2583-480a-8549-08dce865845c",
      name: "Vin1",
      slug: "vin1",
      stock: 10,
      description: "Un vin",
      price: 12,
      originCountry: "France",
      category: "alcohol",
      quantitySold: 0,
      supplier: {
        supplierId: "f9808411-e7f2-4234-c2df-08dce86496c7",
        name: "Maison1",
        description: "string",
        phoneNumber: "string",
      },
      alcoholFamily: {
        alcoholFamilyId: "ebed4c4c-5c2c-438a-1535-08dce8644585",
        name: "Vin",
      },
      alcoholVolume: "13",
      year: "2000",
      capacity: 33,
      expirationDate: null,
    },
    quantity: 2,
  },
  {
    item: {
      itemId: "df4718d6-cc14-41ed-854a-08dce865845c",
      name: "Vin2",
      slug: "vin2",
      stock: 12,
      description: "Un vin",
      price: 12,
      originCountry: "Espagne",
      category: "alcohol",
      quantitySold: 0,
      supplier: {
        supplierId: "f9808411-e7f2-4234-c2df-08dce86496c7",
        name: "Maison1",
        description: "string",
        phoneNumber: "string",
      },
      alcoholFamily: {
        alcoholFamilyId: "ebed4c4c-5c2c-438a-1535-08dce8644585",
        name: "Vin",
      },
      alcoholVolume: "13",
      year: "1999",
      capacity: 75,
      expirationDate: null,
    },
    quantity: 4,
  },
];

export default function Cart() {
  const [cart, setCart] = useState<CartItem[]>([]);

  const updateCartItemQuantity = async (
    itemId: string,
    newQuantity: number
  ) => {
    // Si la nouvelle quantité est 0, supprimer l'élément
    if (newQuantity <= 0) {
      setCart((prevCart) =>
        prevCart.filter((cartItem) => cartItem.item.itemId !== itemId)
      );
      await updateCart(itemId, 0); // Suppression via API
      return;
    }

    // Mettre à jour la quantité de l'élément dans le panier
    setCart((prevCart) =>
      prevCart.map((cartItem) =>
        cartItem.item.itemId === itemId
          ? { ...cartItem, quantity: newQuantity }
          : cartItem
      )
    );

    // Mettre à jour la quantité via l'API
    await updateCart(itemId, newQuantity);
  };

  useEffect(() => {
    const loadItems = async () => {
      setCart(initialCart);
    };
    loadItems();
  }, []);

  // useEffect(() => {
  //   const loadItems = async () => {
  //     try {
  //       const cartData = await getCart(); // Appel API pour récupérer le panier
  //       setCart(cartData);
  //     } catch (error) {
  //       console.error("Erreur lors du chargement du panier :", error);
  //     }
  //   };
  //   loadItems();
  // }, []);

  return (
    <div>
      {cart.length === 0 ? (
        <p>Votre panier est vide.</p>
      ) : (
        <div className="flex flex-col gap-6">
          {cart.map((cartItem) => (
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
