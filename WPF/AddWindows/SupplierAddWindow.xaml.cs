using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using WPF.Class.Adrsess;
using WPF.Class.Supplier;

namespace WPF
{
    public partial class SupplierAddWindow: Window
    {
        public SupplierRequestDTO Supplier { get; set; }
        private static readonly HttpClient client = new HttpClient();
        private List<AddressResponseDTO> addresses;

        public SupplierAddWindow()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                // Charger les adresses
                addresses = await client.GetFromJsonAsync<List<AddressResponseDTO>>("http://localhost:5165/api/Adress");

                // Lier les données aux ComboBox
                AddressComboBox.ItemsSource = addresses;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Erreur lors du chargement des données : {e.Message}");
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                Supplier = new SupplierRequestDTO
                {
                    Name = Name.Text,
                    Description = Description.Text,
                    PhoneNumber = PhoneNumber.Text,
                    AddressId = (Guid)AddressComboBox.SelectedValue,
                    IsActive = (bool)ActiveCheckBox.IsChecked,
                };
                // Convertir l'objet en JSON
                string jsonItem = JsonSerializer.Serialize(Supplier);
                var content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

                // Envoyer la requête PUT à l'API
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync($"http://localhost:5165/api/Supplier", content);

                    // Vérifier si la requête a réussi
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Supplier added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode}", "Update Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        DialogResult = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Fermer la fenêtre
            Close();
        }
    }
}
