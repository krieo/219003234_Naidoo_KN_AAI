using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace _219003234_Naidoo_KN_AAI
{
    /// <summary>
    /// This class will be used to handle the csv files accordingly
    /// </summary>
    /// 
    public class FileHandler
    {

        /// <summary>
        /// This method is used to read from the file
        /// </summary>
        public void readFromFile()
        {
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string dataFolderPath = Path.Combine(projectDirectory, "Data files");
            string csvFileName = "train.csv"; // Name of the CSV file

            string csvFilePath = Path.Combine(dataFolderPath, csvFileName);

            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<DataRecord>();

                foreach (var record in records)
                {
                    // Access record fields using the DataRecord properties
                    Console.WriteLine($"Id: {record.Id}, ProductId: {record.ProductId}, Type: {record.Type}, ...");
                }
            }
        }

    }

  
}
