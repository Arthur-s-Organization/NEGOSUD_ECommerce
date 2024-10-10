using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32; // Pour la boîte de dialogue de sélection de fichier
using WPF.Class; // Nouvelle référence de namespace
using WPF.Class;

namespace WPF
{
    public partial class ItemAddWindow : Window
    {
        public ItemRequestDTO Item { get; private set; }
        private static readonly HttpClient client = new HttpClient();
        private List<SupplierResponseDTO> suppliers;
        private List<AlcoholFamilyResponseDTO> alcoholFamilies;
        private string selectedImagePath; // Pour stocker le chemin de l'image sélectionnée


        public ItemAddWindow()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                // Charger les fournisseurs et les familles d'alcool
                suppliers = await client.GetFromJsonAsync<List<SupplierResponseDTO>>("https://localhost:7246/api/Supplier");
                alcoholFamilies = await client.GetFromJsonAsync<List<AlcoholFamilyResponseDTO>>("https://localhost:7246/api/AlcoholFamily");

                // Lier les données aux ComboBox
                SupplierComboBox.ItemsSource = suppliers;
                AlcoholFamilyComboBox.ItemsSource = alcoholFamilies;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Erreur lors du chargement des données : {e.Message}");
            }
        }
        private void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            // Boîte de dialogue pour sélectionner une image
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                SelectedImagePath.Text = selectedImagePath; // Affiche le chemin de l'image dans l'UI
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Créer un objet multipart pour inclure l'image et les données du formulaire
                var multipartContent = new MultipartFormDataContent();

                // Validation pour les valeurs numériques (Stock, Price, Capacity)
                if (!int.TryParse(Stock.Text, out int stock))
                {
                    MessageBox.Show("Stock must be a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!float.TryParse(Price.Text, out float price))
                {
                    MessageBox.Show("Price must be a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                float? capacity = null;
                if (!string.IsNullOrEmpty(Capacity.Text) && float.TryParse(Capacity.Text, out float parsedCapacity))
                {
                    capacity = parsedCapacity;
                }

                DateTime? expirationDate = null;
                if (DateTime.TryParse(ExpirationDate.Text, out DateTime parsedExpirationDate))
                {
                    expirationDate = parsedExpirationDate;
                }

                // Ajouter les données du formulaire dans multipartContent
                multipartContent.Add(new StringContent(Name.Text), "Name");
                multipartContent.Add(new StringContent(stock.ToString()), "Stock");
                multipartContent.Add(new StringContent(Description.Text), "Description");
                multipartContent.Add(new StringContent(price.ToString()), "Price");
                multipartContent.Add(new StringContent(OriginCountry.Text), "OriginCountry");
                multipartContent.Add(new StringContent(((Guid)SupplierComboBox.SelectedValue).ToString()), "SupplierId");
                multipartContent.Add(new StringContent(AlcoholVolume.Text), "AlcoholVolume");
                multipartContent.Add(new StringContent(Year.Text), "Year");

                if (capacity.HasValue)
                {
                    multipartContent.Add(new StringContent(capacity.Value.ToString()), "Capacity");
                }

                if (expirationDate.HasValue)
                {
                    multipartContent.Add(new StringContent(expirationDate.Value.ToString("yyyy-MM-dd")), "ExpirationDate");
                }

                if (AlcoholFamilyComboBox.SelectedValue != null)
                {
                    multipartContent.Add(new StringContent(((Guid)AlcoholFamilyComboBox.SelectedValue).ToString()), "AlcoholFamilyId");
                }

                multipartContent.Add(new StringContent((string)(CategoryComboBox.SelectedItem as ComboBoxItem)?.Content), "Category");

                // Ajouter l'image si elle existe
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    var fileStream = new FileStream(selectedImagePath, FileMode.Open, FileAccess.Read);
                    var imageContent = new StreamContent(fileStream);
                    imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg"); // ou png selon le type de fichier
                    multipartContent.Add(imageContent, "ImageFile", Path.GetFileName(selectedImagePath));
                }

                // Envoyer la requête avec multipart form-data
                var response = await client.PostAsync("https://localhost:7246/api/Item", multipartContent);

                // Vérifier si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Item updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error: {response.StatusCode}\nDetails: {responseContent}", "Update Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    DialogResult = false;
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
