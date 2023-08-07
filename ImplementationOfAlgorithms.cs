using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _219003234_Naidoo_KN_AAI
{
    public class ImplementationOfAlgorithms
    {
        /// <summary>
        /// This method performs the calling of the naive bayes algorithms
        /// </summary>
        public void NaiveBayesAlgorithm()
        {
            FileHandler fileHandler = new FileHandler();
            List<DataRecord> allData = fileHandler.readFromFile();

            // Determine the split point (90% for training, 10% for testing)
            int splitIndex = (int)(allData.Count * 0.9);
            List<DataRecord> trainingData = allData.Take(splitIndex).ToList();
            List<DataRecord> testingData = allData.Skip(splitIndex).ToList();

            // Initialize and train the NaiveBayesClassifier
            NaiveBayesClassifier classifier = new NaiveBayesClassifier();
            classifier.Train(trainingData);
            Console.WriteLine("Training the Naive Bayes classifier...");
            Console.WriteLine("Training complete.");

            int correctPredictions = 0;
            int totalPredictions = 0;

            Console.WriteLine("Testing the classifier...");
            foreach (var testRecord in testingData)
            {
                Console.WriteLine($"Testing record with Id: {testRecord.Id}");

                int actualMachineFailure = testRecord.MachineFailure;
                int predictedMachineFailure = classifier.Predict(testRecord);

                if (actualMachineFailure == predictedMachineFailure)
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

            double accuracy = (double)correctPredictions / totalPredictions * 100;
            Console.WriteLine("Testing complete.");
            Console.WriteLine($"Total Test Data: {totalPredictions}");
            Console.WriteLine($"Correct Predictions: {correctPredictions}");
            Console.WriteLine($"Incorrect Predictions: {totalPredictions - correctPredictions}");
            Console.WriteLine($"Accuracy: {accuracy:F2}%");
        }
    }
}
