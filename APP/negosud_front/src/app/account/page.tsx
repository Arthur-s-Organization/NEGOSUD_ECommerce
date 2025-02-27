// Accout page with customer details and order history, available only when connected
"use client";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Label } from "@/components/ui/label";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { formatDateTime } from "@/lib/utils";
import { logout } from "@/services/authService";
import {
  fetchCustomerbyId,
  getCustomerOrders,
} from "@/services/customerService";
import { Customer, Order, OrderList } from "@/services/scheme";
import { User, Package, Clock } from "lucide-react";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

export default function AccountPage() {
  const [customer, setCustomer] = useState<Customer>();
  const [orders, setOrders] = useState<OrderList>([]);

  useEffect(() => {
    const userId = localStorage.getItem("userId");
    if (userId) {
      const loadCustomer = async () => {
        try {
          const currentCustomer = await fetchCustomerbyId(userId);
          if (currentCustomer) {
            setCustomer(currentCustomer);
            const currentOrders = await getCustomerOrders(currentCustomer.id);
            if (currentOrders) {
              setOrders(currentOrders);
            } else {
              setOrders([]);
            }
          }
        } catch (error) {
          console.error("Erreur lors du chargement de l'utilisateur :", error);
        }
      };
      loadCustomer();
    }
  }, []);

  const router = useRouter();
  const handleLogout = () => {
    logout();
    router.push("/");
  };

  function getStatusStyle(status: string) {
    switch (status) {
      case "1":
        return "bg-blue-100 text-blue-800";
      case "2":
        return "bg-yellow-100 text-yellow-800";
      case "3":
        return "bg-green-100 text-green-800";
      default:
        return "bg-red-100 text-red-800";
    }
  }

  function getStatusLabel(status: string) {
    switch (status) {
      case "1":
        return "En traitement";
      case "2":
        return "Expédiée";
      case "3":
        return "Livrée";
      default:
        return "Une erreur est surevenue, contactez un administrateur";
    }
  }

  function getTotalPrice(order: Order) {
    let total = 0;
    order.orderDetails.forEach((orderLine) => {
      total += orderLine.item.price * orderLine.quantity;
    });
    return total;
  }

  const goToOrderDetails = (orderId: string) => {
    router.push(`/account/orders/${orderId}`);
  };

  return (
    <div className="container mx-auto p-4 space-y-8">
      <h1 className="text-3xl font-bold text-center text-primary font-heading">
        Mon Compte NEGOSUD
      </h1>

      {customer && (
        <Tabs defaultValue="profile" className="w-full">
          <TabsList className="grid w-full grid-cols-2">
            <TabsTrigger value="profile">Profil</TabsTrigger>
            <TabsTrigger value="orders">Commandes</TabsTrigger>
          </TabsList>

          <TabsContent value="profile">
            <Card>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <User className="h-6 w-6 text-primary" />
                  Informations Personnelles
                </CardTitle>
                <CardDescription>
                  Vos informations personnelles.
                </CardDescription>
              </CardHeader>
              <CardContent className="space-y-4">
                <div className="grid grid-cols-2 gap-4">
                  <div>
                    <Label>Nom</Label>
                    <p className="text-lg font-medium">
                      {customer.firstName} {customer.lastName}
                    </p>
                  </div>
                  <div>
                    <Label>Date de naissance</Label>
                    <p className="text-lg font-medium">
                      {formatDateTime(customer.dateOfBirth)}
                    </p>
                  </div>
                  <div>
                    <Label>Adresse</Label>
                    <p className="text-lg font-medium">
                      {customer.address.streetAddress},{" "}
                      {customer.address.postalCode} {customer.address.city}{" "}
                    </p>
                  </div>
                  <div>
                    <Label>Téléphone</Label>
                    <p className="text-lg font-medium">
                      {customer.phoneNumber}
                    </p>
                  </div>
                </div>
              </CardContent>
            </Card>
          </TabsContent>

          <TabsContent value="orders">
            <Card>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <Package className="h-6 w-6 text-primary" />
                  Historique des Commandes
                </CardTitle>
                <CardDescription>
                  Consultez vos commandes récentes ici.
                </CardDescription>
              </CardHeader>
              <CardContent>
                <Table>
                  <TableHeader>
                    <TableRow>
                      <TableHead>N° de Commande</TableHead>
                      <TableHead>Date</TableHead>
                      <TableHead>Total</TableHead>
                      <TableHead>Statut</TableHead>
                    </TableRow>
                  </TableHeader>
                  <TableBody>
                    {orders.map((order) => (
                      <TableRow
                        key={order.orderID}
                        onClick={() => goToOrderDetails(order.orderID)}
                      >
                        <TableCell>{order.orderID}</TableCell>
                        <TableCell>{order.orderDate}</TableCell>
                        <TableCell>{getTotalPrice(order)} €</TableCell>
                        <TableCell>
                          <span
                            className={`inline-flex items-center px-2 py-1 rounded-full text-xs font-medium ${getStatusStyle(
                              order.status
                            )}`}
                          >
                            <Clock className="w-3 h-3 mr-1" />
                            {getStatusLabel(order.status)}
                          </span>
                        </TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              </CardContent>
            </Card>
          </TabsContent>
        </Tabs>
      )}
      <div className="mt-8 flex justify-center">
        <Button onClick={() => handleLogout()}>Déconnexion</Button>
      </div>
    </div>
  );
}
