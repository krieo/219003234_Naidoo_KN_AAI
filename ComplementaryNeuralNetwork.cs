using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _219003234_Naidoo_KN_AAI
{
    public class ComplementaryNeuralNetwork
    {
        public void TruthNeuralNetworkAlgorithm(double dataCut)
        {
            FileHandler fileHandler = new FileHandler();
            List<DataRecord> allData = fileHandler.readFromFile();

            int splitIndex = (int)(allData.Count * dataCut);
            List<DataRecord> trainingData = allData.Take(splitIndex).ToList();
            List<DataRecord> testingData = allData.Skip(splitIndex).ToList();

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

        public void FalsityNeuralNetworkAlgorithm(double dataCut)
        {
            FileHandler fileHandler = new FileHandler();
            List<DataRecord> allData = fileHandler.readFromFile();

            int splitIndex = (int)(allData.Count * dataCut);
            List<DataRecord> trainingData = allData.Take(splitIndex).ToList();
            List<DataRecord> testingData = allData.Skip(splitIndex).ToList();

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
    }
}
