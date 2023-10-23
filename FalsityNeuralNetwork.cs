using System;
using System.Collections.Generic;

namespace _219003234_Naidoo_KN_AAI
{
    public class FalsityNeuralNetwork
    {
        private List<DataRecord> trainingData; // Training data containing input values and machine failure labels
        private int inputCount; // Number of input features
        private int hiddenCount; // Number of neurons in the hidden layer
        private double learningRate; // Learning rate for updating weights

        private List<double> inputWeights; // Weights for the input layer
        private List<double> hiddenWeights; // Weights for the hidden layer
        private double outputWeight; // Weight for the output neuron

        public FalsityNeuralNetwork(List<DataRecord> trainingData, int hiddenCount = 5, double learningRate = 0.01)
        {
            this.trainingData = trainingData;
            this.inputCount = GetInputValues(trainingData[0]).Count; // Include MachineFailure as input
            this.hiddenCount = hiddenCount;
            this.learningRate = learningRate;

            InitializeWeights(); // Initialize weights for the neural network
        }

        private void InitializeWeights()
        {
            Random random = new Random();

            inputWeights = new List<double>();
            for (int i = 0; i < inputCount; i++)
            {
                inputWeights.Add(random.NextDouble()); // Initialize input weights with random values
            }

            hiddenWeights = new List<double>();
            for (int i = 0; i < hiddenCount; i++)
            {
                hiddenWeights.Add(random.NextDouble()); // Initialize hidden layer weights with random values
            }

            outputWeight = random.NextDouble(); // Initialize output neuron weight with random value
        }

        private double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x)); // Sigmoid activation function
        }

        private double CalculateHiddenNeuronOutput(List<double> inputValues, List<double> weights)
        {
            double sum = 0;
            for (int i = 0; i < inputValues.Count; i++)
            {
                sum += inputValues[i] * weights[i]; // Weighted sum of inputs
            }
            return Sigmoid(sum); // Apply sigmoid activation
        }

        public void Train()
        {
            foreach (var record in trainingData)
            {
                List<double> inputValues = GetInputValues(record);
                double expectedOutput = 1 - record.MachineFailure; // Use the complement of MachineFailure as the target

                // Forward propagation
                List<double> hiddenLayerOutputs = new List<double>();
                for (int i = 0; i < hiddenCount; i++)
                {
                    hiddenLayerOutputs.Add(CalculateHiddenNeuronOutput(inputValues, inputWeights));
                }

                double outputNeuronInput = CalculateHiddenNeuronOutput(hiddenLayerOutputs, hiddenWeights);
                double output = Sigmoid(outputNeuronInput);

                // Backpropagation
                double outputError = expectedOutput - output;
                double outputDelta = outputError * output * (1 - output);

                for (int i = 0; i < hiddenCount; i++)
                {
                    double hiddenError = outputDelta * hiddenWeights[i];
                    double hiddenDelta = hiddenError * hiddenLayerOutputs[i] * (1 - hiddenLayerOutputs[i]);

                    hiddenWeights[i] += learningRate * outputDelta * hiddenLayerOutputs[i];
                    for (int j = 0; j < inputCount; j++)
                    {
                        inputWeights[j] += learningRate * hiddenDelta * inputValues[j];
                    }
                }
            }
        }

        private List<double> GetInputValues(DataRecord record)
        {
            return new List<double>
            {
                record.AirTemperature,
                record.ProcessTemperature,
                record.RotationalSpeed,
                record.Torque,
                record.ToolWear,
                record.MachineFailure // Include the MachineFailure value as an input
                // Include other input features here
            };
        }

        public double Predict(DataRecord testRecord)
        {
            List<double> inputValues = GetInputValues(testRecord);
            double hiddenLayerOutput = CalculateHiddenNeuronOutput(inputValues, inputWeights);
            double outputNeuronInput = CalculateHiddenNeuronOutput(new List<double> { hiddenLayerOutput }, hiddenWeights);
            double output = Sigmoid(outputNeuronInput);
            return output; // Return the predicted output
        }
    }
}
