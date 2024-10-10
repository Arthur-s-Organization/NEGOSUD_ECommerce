"use client";
import { FilterForm, Filters } from "@/components/FilterForm";
import ProductCard from "@/components/ProductCard";
import { fetchAllItems, fetchFilteredItems } from "@/services/itemsService";
import { Item } from "@/services/scheme";
import { useSearchParams } from "next/navigation";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";

export default function Products() {
  const [items, setItems] = useState<Item[] | null>();
  const searchParams = useSearchParams();

  const fetchItems = async (filters: Filters | { name: string }) => {
    const filteredItems = await fetchFilteredItems(filters);
    setItems(filteredItems);
  };
  useEffect(() => {
    const loadItems = async () => {
      const initialItems = await fetchAllItems();
      setItems(initialItems);
    };

    const searchQuery = searchParams.get("search") || "";

    if (searchQuery) {
      const filters = { name: searchQuery };
      fetchItems(filters);
    } else {
      loadItems();
    }
  }, [searchParams]);

  return (
    <div className="py-12 px-6 ">
      <h1 className="text-3xl font-bold mb-10 font-heading text-primary text-center">
        Découvrez notre sélection
      </h1>
      {/* <p className="text-center max-w-3xl mx-auto mb-10">
     TODO : Add the text
      </p> */}
      {items && (
        <div className="flex gap-10">
          <div className="max-w-[16rem] w-full h-fit py-6 bg-primary px-6 rounded-sm">
            <FilterForm onFilter={fetchItems} />
          </div>
          <div className="flex flex-wrap gap-2">
            {items.map((item) => (
              <ProductCard key={item.itemId} product={item} />
            ))}
          </div>
        </div>
      )}
    </div>
  );
}
