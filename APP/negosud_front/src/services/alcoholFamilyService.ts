import { zAlcoholFamily, zAlcoholFamilyList } from "./scheme";

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

  export const getAlcoholFamilyById = async (alcoholFamilyId :string) => {
    try {
      const response = await fetch(`http://localhost:5165/api/AlcoholFamily/${alcoholFamilyId}`);
      if (!response.ok) {
        throw new Error("Erreur lors de la récupération des familles");
      }
      const data = await response.json();
      const parsedData = zAlcoholFamily.parse(data);
      return parsedData;
    } catch (error) {
      console.error("Erreur:", error);
      return null;
    }
  }