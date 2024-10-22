import axios from "axios";
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

  export const createCustomerOrder = async (status: string, customerId: string) => {
    const response = await axios.post(`http://localhost:5165/api/CustomerOrder`, {
      status,
      customerId,
    });
    return response.data;
  };

  export const updateCustomerOrder = async (status: string, customerId: string, customerOrderId: string) => {
    const response = await axios.put(`http://localhost:5165/api/CustomerOrder/${customerOrderId}`, {
      status,
      customerId,
    });
    return response.data;
  };

  export const createCustomerOrderLine = async (customerOrderId: string ,itemId: string, itemQuantity: number ) => {
    const response = await axios.post(`http://localhost:5165/api/CustomerOrder/${customerOrderId}/Items/${itemId}/ItemQuantity/${itemQuantity}`, {
      customerOrderId,
      itemId,
      itemQuantity,
    });
    return response.data;
  };

  export const getCustomerOrderById = async (customerOrderId: string ) => {
    const response = await axios.get(`http://localhost:5165/api/CustomerOrder/${customerOrderId}`, {
    });
    return response.data;
  };

  export const getCustomerOrders = async (customerId: string) => {
    try {
      const response = await axios.get(`http://localhost:5165/api/CustomerOrder/Customer/${customerId}`);
      return response.data;
    } catch (error) {
      console.error("Erreur lors de la récupération des commandes de l'utilisateur :", error);
      return [];
    }
  };