import { zSupplierList } from "./scheme";

export const fetchSuppliers = async () => {
    try {
      const response = await fetch("http://localhost:5165/api/Supplier");
      if (!response.ok) {
        throw new Error("Erreur lors de la récupération des fournisseurs");
      }
      const data = await response.json();
      const parsedData = zSupplierList.parse(data);
      return parsedData;
    } catch (error) {
      console.error("Erreur:", error);
      return null;
    }
  };