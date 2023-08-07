using System;
using System.Collections.Generic;
using System.Linq;

namespace _219003234_Naidoo_KN_AAI
{
    public class NaiveBayesClassifier
    {
        private Dictionary<string, Dictionary<string, List<double>>> featuresByClass;
        private Dictionary<string, int> classCounts;

        public NaiveBayesClassifier()
        {
            featuresByClass = new Dictionary<string, Dictionary<string, List<double>>>();
            classCounts = new Dictionary<string, int>();
        }

        public void Train(List<DataRecord> trainingData)
        {
            foreach (var record in trainingData)
            {
                string className = record.MachineFailure == 1 ? "1" : "0"; // 1 = Failure, 0 = No Failure

                if (!featuresByClass.ContainsKey(className))
                {
                    featuresByClass[className] = new Dictionary<string, List<double>>();
                    classCounts[className] = 0;
                }

                foreach (var property in typeof(DataRecord).GetProperties())
                {
                    if (property.PropertyType == typeof(double) || property.PropertyType == typeof(int))
                    {
                        string propertyName = property.Name;
                        double value = Convert.ToDouble(property.GetValue(record));

                        if (!featuresByClass[className].ContainsKey(propertyName))
                        {
                            featuresByClass[className][propertyName] = new List<double>();
                        }

                        featuresByClass[className][propertyName].Add(value);
                    }
                }

                classCounts[className]++;
            }
        }

        public int Predict(DataRecord testRecord)
        {
            double probFailure = CalculateClassProbability("1", testRecord);
            double probNoFailure = CalculateClassProbability("0", testRecord);

            if (probFailure > probNoFailure)
            {
                return 1; // Predicted Failure
            }
            else
            {
                return 0; // Predicted No Failure
            }
        }

        private double CalculateClassProbability(string className, DataRecord testRecord)
        {
            double classProbability = (double)classCounts[className] / classCounts.Values.Sum();
            double featureProbability = 1.0;

            foreach (var property in typeof(DataRecord).GetProperties())
            {
                if (property.PropertyType == typeof(double) || property.PropertyType == typeof(int))
                {
                    string propertyName = property.Name;
                    double value = Convert.ToDouble(property.GetValue(testRecord));

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
