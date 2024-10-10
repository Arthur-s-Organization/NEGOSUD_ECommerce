import Hero from "@/components/Hero";
import ProductCard from "@/components/ProductCard";
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from "@/components/ui/carousel";
import {
  fetchBestSellingItems,
  fetchRecentItems,
} from "@/services/itemsService";

export default async function Home() {
  const bestSellings = await fetchBestSellingItems();
  const recentItems = await fetchRecentItems();
  return (
    <>
      <Hero />
      <div className="py-12 max-w-6xl mx-auto flex flex-col gap-12 px-6">
        {bestSellings && (
          <div className="flex flex-col gap-4">
            <h2 className="font-heading font-bold text-2xl">
              Les meilleures ventes :
            </h2>
            <Carousel className="w-full">
              <CarouselContent>
                {bestSellings.map((item) => (
                  <CarouselItem className="basis-1/3" key={item.itemId}>
                    <ProductCard product={item} />
                  </CarouselItem>
                ))}
              </CarouselContent>
              <CarouselPrevious />
              <CarouselNext />
            </Carousel>
          </div>
        )}
        {recentItems && (
          <div className="flex flex-col gap-4">
            <h2 className="font-heading font-bold text-2xl">Nouveautés :</h2>
            <Carousel className="w-full">
              <CarouselContent>
                {recentItems.map((item) => (
                  <CarouselItem className="basis-1/3" key={item.itemId}>
                    <ProductCard product={item} />
                  </CarouselItem>
                ))}
              </CarouselContent>
              <CarouselPrevious />
              <CarouselNext />
            </Carousel>
          </div>
        )}
      </div>
    </>
  );
}
