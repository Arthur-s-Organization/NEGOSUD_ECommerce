import Hero from "@/components/Hero";
import ProductCard from "@/components/ProductCard";
const product = {
  name: "Nom du produit",
  price: "17,99",
  image: "/image.png",
  supplier: {
    name: "nom de la maison",
  },
  alcoholFamily: {
    name: "rouge",
  },
  originCountry: "France",
};
export default function Home() {
  return (
    <>
      <Hero />
      <div className="py-12 max-w-6xl mx-auto flex flex-col gap-12">
        <div className="flex flex-col gap-4">
          <h2 className="font-heading font-bold text-2xl">
            Les meilleures ventes :
          </h2>
          <div className="flex gap-x-2 items-center">
            <ProductCard product={product} />
            <ProductCard product={product} />
            <ProductCard product={product} />
            <ProductCard product={product} />
          </div>
        </div>
        <div className="flex flex-col gap-4">
          <h2 className="font-heading font-bold text-2xl">Nouveautés :</h2>
          <div className="flex gap-x-2 items-center">
            <ProductCard product={product} />
            <ProductCard product={product} />
            <ProductCard product={product} />
            <ProductCard product={product} />
          </div>
        </div>
      </div>
    </>
  );
}
