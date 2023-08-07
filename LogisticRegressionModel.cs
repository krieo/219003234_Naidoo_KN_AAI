using System;
using System.Collections.Generic;
using System.Linq;

namespace _219003234_Naidoo_KN_AAI
{
    public class LogisticRegressionModel
    {
        private double[] coefficients;

        public void Train(List<DataRecord> trainingData)
        {
            // Preprocess the training data
            double[][] features = trainingData.Select(record => GetFeatureVector(record)).ToArray();
            int[] labels = trainingData.Select(record => record.MachineFailure).ToArray();

            // Initialize coefficients
            coefficients = new double[features[0].Length + 1];

            // Train using gradient descent
            GradientDescent(features, labels, coefficients, learningRate: 0.01, numIterations: 1000);
        }

        public int Predict(DataRecord testRecord)
        {
            double[] featureVector = GetFeatureVector(testRecord);
            double prediction = PredictLogisticRegression(featureVector, coefficients);

            return prediction >= 0.5 ? 1 : 0;
        }

        private double[] GetFeatureVector(DataRecord record)
        {
            // Exclude MachineFailure from features
            return new double[]
            {
                1.0, // Intercept term
                record.AirTemperature,
                record.ProcessTemperature,
                record.RotationalSpeed,
                record.Torque
            };
        }

        private void GradientDescent(double[][] features, int[] labels, double[] coefficients, double learningRate, int numIterations)
        {
            int numFeatures = features[0].Length;

            for (int iteration = 0; iteration < numIterations; iteration++)
            {
                double[] predictions = new double[features.Length];

                // Calculate predictions
                for (int i = 0; i < features.Length; i++)
                {
                    predictions[i] = PredictLogisticRegression(features[i], coefficients);
                }

                double[] errors = new double[features.Length];

                // Calculate errors
                for (int i = 0; i < features.Length; i++)
                {
                    errors[i] = labels[i] - predictions[i];
                }

                // Update coefficients using gradients
                for (int j = 0; j < numFeatures + 1; j++)
                {
                    double gradient = 0;

                    for (int i = 0; i < features.Length; i++)
                    {
                        gradient += errors[i] * (j == 0 ? 1 : features[i][j - 1]);
                    }

                    coefficients[j] += learningRate * gradient;
                }
            }
        }

        private double PredictLogisticRegression(double[] featureVector, double[] coefficients)
        {
            // Calculate the linear combination
            double z = coefficients.Zip(featureVector, (c, x) => c * x).Sum();

            // Apply the sigmoid function
            double probability = 1.0 / (1.0 + Math.Exp(-z));
            return probability;
        }
    }
}
