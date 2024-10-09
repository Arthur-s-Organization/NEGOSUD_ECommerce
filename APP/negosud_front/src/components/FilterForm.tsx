import { useState } from "react";
import { Button } from "./ui/button";
import { fetchAlcoholFamilies, fetchSuppliers } from "@/services/itemsService";
import { AlcoholFamily, Supplier } from "@/services/scheme";

export type Filters = {
  Category: string;
  MaxPrice: string;
  MinPrice: string;
  SupplierId: string;
  AlcoholFamilyId: string;
};

export const FilterForm = ({
  onFilter,
}: {
  onFilter: (filters: Filters) => void;
}) => {
  const [filters, setFilters] = useState({
    Category: "",
    MaxPrice: "",
    MinPrice: "",
    SupplierId: "",
    AlcoholFamilyId: "",
  });
  const [category, setCategory] = useState<string>("");
  const [suppliers, setSuppliers] = useState<Supplier[] | null>();
  const [alcoholFamilies, setAlcoholFamilies] = useState<
    AlcoholFamily[] | null
  >();

  const loadSuppliers = async () => {
    const fetchedSuppliers = await fetchSuppliers();
    setSuppliers(fetchedSuppliers);
  };

  const loadAlcoholFamily = async () => {
    const fetchedAlcoholFamily = await fetchAlcoholFamilies();
    setAlcoholFamilies(fetchedAlcoholFamily);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFilters((prevFilters) => ({
      ...prevFilters,
      [name]: value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onFilter(filters);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <label
          htmlFor="Category"
          className="block text-sm font-medium text-white"
        >
          Catégorie
        </label>
        <select
          id="Category"
          value={category}
          onChange={(e) => setCategory(e.target.value)}
          className="mt-1 block w-full px-3 py-2 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-primary focus:border-primary sm:text-sm"
        >
          <option value="">Toutes les catégories</option>
          <option value="alcohol">Alcool</option>
          <option
            value="common"
            onClick={() => setFilters({ ...filters, AlcoholFamilyId: "" })}
          >
            Accessoires
          </option>
        </select>
      </div>

      <div>
        <label
          htmlFor="MinPrice"
          className="block text-sm font-medium text-white"
        >
          Prix minimum
        </label>
        <input
          type="number"
          id="MinPrice"
          name="MinPrice"
          value={filters.MinPrice}
          onChange={handleChange}
          className="input-field pl-2"
        />
      </div>

      <div>
        <label
          htmlFor="MaxPrice"
          className="block text-sm font-medium text-white"
        >
          Prix maximum
        </label>
        <input
          type="number"
          id="MaxPrice"
          name="MaxPrice"
          value={filters.MaxPrice}
          onChange={handleChange}
          className="input-field pl-2"
        />
      </div>

      <div>
        <label
          htmlFor="Supplier"
          className="block text-sm font-medium text-white"
        >
          Fournisseur
        </label>
        <select
          id="Supplier"
          name="Supplier"
          value={filters.SupplierId}
          onClick={loadSuppliers}
          onChange={(e) =>
            setFilters({ ...filters, SupplierId: e.target.value })
          }
          className="mt-1 block w-full px-3 py-2 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-primary focus:border-primary sm:text-sm"
        >
          <option value="">Toutes les maisons</option>
          {suppliers?.map((supplier) => (
            <option key={supplier.supplierId} value={supplier.supplierId}>
              {supplier.name}
            </option>
          ))}
        </select>
      </div>

      {category !== "common" && (
        <div>
          <label
            htmlFor="AlcoholFamily"
            className="block text-sm font-medium text-white"
          >
            Maison
          </label>
          <select
            id="AlcoholFamily"
            name="AlcoholFamily"
            value={filters.AlcoholFamilyId}
            onClick={loadAlcoholFamily}
            onChange={(e) =>
              setFilters({ ...filters, AlcoholFamilyId: e.target.value })
            }
            className="mt-1 block w-full px-3 py-2 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-primary focus:border-primary sm:text-sm"
          >
            <option value="">Toutes les familles</option>
            {alcoholFamilies?.map((alcoholFamily) => (
              <option
                key={alcoholFamily.alcoholFamilyId}
                value={alcoholFamily.alcoholFamilyId}
              >
                {alcoholFamily.name}
              </option>
            ))}
          </select>
        </div>
      )}

      <Button type="submit" variant="secondary">
        Appliquer les filtres
      </Button>
    </form>
  );
};
