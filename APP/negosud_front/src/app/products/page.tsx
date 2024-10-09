"use client";
import { FilterForm, Filters } from "@/components/FilterForm";
import ProductCard from "@/components/ProductCard";
import { fetchAllItems, fetchFilteredItems } from "@/services/itemsService";
import { Item } from "@/services/scheme";
import { useEffect, useState } from "react";

export default function Products() {
  const [items, setItems] = useState<Item[] | null>();

  const fetchItems = async (filters: Filters) => {
    const filteredItems = await fetchFilteredItems(filters);
    setItems(filteredItems);
  };

  useEffect(() => {
    const loadItems = async () => {
      const initialItems = await fetchAllItems();
      setItems(initialItems);
    };
    loadItems();
  }, []);

  return (
    <div className="py-12 px-6 ">
      <h1 className="text-3xl font-bold mb-2 font-heading text-primary">
        Produits
      </h1>
      {items && (
        <div className="flex gap-10">
          <div className="w-fit min-h-full bg-primary px-6">
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
