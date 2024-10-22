using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using WPF.Class.AlcoholFamily;

namespace WPF
{
    public partial class AlcoholFamilyAddWindow : Window
    {
        public AlcoholFamilyRequestDTO AlcohoFamily { get; private set; }

        public AlcoholFamilyAddWindow()
        {
            InitializeComponent();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Valider et sauvegarder les modifications dans un objet AlcoholFamilyRequestDTO
                AlcohoFamily = new AlcoholFamilyRequestDTO
                {
                    Name = Name.Text
                };

                // Convertir l'objet en JSON
                string jsonItem = JsonSerializer.Serialize(AlcohoFamily);
                var content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

                // Envoyer la requête PUT à l'API
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync($"http://localhost:5165/api/AlcoholFamily", content);

                    // Vérifier si la requête a réussi
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Alcohol Family updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
