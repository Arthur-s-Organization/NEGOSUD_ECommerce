import { useEffect, useState } from "react";
import { useSession } from "next-auth/react";
import { Item } from "@/services/scheme";
import CartItemCard from "@/components/CartItem";

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

export type CartItem = {
  item: Item;
  quantity: number;
};
export default function Cart() {
  //   const { data: session, status } = useSession();
  const [cartItems, setCartItems] = useState<CartItem[]>([]);
  const updateCartItemQuantity = async (
    itemId: string,
    newQuantity: number
  ) => {
    setCartItems((prevCart) =>
      prevCart.map((cartItem) =>
        cartItem.item.itemId === itemId
          ? { ...cartItem, quantity: newQuantity }
          : cartItem
      )
    );
    // try {
    //   const response = await fetch(`/api/cart/${itemId}`, {
    //     method: "PUT",
    //     headers: {
    //       "Content-Type": "application/json",
    //     },
    //     body: JSON.stringify({ quantity: newQuantity }),
    //   });

    //   if (!response.ok) {
    //     console.error("Erreur lors de la mise à jour de la quantité");
    //   }
    // } catch (error) {
    //   console.error("Erreur lors de la mise à jour du panier : ", error);
    // }
  };

  //   const [loading, setLoading] = useState(true);

  //   useEffect(() => {
  //     const fetchCartItems = async () => {
  //       if (session) {
  //         const res = await fetch("https://your-api.net/api/cart", {
  //           method: "GET",
  //           headers: {
  //             Authorization: `Bearer ${session.accessToken}`, // Utilise le token JWT pour authentifier
  //           },
  //         });

  //         if (res.ok) {
  //           const items = await res.json();
  //           setCartItems(items);
  //         } else {
  //           console.error("Échec de la récupération des articles du panier");
  //         }
  //       }
  //       setLoading(false);
  //     };

  //     fetchCartItems();
  //   }, [session]);
  const session = null;
  if (!session) {
    // Rediriger vers la page de connexion si non authentifié
    window.location.href = "/auth/signin";
    return null; // Pour ne pas afficher le reste du composant
  }

  useEffect(() => {
    const loadItems = async () => {
      setCartItems(initialCart);
    };
    loadItems();
  }, []);
  return (
    <div>
      {cartItems?.length === 0 ? (
        <p>Votre panier est vide.</p>
      ) : (
        <div className="flex flex-col gap-6">
          {cartItems?.map((cartItem) => (
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
