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
    public partial class CustomerEditWindow: Window
    {
        private Guid customerId;
        public CustomerRequestDTO Customer { get; set; }
        private static readonly HttpClient client = new HttpClient();
        private List<AddressResponseDTO> addresses;

        public CustomerEditWindow(CustomerResponseDTO customer)
        {
            InitializeComponent();
            LoadDataAsync();

            if (customer != null)
            {
                FirstName.Text = customer.FirstName;
                LastName.Text = customer.LastName;
                Gender.Text = customer.Gender;
                DateOfBirth.Text = customer.DateOfBirth.ToString();
                PhoneNumber.Text = customer.PhoneNumber;
                AddressComboBox.SelectedValue = customer.Address.AddressId;
            }
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
                DateTime dateOfBirth;
                if (DateTime.TryParse(DateOfBirth.Text, out dateOfBirth))
                {
                    Customer = new CustomerRequestDTO
                    {
                        FirstName = FirstName.Text,
                        LastName = LastName.Text,
                        Gender = Gender.Text,
                        DateOfBirth = dateOfBirth, // Assignation correcte
                        PhoneNumber = PhoneNumber.Text
                    };
                    // Convertir l'objet en JSON
                    string jsonItem = JsonSerializer.Serialize(Customer);
                    var content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

                    // Envoyer la requête PUT à l'API
                    using (HttpClient client = new HttpClient())
                    {
                        var response = await client.PutAsync($"http://localhost:5165/api/Customer/{customerId}", content);

                        // Vérifier si la requête a réussi
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
