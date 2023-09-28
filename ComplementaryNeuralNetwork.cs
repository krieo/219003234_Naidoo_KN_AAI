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

                // Calculate the error as suggested
                double error = 1.0 - (Ttrain + (1.0 - Ftrain)) / 2.0;
                totalError += error;
            }

            double averageError = totalError / predictArrayTrue.Count;
            Console.WriteLine(averageError.ToString() + " This is the error");
            return averageError;
        }

        // Method to aggregate predictions based on error
        private double AggregatePredictions(double predictionTrue, double predictionFalse)
        {
            // Define an aggregation rule based on error comparison
            if (Math.Abs(predictionTrue - 0.5) < Math.Abs(predictionFalse - 0.5))
            {
                return predictionTrue;
            }
            else
            {
                return predictionFalse;
            }
        }

        // Method to run the complementary neural network algorithm
        public void ComplementaryNeuralNetworkAlgorithm(double dataCut)
        {
            FileHandler fileHandler = new FileHandler();
            List<DataRecord> allData = fileHandler.readFromFile();

            int splitIndex = (int)(allData.Count * dataCut);
            List<DataRecord> trainingData = allData.Take(splitIndex).ToList();
            List<DataRecord> testingData = allData.Skip(splitIndex).ToList();

            // Initialize the arrays to store network predictions
            predictArrayTrue = new List<double>();
            predictArrayFalse = new List<double>();

            // Create instances of TruthNeuralNetwork and FalsityNeuralNetwork
            TruthNeuralNetwork truthNetwork = new TruthNeuralNetwork(trainingData);
            FalsityNeuralNetwork falsityNetwork = new FalsityNeuralNetwork(trainingData);

            Console.WriteLine("Training the Truth Neural Network...");
            truthNetwork.Train();
            Console.WriteLine("Training complete.");

            Console.WriteLine("Training the Falsity Neural Network...");
            falsityNetwork.Train();
            Console.WriteLine("Training complete.");

            int correctPredictions = 0;
            int totalPredictions = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Testing the Complementary Neural Network...");
            foreach (var testRecord in testingData)
            {
                Console.WriteLine($"Testing record with Id: {testRecord.Id}");

                // Get predictions from both networks
                double predictedMachineFailureTrue = truthNetwork.Predict(testRecord);
                double predictedMachineFailureFalse = falsityNetwork.Predict(testRecord);

                // Aggregate predictions based on error
                double aggregatedPrediction = AggregatePredictions(predictedMachineFailureTrue, predictedMachineFailureFalse);

                int actualMachineFailure = testRecord.MachineFailure;
                int predictedBinaryOutput = aggregatedPrediction > 0.5 ? 1 : 0;

                if (actualMachineFailure == predictedBinaryOutput)
                {
                    correctPredictions++;
                    Console.WriteLine("Prediction: Correct");
                }
                else
                {
                    Console.WriteLine("Prediction: Incorrect");
                }

                // Store the predictions in the arrays
                predictArrayTrue.Add(predictedMachineFailureTrue);
                predictArrayFalse.Add(predictedMachineFailureFalse);

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

            // Calculate and display the error
            double averageError = CalculateError();
            Console.WriteLine($"Average Error: {averageError}");

            // Print truth network's prediction total and accuracy
           // Console.WriteLine("Truth Network's Predictions:");
           // Console.WriteLine($"Total Predictions: {predictArrayTrue.Count}");
            double truthNetworkAccuracy = CalculateNetworkAccuracy(predictArrayTrue, testingData);
           // Console.WriteLine($"Accuracy: {truthNetworkAccuracy:F2}%");

            // Print falsity network's prediction total and accuracy
           // Console.WriteLine("Falsity Network's Predictions:");
           // Console.WriteLine($"Total Predictions: {predictArrayFalse.Count}");
            double falsityNetworkAccuracy = CalculateNetworkAccuracy(predictArrayFalse, testingData);
          //  Console.WriteLine($"Accuracy: {falsityNetworkAccuracy:F2}%");
        }

        // Method to calculate network accuracy
        private double CalculateNetworkAccuracy(List<double> predictionArray, List<DataRecord> testingData)
        {
            int correctPredictions = 0;

            for (int i = 0; i < predictionArray.Count; i++)
            {
                int actualMachineFailure = testingData[i].MachineFailure;
                int predictedBinaryOutput = predictionArray[i] > 0.5 ? 1 : 0;

                if (actualMachineFailure == predictedBinaryOutput)
                {
                    correctPredictions++;
                }
            }

            double accuracy = (double)correctPredictions / predictionArray.Count * 100;
            return accuracy;
        }

      
    }
}
