using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF.Class.Adrsess;
using WPF.Class.AlcoholFamily;
using WPF.Class.Customer;
using WPF.Class.CustomerOrder;
using WPF.Class.Item;
using WPF.Class.Supplier;
using Newtonsoft.Json;
using WPF.Class.SupplierOrder;

namespace WPF
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private List<ItemResponseDTO> items;
        private List<SupplierResponseDTO> suppliers;
        private List<CustomerResponseDTO> customers;
        private List<AlcoholFamilyResponseDTO> alcoholFamily;
        private List<AddressResponseDTO> addresses;
        private List<CustomerOrderResponseDTO> customerOrders;
        private List<SupplierOrderResponseDTO> suppliersOrders;

        public MainWindow()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var itemsTask = LoadItemsAsync();
                var suppliersTask = LoadSuppliersAsync();
                var customersTask = LoadCustomersAsync();
                var alcoholTask = LoadAlcoholFamily();
                var addressesTask = LoadAddress();
                var customerOrdersTask = LoadCustomerOrders();
                var supplierOrdersTask = LoadSupplierOrders();
                await Task.WhenAll(itemsTask, suppliersTask, customersTask, alcoholTask, addressesTask, customerOrdersTask, supplierOrdersTask);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Erreur lors du chargement des données : {e.Message}");
            }
        }

        private async Task LoadItemsAsync()
        {
            try
            {
                items = await client.GetFromJsonAsync<List<ItemResponseDTO>>("http://localhost:5165/api/Item");
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Erreur lors de la récupération des données : {e.Message}");
            }
        }

        private async Task LoadSuppliersAsync()
        {
            try
            {
                suppliers = await client.GetFromJsonAsync<List<SupplierResponseDTO>>("http://localhost:5165/api/Supplier");
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Erreur lors de la récupération des fournisseurs : {e.Message}");
            }
        }

        private async Task LoadCustomersAsync()
        {
            try
            {
                customers = await client.GetFromJsonAsync<List<CustomerResponseDTO>>("http://localhost:5165/api/Customer");
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Erreur lors de la récupération des clients : {e.Message}");
            }
        }

        private async Task LoadAlcoholFamily()
        {
            try
            {
                alcoholFamily = await client.GetFromJsonAsync<List<AlcoholFamilyResponseDTO>>("http://localhost:5165/api/AlcoholFamily");
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Erreur lors de la récupération des clients : {e.Message}");
            }
        }

        private async Task LoadAddress()
        {
            try
            {
                addresses = await client.GetFromJsonAsync<List<AddressResponseDTO>>("http://localhost:5165/api/Adress");
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Erreur lors de la récupération des adresses : {e.Message}");
            }
        }

        private async Task LoadCustomerOrders()
        {
            try
            {
                customerOrders = await client.GetFromJsonAsync<List<CustomerOrderResponseDTO>>("http://localhost:5165/api/CustomerOrder");
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Erreur lors de la récupération des commandes client : {e.Message}");
            }
        }

        private async Task LoadSupplierOrders()
        {
            try
            {
                suppliersOrders = await client.GetFromJsonAsync<List<SupplierOrderResponseDTO>>("http://localhost:5165/api/SupplierOrder");
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Erreur lors de la récupération des commandes fournisseurs : {e.Message}");
            }
        }

        private void btnItems_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = items;
            SetColumnsVisibility(true, "Items");
        }

        private void btnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = suppliers;
            SetColumnsVisibility(false, "Suppliers");
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = customers;
            SetColumnsVisibility(false, "Customers");
        }

        private void btnAlcohoolFamily_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = alcoholFamily;
            SetColumnsVisibility(false, "Alcohol Family");
        }

        private void btnAddress_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = addresses;
            SetColumnsVisibility(false, "Addresses");
        }

        private void btnCustomerOrder_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = customerOrders;
            SetColumnsVisibility(false, "Customer Orders");
        }

        private void btnSupplierOrder_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = suppliersOrders;
            SetColumnsVisibility(false, "Supplier Orders");
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDataAsync();
            DataGrid.ItemsSource = items;
            SetColumnsVisibility(true, "Items");
        }

            private void SetColumnsVisibility(bool showItems, string type)
        {
            foreach (var column in DataGrid.Columns)
            {
                column.Visibility = type switch
                {
                    "Items" => column.Header.ToString() switch
                    {
                        "Nom Produit" or "Stock" or "Prix" or "Pays d'origine" or "Nom du fournisseur" or "Famille d'alcool" or "Actif" or "Actions" => Visibility.Visible,
                        _ => Visibility.Collapsed,
                    },
                    "Suppliers" => column.Header.ToString() switch
                    {
                        "Nom fournisseur" or "Description" or "Phone Number" or "Adresse" or "Ville" or "Actions" or "Actif" => Visibility.Visible,
                        _ => Visibility.Collapsed,
                    },
                    "Customers" => column.Header.ToString() switch
                    {
                        "Prénom" or "Nom" or "Email" or "Portable" or "Actions" => Visibility.Visible,
                        _ => Visibility.Collapsed,
                    },
                    "Alcohol Family"=> column.Header.ToString() switch
                    {
                        "Nom de la famille d'alcool" or "Actions" => Visibility.Visible,
                        _ => Visibility.Collapsed,
                    },
                    "Addresses" => column.Header.ToString() switch
                    {
                        "Numéro de Rue" or "Villes" or "Code postal" or "Actions" => Visibility.Visible,
                        _ => Visibility.Collapsed,
                    },
                    "Customer Orders" => column.Header.ToString() switch
                    {
                        "Id Commande" or "Statut" => Visibility.Visible,
                        _ => Visibility.Collapsed,
                    },
                    "Supplier Orders" => column.Header.ToString() switch
                    {
                        "Id Commande" or "Statut Commande" => Visibility.Visible,
                        _ => Visibility.Collapsed,
                    },
                    _ => Visibility.Collapsed,
                };
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var selectedItem = (dynamic)button.Tag; // Récupérer l'élément sélectionné
                string itemType = selectedItem.GetType().Name; // Obtenir le type de l'élément

                // Dictionnaire pour mapper les types de DTO aux types abrégés
                var typeMapping = new Dictionary<string, string>
        {
            { "ItemResponseDTO", "Item" },
            { "SupplierResponseDTO", "Supplier" },
            { "CustomerResponseDTO", "Customer" },
            {"AlcoholFamilyResponseDTO", "AlcoholFamily" },
            {"AddressResponseDTO", "Adress" }
        };

                MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet élément ?",
                    "Confirmation de Suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Déterminer l'ID selon le type d'élément
                        string id = itemType switch
                        {
                            "ItemResponseDTO" => selectedItem.ItemId.ToString(),  // Utiliser ItemId pour les Items
                            "SupplierResponseDTO" => selectedItem.SupplierId.ToString(),  // Utiliser SupplierId pour les Suppliers
                            "CustomerResponseDTO" => selectedItem.CustomerId.ToString(),  // Utiliser CustomerId pour les Customers
                            "AlcoholFamilyResponseDTO" => selectedItem.AlcoholFamilyId.ToString(), //Utiliser AlcoholFamilyId pour les AlcoholFamily
                            "AddressResponseDTO" => selectedItem.AddressId.ToString(), //Utiliser AddressId pour les Adresses
                            _ => throw new InvalidOperationException("Type d'élément non géré.")
                        };

                        // Obtenir le type abrégé à partir du mapping
                        if (!typeMapping.TryGetValue(itemType, out string apiType))
                        {
                            throw new InvalidOperationException("Type d'élément non géré.");
                        }

                        string apiUrl = $"http://localhost:5165/api/{apiType}/{id}";
                        var response = await client.DeleteAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Élément supprimé avec succès.");
                            await LoadDataAsync(); // Recharger les données après la suppression
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la suppression de l'élément.");
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        MessageBox.Show($"Erreur lors de la suppression : {ex.Message}");
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var selectedItem = (dynamic)button.Tag; // Récupérer l'élément sélectionné
                string itemType = selectedItem.GetType().Name; // Obtenir le type de l'élément

                // Utiliser une méthode pour gérer l'édition en fonction du type d'élément
                OpenEditWindowForType(selectedItem, itemType);
            }
        }

        private void OpenEditWindowForType(dynamic selectedItem, string itemType)
        {
            // Mapper les types d'éléments à leur fenêtre d'édition correspondante
            switch (itemType)
            {
                case "ItemResponseDTO":
                    // Convertir ItemResponseDTO en ItemRequestDTO pour la modification
                    

                    // Ouvrir la fenêtre d'édition pour les items
                    ItemEditWindow itemEditWindow = new ItemEditWindow(selectedItem);
                    if (itemEditWindow.ShowDialog() == true)
                    {
                        MessageBox.Show("Item modifié avec succès.");
                        LoadDataAsync(); // Recharger les données
                    }
                    break;

                case "SupplierResponseDTO":
                    // Ouvrir la fenêtre d'édition pour les fournisseurs
                    SupplierEditWindow supplierEditWindow = new SupplierEditWindow(selectedItem);
                    if (supplierEditWindow.ShowDialog() == true)
                    {
                        MessageBox.Show("Fournisseur modifié avec succès.");
                        LoadDataAsync();
                    }
                    break;

                case "CustomerResponseDTO":
                    // Ouvrir la fenêtre d'édition pour les clients
                    CustomerEditWindow customerEditWindow = new CustomerEditWindow(selectedItem);
                    if (customerEditWindow.ShowDialog() == true)
                    {
                        MessageBox.Show("Client modifié avec succès.");
                        LoadDataAsync();
                    }
                    break;

                case "AlcoholFamilyResponseDTO":
                    // Ouvrir la fenêtre d'édition pour les familles d'alcool
                    AlcoholFamilyEditWindow alcoholFamilyEditWindow = new AlcoholFamilyEditWindow(selectedItem);
                    if (alcoholFamilyEditWindow.ShowDialog() == true)
                    {
                        MessageBox.Show("Famille d'alcool modifiée avec succès.");
                        LoadDataAsync();
                    }
                    break;

                case "AddressResponseDTO":
                    // Ouvrir la fenêtre d'édition pour les Adresses
                    AddressEditWindow addressEditWindow = new AddressEditWindow(selectedItem);
                    if (addressEditWindow.ShowDialog() == true)
                    {
                        MessageBox.Show("Adresse modifié avec succès.");
                        LoadDataAsync();
                    }
                    break;

                default:
                    MessageBox.Show("Le type d'élément sélectionné n'est pas pris en charge pour l'édition.");
                    break;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string currentCategory = GetCurrentCategory();

            switch (currentCategory)
            {
                case "Items":
                    // Ouvrir la fenêtre d'ajout pour les items
                    ItemAddWindow itemAddWindow = new ItemAddWindow();
                    if (itemAddWindow.ShowDialog() == true)
                    {
                        MessageBox.Show("Item ajouté avec succès.");
                        LoadDataAsync(); // Recharger les données
                    }
                    break;

                case "Suppliers":
                    // Ouvrir la fenêtre d'ajout pour les fournisseurs
                    SupplierAddWindow supplierAddWindow = new SupplierAddWindow();
                    if (supplierAddWindow.ShowDialog() == true)
                    {
                        MessageBox.Show("Fournisseur ajouté avec succès.");
                        LoadDataAsync();
                    }
                    break;

                case "Customers":
                    // Ouvrir la fenêtre d'ajout pour les clients
                    CustomerAddWindow customerAddWindow = new CustomerAddWindow();
                    if (customerAddWindow.ShowDialog() == true)
                    {
                        MessageBox.Show("Client ajouté avec succès.");
                        LoadDataAsync();
                    }
                    break;

                case "Alcohol Family":
                    // Ouvrir la fenêtre d'ajout pour les familles d'alcool
                    AlcoholFamilyAddWindow alcoholFamilyAddWindow = new AlcoholFamilyAddWindow();
                    if (alcoholFamilyAddWindow.ShowDialog() == true)
                    {
                        MessageBox.Show("Famille d'alcool ajoutée avec succès.");
                        LoadDataAsync();
                    }
                    break;
                case "Addresses":
                    //Ouvrir la fenêtre d'ajout pour les adresses
                    AddressAddWindow addressAddWindow = new AddressAddWindow();
                    if (addressAddWindow.ShowDialog() == true)
                    {
                        MessageBox.Show("Adresse ajoutée avec succès.");
                        LoadDataAsync();
                    }
                    break;
                default:
                    MessageBox.Show("Veuillez sélectionner une catégorie valide.");
                    break;
            }
        }

        // Méthode pour déterminer la catégorie actuelle
        private string GetCurrentCategory()
        {
            if (DataGrid.ItemsSource == items)
                return "Items";
            else if (DataGrid.ItemsSource == suppliers)
                return "Suppliers";
            else if (DataGrid.ItemsSource == customers)
                return "Customers";
            else if (DataGrid.ItemsSource == alcoholFamily)
                return "Alcohol Family";
            else if (DataGrid.ItemsSource == addresses)
                return "Addresses";
            return string.Empty;
        }



        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private async void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column is DataGridTemplateColumn && e.Column.Header.ToString() == "Stock")
            { 
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    var dataGrid = sender as DataGrid;
                    var editedItem = e.Row.Item as ItemResponseDTO; // Cast de l'objet modifié en ItemResponseDTO

                    if (editedItem != null)
                    {
                        try
                        {
                            using (var client = new HttpClient())
                            {
                                // Création du multipart content
                                var multipartContent = new MultipartFormDataContent();

                                // Ajout des champs dans le multipart
                                multipartContent.Add(new StringContent(editedItem.Name), "Name");
                                multipartContent.Add(new StringContent(editedItem.Stock.ToString()), "Stock");
                                multipartContent.Add(new StringContent(editedItem.Price.ToString()), "Price");
                                multipartContent.Add(new StringContent(editedItem.OriginCountry), "OriginCountry");
                                multipartContent.Add(new StringContent(editedItem.Description), "Description");
                                multipartContent.Add(new StringContent(editedItem.IsActive.ToString()), "IsActive");
                                multipartContent.Add(new StringContent(editedItem.Supplier.SupplierId.ToString()), "SupplierId");
                                multipartContent.Add(new StringContent(editedItem.AlcoholFamily.AlcoholFamilyId.ToString()), "AlcoholFamilyId");
                                multipartContent.Add(new StringContent(editedItem.Category), "Category");
                                multipartContent.Add(new StringContent(editedItem.AlcoholVolume), "AlcoholVolume");
                                multipartContent.Add(new StringContent(editedItem.Year), "Year");
                                multipartContent.Add(new StringContent(editedItem.Capacity.ToString()), "Capacity");
                                multipartContent.Add(new StringContent(editedItem.ExpirationDate.ToString()), "ExpirationDate");


                                // Envoi de la requête PUT avec le multipart content
                                var response = await client.PutAsync($"http://localhost:5165/api/Item/{editedItem.ItemId}", multipartContent);

                                if (!response.IsSuccessStatusCode)
                                {
                                    MessageBox.Show("Erreur lors de la mise à jour du stock dans la base de données.");
                                }
                            }
                        }
                        catch (HttpRequestException ex)
                        {
                            MessageBox.Show($"Erreur lors de la mise à jour : {ex.Message}");
                        }
                    }
                }
            }
            if (e.Column is DataGridTemplateColumn && e.Column.Header.ToString() == "Status")
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    var dataGrid = sender as DataGrid;
                    var editedOrder = e.Row.Item as CustomerOrderResponseDTO; // Cast de l'objet modifié en ItemResponseDTO

                    if (editedOrder != null)
                    {
                        try
                        {
                            using (var client = new HttpClient())
                            {
                                var customerOrder = new CustomerOrderRequestDTO
                                {
                                    Status = editedOrder.Status,
                                    CustomerId = editedOrder.Customer.Id
                                };

                                var jsonItem = JsonConvert.SerializeObject(customerOrder);
                                var content = new StringContent(jsonItem, Encoding.UTF8, "application/json");


                                // Envoyer la requête PUT à l'API
                                using (HttpClient Orderclient = new HttpClient())
                                {
                                    var response = await Orderclient.PutAsync($"http://localhost:5165/api/CustomerOrder/{editedOrder.OrderID}", content);

                                    if (!response.IsSuccessStatusCode)
                                    {
                                        MessageBox.Show("Erreur lors de la mise à jour du stock dans la base de données.");
                                    }
                                   
                                }

                            }
                        }
                        catch (HttpRequestException ex)
                        {
                            MessageBox.Show($"Erreur lors de la mise à jour : {ex.Message}");
                        }
                    }
                }
            }
            if (e.Column is DataGridTemplateColumn && e.Column.Header.ToString() == "Order Status")
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    var dataGrid = sender as DataGrid;
                    var editedOrder = e.Row.Item as SupplierOrderResponseDTO; // Cast de l'objet modifié en ItemResponseDTO

                    if (editedOrder != null)
                    {
                        try
                        {
                            using (var client = new HttpClient())
                            {
                                var customerOrder = new SupplierOrderRequestDTO
                                {
                                    Status = editedOrder.Status,
                                    SupplierId = editedOrder.Supplier.SupplierId
                                };

                                var jsonItem = JsonConvert.SerializeObject(customerOrder);
                                var content = new StringContent(jsonItem, Encoding.UTF8, "application/json");


                                // Envoyer la requête PUT à l'API
                                using (HttpClient Orderclient = new HttpClient())
                                {
                                    var response = await Orderclient.PutAsync($"http://localhost:5165/api/SupplierOrder/{editedOrder.OrderID}", content);

                                    if (!response.IsSuccessStatusCode)
                                    {
                                        MessageBox.Show("Erreur lors de la mise à jour du stock dans la base de données.");
                                    }

                                }

                            }
                        }
                        catch (HttpRequestException ex)
                        {
                            MessageBox.Show($"Erreur lors de la mise à jour : {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}
