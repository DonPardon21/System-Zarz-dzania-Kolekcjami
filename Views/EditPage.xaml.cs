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

            // Ustaw kontekst wi�zania dla strony na kolekcj�, kt�r� u�ytkownik edytuje
            BindingContext = this.collection;

            // Za�aduj kolekcj� tylko raz przy utworzeniu strony
            LoadCollections();
        }

        private void LoadCollections()
        {
            // Pobierz kolekcj�
            var fileOperations = new FileOperations();
            collectionList = new ObservableCollection<Collections>();
            fileOperations.FileLoad(collectionList);
        }

        private void Button_Save(object sender, EventArgs e)
        {

            string name = editName.Text;
            string description = editDescription.Text;
            int.TryParse(editCount.Text, out int count);


            // Obs�uga b��du, je�li nazwa kolekcji jest pusta
            if (string.IsNullOrWhiteSpace(name))
            {
                DisplayAlert("B��d", "Nazwa kolekcji nie mo�e by� pusta.", "OK");
                return;
            }

            // Zaktualizuj w�a�ciwo�ci kolekcji
            collection.Name = name;
            collection.Description = description;
            collection.Count = count;

            // Znajd� kolekcj� do edycji w kolekcji
            var collectionToEdit = collectionList.FirstOrDefault(c => c.Name == collection.Name);

            // Obs�uga b��du, je�li nie mo�na znale�� kolekcji do edycji
            if (collectionToEdit == null)
            {
                DisplayAlert("Sukces", "Pomy�lnie zaaktualizowano kolekcj�!", "OK");
                return;
            }

            // Zaktualizuj kolekcj�
            collectionToEdit.Name = collection.Name;
            collectionToEdit.Description = collection.Description;
            collectionToEdit.Count = collection.Count;

            // Zapisz zmiany w pliku
            var fileOperations = new FileOperations();
            fileOperations.SaveCollections(collectionList);

            // Opcjonalnie: Mo�esz tutaj zaktualizowa� widok, je�li chcesz od�wie�y� go po zapisaniu zmian
        }


    }
}
