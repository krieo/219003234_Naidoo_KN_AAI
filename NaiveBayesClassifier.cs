using System;
using System.Collections.Generic;
using System.Linq;

namespace _219003234_Naidoo_KN_AAI
{
    public class NaiveBayesClassifier
    {
        //this is a nested dictionary where the outer dictionary is class names (0 = no failure / 1 = failure)
        //the inner dictionary is the value names from the csv file like air temperature etc
        private Dictionary<string, Dictionary<string, List<double>>> featuresByClass;
        //this dictionary holds the values for the output classes (0 = no failure / 1 = failure)
        private Dictionary<string, int> classCounts;

        /// <summary>
        /// This is the constructor
        /// </summary>
        public NaiveBayesClassifier()
        {
            // Initialize dictionaries to store features by class and class counts
            featuresByClass = new Dictionary<string, Dictionary<string, List<double>>>();
            classCounts = new Dictionary<string, int>();
        }

        /// <summary>
        /// This Method is used to train the Naive Bayes classifier using the training data
        /// </summary>
        /// <param name="trainingData">This is a list of training data that is passed to the method</param>
        public void Train(List<DataRecord> trainingData)
        {
            foreach (var record in trainingData)
            {
                // Determine the class (1 = Failure, 0 = No Failure)
                string className = record.MachineFailure == 1 ? "1" : "0";

                // Initialize dictionaries and counts for the class if not already present
                if (!featuresByClass.ContainsKey(className))
                {
                    featuresByClass[className] = new Dictionary<string, List<double>>();
                    classCounts[className] = 0;
                }

                // Loop through properties of the DataRecord class
                foreach (var property in typeof(DataRecord).GetProperties())
                {
                    // Check if property type is double or int
                    if (property.PropertyType == typeof(double) || property.PropertyType == typeof(int))
                    {
                        string propertyName = property.Name;
                        double value = Convert.ToDouble(property.GetValue(record));

                        // Initialize feature list for the property if not already present
                        if (!featuresByClass[className].ContainsKey(propertyName))
                        {
                            featuresByClass[className][propertyName] = new List<double>();
                        }

                        // Add the property value to the feature list
                        featuresByClass[className][propertyName].Add(value);
                    }
                }

                // Increment the class count
                classCounts[className]++;
            }

        }

        // Method to predict class for a test record
        public int Predict(DataRecord testRecord)
        {
            // Calculate probabilities for failure and no failure classes
            double probFailure = CalculateClassProbability("1", testRecord);
            double probNoFailure = CalculateClassProbability("0", testRecord);

            // Choose the class with higher probability as the prediction
            if (probFailure > probNoFailure)
            {
                return 1; // Predicted Failure
            }
            else
            {
                return 0; // Predicted No Failure
            }
        }

        // Method to calculate the probability of a class for a test record
        private double CalculateClassProbability(string className, DataRecord testRecord)
        {
            // Calculate class probability and feature probability
            double classProbability = (double)classCounts[className] / classCounts.Values.Sum();
            double featureProbability = 1.0;

            // Loop through properties of the DataRecord class
            foreach (var property in typeof(DataRecord).GetProperties())
            {
                // Check if property type is double or int
                if (property.PropertyType == typeof(double) || property.PropertyType == typeof(int))
                {
                    string propertyName = property.Name;
                    double value = Convert.ToDouble(property.GetValue(testRecord));

                    // Calculate feature probability using Gaussian distribution
                    if (featuresByClass[className].ContainsKey(propertyName))
                    {
                        double mean = featuresByClass[className][propertyName].Average();
                        double stdDev = Math.Sqrt(featuresByClass[className][propertyName].Select(x => Math.Pow(x - mean, 2)).Sum() / featuresByClass[className][propertyName].Count);

                        double probability = (1.0 / (stdDev * Math.Sqrt(2 * Math.PI))) * Math.Exp(-Math.Pow(value - mean, 2) / (2 * Math.Pow(stdDev, 2)));
                        featureProbability *= probability;
                    }
                }
            }

            return classProbability * featureProbability;
        }
    }
}
