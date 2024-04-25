using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SystemKolekcjonerstwa.Models;

namespace SystemKolekcjonerstwa.Views
{
    public partial class EditPage : ContentPage
    {
        private Collections collection;
        private ObservableCollection<Collections> collectionList;

        public EditPage(Collections collection)
        {
            InitializeComponent();
            this.collection = collection;

            // Ustaw kontekst wi¹zania dla strony na kolekcjê, któr¹ u¿ytkownik edytuje
            BindingContext = this.collection;

            // Za³aduj kolekcjê tylko raz przy utworzeniu strony
            LoadCollections();
        }

        private void LoadCollections()
        {
            // Pobierz kolekcjê
            var fileOperations = new FileOperations();
            collectionList = new ObservableCollection<Collections>();
            fileOperations.FileLoad(collectionList);
        }

        private void Button_Save(object sender, EventArgs e)
        {

            string name = editName.Text;
            string description = editDescription.Text;
            int.TryParse(editCount.Text, out int count);


            // Obs³uga b³êdu, jeœli nazwa kolekcji jest pusta
            if (string.IsNullOrWhiteSpace(name))
            {
                DisplayAlert("B³¹d", "Nazwa kolekcji nie mo¿e byæ pusta.", "OK");
                return;
            }

            // Zaktualizuj w³aœciwoœci kolekcji
            collection.Name = name;
            collection.Description = description;
            collection.Count = count;

            // ZnajdŸ kolekcjê do edycji w kolekcji
            var collectionToEdit = collectionList.FirstOrDefault(c => c.Name == collection.Name);

            // Obs³uga b³êdu, jeœli nie mo¿na znaleŸæ kolekcji do edycji
            if (collectionToEdit == null)
            {
                DisplayAlert("Sukces", "Pomyœlnie zaaktualizowano kolekcjê!", "OK");
                return;
            }

            // Zaktualizuj kolekcjê
            collectionToEdit.Name = collection.Name;
            collectionToEdit.Description = collection.Description;
            collectionToEdit.Count = collection.Count;

            // Zapisz zmiany w pliku
            var fileOperations = new FileOperations();
            fileOperations.SaveCollections(collectionList);

            // Opcjonalnie: Mo¿esz tutaj zaktualizowaæ widok, jeœli chcesz odœwie¿yæ go po zapisaniu zmian
        }


    }
}
