import { zItemList } from './scheme'; 
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
    const response = await fetch("http://localhost:5165/api/Item/topselling/5");
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

export const fetchRecentItems = async () => {
  try {
    const response = await fetch("http://localhost:5165/api/Item/recent/5");
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
