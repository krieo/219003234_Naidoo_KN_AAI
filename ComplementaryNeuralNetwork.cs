using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _219003234_Naidoo_KN_AAI
{
    public class ComplementaryNeuralNetwork
    {
        private List<double> predictArrayTrue; // To store predictions from the truth network
        private List<double> predictArrayFalse; // To store predictions from the falsity network

        // Method to run the truth neural network algorithm
        public void TruthNeuralNetworkAlgorithm(double dataCut)
        {
            FileHandler fileHandler = new FileHandler();
            List<DataRecord> allData = fileHandler.readFromFile();

            int splitIndex = (int)(allData.Count * dataCut);
            List<DataRecord> trainingData = allData.Take(splitIndex).ToList();
            List<DataRecord> testingData = allData.Skip(splitIndex).ToList();

            // Initialize the array to store truth network predictions
            predictArrayTrue = new List<double>();

            TruthNeuralNetwork neuralNetwork = new TruthNeuralNetwork(trainingData);
            Console.WriteLine("Training the Truth Neural Network...");
            neuralNetwork.Train();
            Console.WriteLine("Training complete.");

            int correctPredictions = 0;
            int totalPredictions = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Testing the Truth Neural Network...");
            foreach (var testRecord in testingData)
            {
                Console.WriteLine($"Testing record with Id: {testRecord.Id}");

                double predictedMachineFailure = neuralNetwork.Predict(testRecord);

                int actualMachineFailure = testRecord.MachineFailure;
                int predictedBinaryOutput = predictedMachineFailure > 0.5 ? 1 : 0;

                if (actualMachineFailure == predictedBinaryOutput)
                {
                    correctPredictions++;
                    Console.WriteLine("Prediction: Correct");
                }
                else
                {
                    Console.WriteLine("Prediction: Incorrect");
                }

                // Store the prediction in the array
                predictArrayTrue.Add(predictedMachineFailure);

                totalPredictions++;
            }

            stopwatch.Stop();
            TimeSpan trainingTime = stopwatch.Elapsed;
            double accuracy = (double)correctPredictions / totalPredictions * 100;
            Console.WriteLine("Testing complete.");
            Console.WriteLine($"Total Test Data: {totalPredictions}");
            Console.WriteLine($"Correct Predictions: {correctPredictions}");
            Console.WriteLine($"Incorrect Predictions: {totalPredictions - correctPredictions}");
            double roundedMinutes = Math.Round(trainingTime.TotalMinutes, 2);
            Console.WriteLine($"Training Time: {roundedMinutes} minutes");
            Console.WriteLine($"Accuracy: {accuracy:F2}%");
        }

        // Method to run the falsity neural network algorithm
        public void FalsityNeuralNetworkAlgorithm(double dataCut)
        {
            FileHandler fileHandler = new FileHandler();
            List<DataRecord> allData = fileHandler.readFromFile();

            int splitIndex = (int)(allData.Count * dataCut);
            List<DataRecord> trainingData = allData.Take(splitIndex).ToList();
            List<DataRecord> testingData = allData.Skip(splitIndex).ToList();

            // Initialize the array to store falsity network predictions
            predictArrayFalse = new List<double>();

            FalsityNeuralNetwork neuralNetwork = new FalsityNeuralNetwork(trainingData);
            Console.WriteLine("Training the Falsity Neural Network...");
            neuralNetwork.Train();
            Console.WriteLine("Training complete.");

            int correctPredictions = 0;
            int totalPredictions = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Testing the Falsity Neural Network...");
            foreach (var testRecord in testingData)
            {
                Console.WriteLine($"Testing record with Id: {testRecord.Id}");

                double predictedMachineFailure = neuralNetwork.Predict(testRecord);

                int actualMachineFailure = testRecord.MachineFailure;
                int predictedBinaryOutput = predictedMachineFailure > 0.5 ? 1 : 0;

                if (actualMachineFailure == predictedBinaryOutput)
                {
                    correctPredictions++;
                    Console.WriteLine("Prediction: Correct");
                }
                else
                {
                    Console.WriteLine("Prediction: Incorrect");
                }

                // Store the prediction in the array
                predictArrayFalse.Add(predictedMachineFailure);

                totalPredictions++;
            }

            stopwatch.Stop();
            TimeSpan trainingTime = stopwatch.Elapsed;
            double accuracy = (double)correctPredictions / totalPredictions * 100;
            Console.WriteLine("Testing complete.");
            Console.WriteLine($"Total Test Data: {totalPredictions}");
            Console.WriteLine($"Correct Predictions: {correctPredictions}");
            Console.WriteLine($"Incorrect Predictions: {totalPredictions - correctPredictions}");
            double roundedMinutes = Math.Round(trainingTime.TotalMinutes, 2);
            Console.WriteLine($"Training Time: {roundedMinutes} minutes");
            Console.WriteLine($"Accuracy: {accuracy:F2}%");
        }

        // Method to calculate the error based on the predicted arrays
        public double CalculateError()
        {
            if (predictArrayTrue == null || predictArrayFalse == null || predictArrayTrue.Count != predictArrayFalse.Count)
            {
                throw new InvalidOperationException("Predict arrays are not initialized or do not have equal length.");
            }

            double totalError = 0.0;

            for (int i = 0; i < predictArrayTrue.Count; i++)
            {
                double Ttrain = predictArrayTrue[i];
                double Ftrain = predictArrayFalse[i];
                double error = 1.0 - (Ttrain + (1.0 - Ftrain)) / 2.0;
                totalError += error;
            }

            double averageError = totalError / predictArrayTrue.Count;
            return averageError;
        }
    }
}
