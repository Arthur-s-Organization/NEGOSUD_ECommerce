import { zCustomer } from "./scheme";

export const fetchCustomerbyId = async (userId : string) => {
    try {
      const response = await fetch(`http://localhost:5165/api/Customer/${userId}`);
      if (!response.ok) {
        throw new Error("Erreur lors de la récupération de l'utilisateur");
      }
      const data = await response.json();
      const parsedData = zCustomer.parse(data);
      return parsedData;
    }catch(error) {
      console.error("Erreur:", error);
      return null;
    }
  }