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
    public partial class AlcoholFamilyEditWindow : Window
    {
        private Guid AlcoholFamilyId;
        public AlcoholFamilyRequestDTO AlcohoFamily { get; private set; }

        public AlcoholFamilyEditWindow(AlcoholFamilyResponseDTO alcohoFamily)
        {
            InitializeComponent();

            if (alcohoFamily != null)
            {
                // Remplir les champs avec les données existante
                Name.Text = alcohoFamily.Name;
                AlcoholFamilyId = alcohoFamily.AlcoholFamilyId;
            }
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
                    var response = await client.PutAsync($"https://localhost:7246/api/AlcoholFamily/{AlcoholFamilyId}", content);

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
