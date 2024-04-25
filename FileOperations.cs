using SystemKolekcjonerstwa.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;


    public class FileOperations
    {
   static public string filePath { get; set; } = Path.Combine(FileSystem.Current.AppDataDirectory, "Kolekcje.txt");

    public FileOperations() { }

    public void FileLoad(ObservableCollection<Collections> collectionList)
    {
        Debug.WriteLine("Ścieżka do pliku");
        Debug.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------");
        Debug.WriteLine(filePath);
        Debug.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------");
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
            return;
        }

        StreamReader streamReader = new StreamReader(filePath);
        string line;

        while ((line = streamReader.ReadLine()) != null)
        {
            string[] lineSplited = line.Split("/");
            if (lineSplited.Length >= 4) //Sprawdź czy tablica ma wystarczającą liczbę elementów
            {
                string name = lineSplited[0];
                string collectionType = lineSplited[1];
                string description = lineSplited[2];
                int count = 0;

                if (Int32.TryParse(lineSplited[3], out int c)) { count = c; }

                collectionList.Add(new Collections
                {
                    Name = name,
                    CollectionType = collectionType,
                    Description = description,
                    Count = count
                });
            }
        }
        streamReader.Close();
    }


    public void SaveCollections(ObservableCollection<Collections> collectionList)
        {
            List<string> list = new List<string>();
            foreach (Collections collection in collectionList) 
            {
            string expression = $"{collection.Name}/{collection.CollectionType}/{collection.Description}/{collection.Count}";
            list.Add(expression);
            }
            File.WriteAllLines(filePath, list);
        }

}

