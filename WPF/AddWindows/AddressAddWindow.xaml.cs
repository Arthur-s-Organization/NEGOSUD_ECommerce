using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using WPF.Class.Adrsess;

namespace WPF
{
    public partial class AddressAddWindow : Window
    {
        public AddressRequestDTO Address { get; private set; }

        public AddressAddWindow()
        {
            InitializeComponent();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Valider et sauvegarder les modifications dans un objet AlcoholFamilyRequestDTO
                Address = new AddressRequestDTO
                {
                    StreetAddress = StreetAddress.Text,
                    City = City.Text,
                    PostalCode = PostalCode.Text,
                };

                // Convertir l'objet en JSON
                string jsonItem = JsonSerializer.Serialize(Address);
                var content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

                // Envoyer la requête PUT à l'API
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync($"http://localhost:5165/api/Adress", content);

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
