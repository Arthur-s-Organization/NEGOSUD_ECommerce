import { useEffect, useState } from "react";
import { useSession } from "next-auth/react";
import { fetchAllItems } from "@/services/itemsService";
import { Item } from "@/services/scheme";

type CartItem = {
  item: Item;
  // quantity: number;
};
export default function Cart() {
  //   const { data: session, status } = useSession();
  //   const [cartItems, setCartItems] = useState<CartItem[]>([]);
  const [cartItems, setCartItems] = useState<Item[] | null>();

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

  //   if (!session) {
  //     // Rediriger vers la page de connexion si non authentifié
  //     window.location.href = "/auth/signin";
  //     return null; // Pour ne pas afficher le reste du composant
  //   }

  useEffect(() => {
    const loadItems = async () => {
      const initialItems = await fetchAllItems();
      setCartItems(initialItems);
    };
    loadItems();
  }, [cartItems]);
  return (
    <div>
      {cartItems?.length === 0 ? (
        <p>Votre panier est vide.</p>
      ) : (
        <ul>
          {cartItems?.map((item) => (
            <li key={item.itemId}>
              {item.name} - x ${item.price.toFixed(2)}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
