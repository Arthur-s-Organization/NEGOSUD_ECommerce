import Hero from "@/components/Hero";
import ProductCard from "@/components/ProductCard";
import {
  fetchAllItems,
  fetchBestSellingItems,
  fetchRecentItems,
} from "@/services/itemsService";

export default async function Home() {
  const bestSelling = (await fetchBestSellingItems()) ?? [];
  const recentItem = (await fetchRecentItems()) ?? [];
  return (
    <>
      <Hero />
      <div className="py-12 max-w-6xl mx-auto flex flex-col gap-12">
        <div className="flex flex-col gap-4">
          <h2 className="font-heading font-bold text-2xl">
            Les meilleures ventes :
          </h2>
          <div className="flex gap-x-2 items-center">
            {bestSelling.map((item) => (
              <ProductCard key={item.itemId} product={item} />
            ))}
          </div>
        </div>
        <div className="flex flex-col gap-4">
          <h2 className="font-heading font-bold text-2xl">Nouveautés :</h2>
          <div className="flex gap-x-2 items-center">
            {recentItem.map((item) => (
              <ProductCard key={item.itemId} product={item} />
            ))}
          </div>
        </div>
      </div>
    </>
  );
}
