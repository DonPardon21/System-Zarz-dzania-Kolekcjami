using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SystemKolekcjonerstwa.Models;


namespace SystemKolekcjonerstwa.Views;

public partial class MainPage : ContentPage
{
	private string newType;

	public ObservableCollection<Collections> collectionList { get; set; } = new ObservableCollection<Collections>();
    public ObservableCollection<Collections> selectedCollectionType { get; set; } = new ObservableCollection<Collections>();
    public ObservableCollection<string> typeList { get; set; } = new ObservableCollection<string>();

	private string selectedCollection {  get; set; }
    

	public FileOperations file_operations { get; set; }	= new FileOperations();


    public MainPage()
	{
      

        InitializeComponent();
		file_operations.FileLoad(collectionList);

		foreach (var item in collectionList.DistinctBy(el => el.CollectionType))
		{
			typeList.Add(item.CollectionType);
		}

		BindingContext = this;
	}

	private void Picker_SelectedIndexChanged(object sender, EventArgs e)
	{
		var picker = (Picker)sender;
		int selectedIndex = picker.SelectedIndex;

		if (selectedIndex != -1)
		{
			selectedCollectionType.Clear();
			selectedCollection = picker.Items[selectedIndex];
			foreach (var item in collectionList.Where(item => item.CollectionType == selectedCollection))
			{
				selectedCollectionType.Add(item);
			}

			gridSelectedCollectionEdit.IsVisible = true;
		}
	}

	private void Button_Delete(object sender, EventArgs e) 
	{
		Collections collection = (Collections)((Button)sender).BindingContext;
		collectionList.Remove(collection);
		selectedCollectionType.Remove(collection);

		file_operations.SaveCollections(collectionList);
	}

    private void Button_Add(object sender, EventArgs e)
    {
        string name = editName.Text.Trim();
        string description = editDescription.Text.Trim();
        int.TryParse(editCount.Text, out int count);

        // SprawdŸ, czy istnieje ju¿ kolekcja o takiej samej nazwie Name
        if (collectionList.Any(c => c.Name == name))
        {
            // Jeœli ju¿ istnieje kolekcja o takiej samej nazwie, wyœwietl alert
            DisplayAlert("Uwaga", "Kolekcja o tej samej nazwie ju¿ istnieje.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
        {
            DisplayAlert("Uwaga", "Pole nazwa i opis musz¹ byæ wype³nione", "OK");
            return;
        }
        if (count <= 0)
        {
            DisplayAlert("Uwaga", "Musisz podaæ iloœæ sztuk", "OK");
            return;
        }

        Collections collection = new Collections
        {
            Name = name,
            Count = count,
            Description = description,
            CollectionType = selectedCollection
        };

        collectionList.Add(collection);
        selectedCollectionType.Add(collection);

        file_operations.SaveCollections(collectionList);
    }

    public void Button_NewType(object sender, EventArgs e)
    {
        string newTypeName = newTypeEditor.Text.Trim();

        if (!string.IsNullOrEmpty(newTypeName))
        {
            if (!typeList.Contains(newTypeName))
            {
                typeList.Add(newTypeName);
            }
            else
            {
                DisplayAlert("Uwaga", "Ta klasa ju¿ istnieje.", "OK");
                return;
            }
        }
        else
        {
            DisplayAlert("Uwaga", "Nazwa klasy nie mo¿e byæ pusta.:", "OK");
        }

    }
    private void Button_Edit(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var collection = (Collections)button.BindingContext;

        // Przekazanie kolekcji do strony edycji
        var editPage = new EditPage(collection);
        Navigation.PushAsync(editPage);
    }


}

