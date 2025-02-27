import { Filters } from '@/components/FilterForm';
import { zItem, zItemList} from './scheme'; 
import { headers } from 'next/headers';
import axios from 'axios';
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

export const fetchItemBySlug = async (slug : string) => {
  try {
    const response = await fetch(`http://localhost:5165/api/Item/${slug}`);
    if (!response.ok) {
      throw new Error("Erreur lors de la récupération du produit");
    }
    const data = await response.json();
    const parsedData = zItem.parse(data);
    return parsedData;
  }catch(error) {
    console.error("Erreur:", error);
    return null;
  }
}

export const fetchBestSellingItems = async () => {
  try {
    const response = await fetch("http://localhost:5165/api/Item/topselling/10");
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
    const response = await fetch("http://localhost:5165/api/Item/recent/10");
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

export const fetchFilteredItems = async (filters : Filters | {name : string}) => {
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


export const fetchItemImage = async (id: string) => {
  try {
    const response = await axios.get(`http://localhost:5165/api/Item/${id}/image`, {
      responseType: 'blob'
      
    });
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération de l'image:", error);
    return null;
  }
};