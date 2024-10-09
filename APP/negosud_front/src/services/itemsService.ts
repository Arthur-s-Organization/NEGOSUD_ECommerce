import { Filters } from '@/components/FilterForm';
import { zAlcoholFamilyList, zItemList, zSupplierList } from './scheme'; 
export const fetchAllItems = async () => {
  try {
    const response = await fetch("http://localhost:5165/api/Item");
    if (!response.ok) {
      throw new Error("Erreur lors de la récupération des produits");
    }
    const data = await response.json();
    const parsedData = zItemList.parse(data);
    return parsedData;
  } catch (error) {
    console.error("Erreur:", error);
    return null;
  }
};

export const fetchBestSellingItems = async () => {
  try {
    const response = await fetch("http://localhost:5165/api/Item/topselling/4");
    if (!response.ok) {
      throw new Error("Erreur lors de la récupération des produits les plus vendus");
    }
    const data = await response.json();
    const parsedData = zItemList.parse(data);
    return parsedData;
  } catch (error) {
    console.error("Erreur:", error);
    return null;
  }
};

export const fetchRecentItems = async () => {
  try {
    const response = await fetch("http://localhost:5165/api/Item/recent/4");
    if (!response.ok) {
      throw new Error("Erreur lors de la récupération des produits récents");
    }
    const data = await response.json();
    const parsedData = zItemList.parse(data);
    return parsedData;
  } catch (error) {
    console.error("Erreur:", error);
    return null;
  }
};

export const fetchFilteredItems = async (filters : Filters) => {
  try {
    const cleanedFilters = Object.fromEntries(
      Object.entries(filters).filter(([_, value]) => value !== undefined && value !== "")
    );
    const queryParams = new URLSearchParams(cleanedFilters).toString();
    const response = await fetch(`http://localhost:5165/api/Item/filter?${queryParams}`);
    if (!response.ok) {
      throw new Error("Erreur lors de la récupération des produits avec les filtres");
    }
    const data = await response.json();
    const parsedData = zItemList.parse(data);
    return parsedData;
  }catch(error) {
    console.error("Erreur:", error);
    return null;
  }
}

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

export const fetchAlcoholFamilies = async () => {
  try {
    const response = await fetch("http://localhost:5165/api/AlcoholFamily");
    if (!response.ok) {
      throw new Error("Erreur lors de la récupération des familles");
    }
    const data = await response.json();
    const parsedData = zAlcoholFamilyList.parse(data);
    return parsedData;
  } catch (error) {
    console.error("Erreur:", error);
    return null;
  }
};