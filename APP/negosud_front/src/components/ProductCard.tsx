"use client";
import Image from "next/image";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "./ui/card";
import { Button } from "./ui/button";
import { ShoppingCart } from "lucide-react";
import { Item } from "@/services/scheme";
import Link from "next/link";
import { addToCart, getCart } from "@/services/cartService";
import { useToast } from "@/hooks/use-toast";

export default function ProductCard({ product }: { product: Item }) {
  const { toast } = useToast();
  const handleAddToCart = async (itemId: string, e: React.FormEvent) => {
    e.preventDefault();
    await addToCart(itemId, 1);
    await getCart();

    toast({
      title: `${product.name} a été ajouté à votre panier.`,
      duration: 3000,
    });
  };
  return (
    <Link href={`/products/${product.slug}`}>
      <Card className="border border-primary w-[20rem] hover:shadow-2xl items-center flex flex-col">
        <CardHeader className="text-center">
          <CardTitle>{product.name}</CardTitle>
        </CardHeader>
        <CardContent>
          <Image
            src={"/image-placeholder.png"}
            alt={"product image"}
            width={207}
            height={300}
          />
        </CardContent>
        <CardFooter className="flex flex-col items-start gap-3 w-full px-10">
          <p className="font-bold text-lg">{product.price} €</p>
          <div className="flex items-end  w-full justify-between">
            <div>
              {product.supplier && (
                <p>
                  <span className="font-bold text-primary">Produit par : </span>
                  {product.supplier.name}
                </p>
              )}
              {product.category && (
                <p>
                  <span className="font-bold text-primary">Catégorie : </span>
                  {product.category === "alcohol"
                    ? product.alcoholFamily?.name
                    : "Accessoire"}
                </p>
              )}
              <p>
                <span className="font-bold text-primary">
                  Pays d&apos;origine :{" "}
                </span>
                {product.originCountry}
              </p>
            </div>
            <Button
              variant="outline"
              className="text-primary "
              onClick={(e) => handleAddToCart(product.itemId, e)}
            >
              <ShoppingCart />
            </Button>
          </div>
        </CardFooter>
      </Card>
    </Link>
  );
}
