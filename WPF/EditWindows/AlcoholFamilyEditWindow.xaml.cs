using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using WPF.Class;

namespace WPF
{
    public partial class AddressEditWindow : Window
    {
        private Guid AddressId;
        public AddressRequestDTO Address { get; private set; }

        public AddressEditWindow(AddressResponseDTO address)
        {
            InitializeComponent();

            if (address != null)
            {
                // Remplir les champs avec les données existante
                StreetAddress.Text = address.StreetAddress;
                PostalCode.Text = address.PostalCode;
                City.Text = address.City;
                AddressId = address.AddressId;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Valider et sauvegarder les modifications dans un objet AlcoholFamilyRequestDTO
                Address = new AddressRequestDTO
                {
                    StreetAddress = StreetAddress.Text,
                    PostalCode = PostalCode.Text,
                    City = City.Text,
                };

                // Convertir l'objet en JSON
                string jsonItem = JsonSerializer.Serialize(Address);
                var content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

                // Envoyer la requête PUT à l'API
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PutAsync($"https://localhost:7246/api/Adress/{AddressId}", content);

                    // Vérifier si la requête a réussi
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Address updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
