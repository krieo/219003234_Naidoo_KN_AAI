using System;
using System.Collections.Generic;
using System.Linq;

namespace _219003234_Naidoo_KN_AAI
{
    /// <summary>
    /// This class handles the behaviours associated with the logistic regression model
    /// </summary>
    public class LogisticRegressionModel
    {   
        //this stores the models weights that is learned during the training process
        private double[] coefficients;

        /// <summary>
        /// This method takes a list of data records as input and preprocessess it by converting it
        /// into a feature vector aka input variables and labels aka output variables. It then 
        /// initializes the coefficients array and trains the model using the gradient descent
        /// </summary>
        /// <param name="trainingData"></param>
        public void Train(List<DataRecord> trainingData)
        {
            // Preprocess the training data
            double[][] features = trainingData.Select(record => GetFeatureVector(record)).ToArray();
            int[] labels = trainingData.Select(record => record.MachineFailure).ToArray();

            // Initialize coefficients
            coefficients = new double[features[0].Length + 1];

            // Train using gradient descent
            GradientDescent(features, labels, coefficients, learningRate: 0.01, numIterations: 1000);
            //GradientDescent(features, labels, coefficients, learningRate: 0.15, numIterations: 2000);
        }

        /// <summary>
        /// This method takes in a single test data record and calculates the predicted output using the regression model
        /// with the learned coefficients it converts the prediction into a binary prediction on the 0.5 threshold
        /// </summary>
        /// <param name="testRecord"></param>
        /// <returns></returns>
        public int Predict(DataRecord testRecord)
        {
            double[] featureVector = GetFeatureVector(testRecord);
            double prediction = PredictLogisticRegression(featureVector, coefficients);

            return prediction >= 0.5 ? 1 : 0;
        }

        /// <summary>
        /// This method takes a data record and constructs a feature vector that 
        /// includes an intercept term 1.0 and selected features 
        /// such as AirTemperature, ProcessTemperature, RotationalSpeed and Torque. 
        /// MachineFailure is excluded from the features as it is the target variable.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This method implements the workings of the gradient descent algorithm to update the models coefficients
        /// It iteratively adjusts the coefficients to minimize the prediction errors by considering the gradients of
        /// the loss function with respect to each coefficient.
        /// </summary>
        /// <param name="features"></param>
        /// <param name="labels"></param>
        /// <param name="coefficients"></param>
        /// <param name="learningRate"></param>
        /// <param name="numIterations"></param>
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

        /// <summary>
        /// This method claculates the linear combination of feature values and coefficients 
        /// it then applies the sigmoid function to map the linear combination to a value between 1 and 0
        /// </summary>
        /// <param name="featureVector"></param>
        /// <param name="coefficients"></param>
        /// <returns></returns>
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
