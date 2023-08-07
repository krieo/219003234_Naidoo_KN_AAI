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
        public List<DataRecord> readFromFile()
        {
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string dataFolderPath = Path.Combine(projectDirectory, "Data files");
            string csvFileName = "train.csv"; // Name of the CSV file

            string csvFilePath = Path.Combine(dataFolderPath, csvFileName);
            List<DataRecord> datalist = new List<DataRecord>();
            int counter = 0;
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<DataRecord>();

                foreach (var record in records)
                {
              /*
                    Console.WriteLine($"Id: {record.Id}");
                    Console.WriteLine($"Product ID: {record.ProductId}");
                    Console.WriteLine($"Type: {record.Type}");
                    Console.WriteLine($"Air Temperature: {record.AirTemperature}");
                    Console.WriteLine($"Process Temperature: {record.ProcessTemperature}");
                    Console.WriteLine($"Rotational Speed: {record.RotationalSpeed}");
                    Console.WriteLine($"Torque: {record.Torque}");
                    Console.WriteLine($"Tool Wear: {record.ToolWear}");
                    Console.WriteLine($"Machine Failure: {record.MachineFailure}");
                    Console.WriteLine(); // Empty line to separate records
                */
                 datalist.Add(record);
                    counter++;
                }
            }
            Console.WriteLine("finished loading data " + "Instances added:" + counter);
        return datalist;
        }

    }

  
}
