using Microsoft.VisualBasic;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using WPF.Class;

namespace WPF
{
    public partial class CustomerAddWindow: Window
    {
        public CustomerRequestDTO Customer { get; set; }
        private List<AddressResponseDTO> addresses;
        private static readonly HttpClient client = new HttpClient();

        public CustomerAddWindow()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                // Charger les adresses
                addresses = await client.GetFromJsonAsync<List<AddressResponseDTO>>("https://localhost:7246/api/Adress");

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
                DateTime dateOfBirth;
                if (DateTime.TryParse(DateOfBirth.Text, out dateOfBirth))
                {
                    Customer = new CustomerRequestDTO
                    {
                        FirstName = FirstName.Text,
                        LastName = LastName.Text,
                        Gender = Gender.Text,
                        DateOfBirth = dateOfBirth, // Assignation correcte
                        EmailAddress = EmailAdress.Text,
                        PhoneNumber = PhoneNumber.Text
                    };
                    // Convertir l'objet en JSON
                    string jsonItem = JsonSerializer.Serialize(Customer);
                    var content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

                    // Envoyer la requête PUT à l'API
                    using (HttpClient client = new HttpClient())
                    {
                        var response = await client.PostAsync($"https://localhost:7246/api/Customer", content);

                        // Vérifier si la requête a réussi
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Customer added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            DialogResult = true;
                        }
                        else
                        {
                            MessageBox.Show($"Error: {response.StatusCode}", "Update Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                            DialogResult = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("La date de naissance n'est pas valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
