using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF.Class;

namespace WPF
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private List<ItemResponseDTO> items;
        private List<SupplierResponseDTO> suppliers;
        private List<CustomerResponseDTO> customers;
        private List<AlcoholFamilyResponseDTO> alcoholFamily;

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
                await Task.WhenAll(itemsTask, suppliersTask, customersTask, alcoholTask);
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
                items = await client.GetFromJsonAsync<List<ItemResponseDTO>>("https://localhost:7246/api/Item");
                DataGrid.ItemsSource = items; // Afficher les items par défaut
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
                suppliers = await client.GetFromJsonAsync<List<SupplierResponseDTO>>("https://localhost:7246/api/Supplier");
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
                customers = await client.GetFromJsonAsync<List<CustomerResponseDTO>>("https://localhost:7246/api/Customer");
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
                alcoholFamily = await client.GetFromJsonAsync<List<AlcoholFamilyResponseDTO>>("https://localhost:7246/api/AlcoholFamily");
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Erreur lors de la récupération des clients : {e.Message}");
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

            private void SetColumnsVisibility(bool showItems, string type)
        {
            foreach (var column in DataGrid.Columns)
            {
                column.Visibility = type switch
                {
                    "Items" => column.Header.ToString() switch
                    {
                        "Item Name" or "Stock" or "Price" or "Origin Country" or "Supplier Item Name" or "Alcohol Family" or "Actions" => Visibility.Visible,
                        _ => Visibility.Collapsed,
                    },
                    "Suppliers" => column.Header.ToString() switch
                    {
                        "Supplier Name" or "Description" or "Phone Number" or "Address" or "City" or "Actions" => Visibility.Visible,
                        _ => Visibility.Collapsed,
                    },
                    "Customers" => column.Header.ToString() switch
                    {
                        "First Name" or "Last Name" or "Email Address" or "Phone Number" or "Actions" => Visibility.Visible,
                        _ => Visibility.Collapsed,
                    },
                    "Alcohol Family"=> column.Header.ToString() switch
                    {
                        "Alcohol Family Name" or "Actions" => Visibility.Visible,
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
            {"AlcoholFamilyResponseDTO", "AlcoholFamily" }
        };

                MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet élément ?",
                    "Confirmation de Suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        MessageBox.Show(itemType);
                        // Déterminer l'ID selon le type d'élément
                        string id = itemType switch
                        {
                            "ItemResponseDTO" => selectedItem.ItemId.ToString(),  // Utiliser ItemId pour les Items
                            "SupplierResponseDTO" => selectedItem.SupplierId.ToString(),  // Utiliser SupplierId pour les Suppliers
                            "CustomerResponseDTO" => selectedItem.CustomerId.ToString(),  // Utiliser CustomerId pour les Customers
                            "AlcoholFamilyResponseDTO" => selectedItem.AlcoholFamilyId.ToString(), //Utiliser AlcoholFamilyId pour les AlcoholFamily
                            _ => throw new InvalidOperationException("Type d'élément non géré.")
                        };

                        // Obtenir le type abrégé à partir du mapping
                        if (!typeMapping.TryGetValue(itemType, out string apiType))
                        {
                            throw new InvalidOperationException("Type d'élément non géré.");
                        }

                        string apiUrl = $"https://localhost:7246/api/{apiType}/{id}";
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


    }
}
