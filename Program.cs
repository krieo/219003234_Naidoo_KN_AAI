using System;
using System.Collections.Generic;

namespace _219003234_Naidoo_KN_AAI
{
    class Program
    {
        static void Main(string[] args)
        {
            FileHandler fileHandler = new FileHandler();
            List<DataRecord> trainingData = fileHandler.readFromFile();

            // Initialize and train the NaiveBayesClassifier
            NaiveBayesClassifier classifier = new NaiveBayesClassifier();
            classifier.Train(trainingData);

            // Create a test record
            DataRecord testRecord = new DataRecord
            {
                Id = 123, // Assign appropriate values for each feature
                ProductId = "ABC123",
                Type = "L",
                AirTemperature = 301.5,
                ProcessTemperature = 310.8,
                RotationalSpeed = 1408,
                Torque = 43.6,
                ToolWear = 166,
                MachineFailure = 0, // This value is not used for prediction
                TWF = 0,
                HDF = 0,
                PWF = 0,
                OSF = 0,
                RNF = 0
            };

            // Predict machine failure
            int predictedMachineFailure = classifier.Predict(testRecord);

            Console.WriteLine($"Predicted Machine Failure: {predictedMachineFailure}");
        }
    }
}
