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

export default function ProductCard({ product }: { product: Item }) {
  return (
    <Link href="/">
      <Card className="border border-primary w-fit hover:shadow-2xl">
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
        <CardFooter className="flex flex-col items-start gap-3">
          <p className="font-bold text-lg">{product.price} €</p>
          <div className="flex items-end">
            <div>
              {product.supplier && (
                <p>
                  <span className="font-bold text-primary">Produit par : </span>
                  {product.supplier.name}
                </p>
              )}
              {product.alcoholFamily && (
                <p>
                  <span className="font-bold text-primary">Catégorie : </span>
                  {product.alcoholFamily.name}
                </p>
              )}
              <p>
                <span className="font-bold text-primary">
                  Pays d&apos;origine :{" "}
                </span>
                {product.originCountry}
              </p>
            </div>
            <Button variant="outline" className="text-primary ">
              <ShoppingCart />
            </Button>
          </div>
        </CardFooter>
      </Card>
    </Link>
  );
}
