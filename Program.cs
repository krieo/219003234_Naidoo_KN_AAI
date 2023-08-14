using _219003234_Naidoo_KN_AAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

ImplementationOfAlgorithms algorithm = new ImplementationOfAlgorithms();

//can use doubles to cut the training data into testing data as well
//algorithm.NaiveBayesAlgorithm(0.99);
// algorithm.NaiveBayesAlgorithm();

ComplementaryNeuralNetwork complementaryNeuralNetwork = new ComplementaryNeuralNetwork();
//complementaryNeuralNetwork.TruthNeuralNetworkAlgorithm(0.9);
complementaryNeuralNetwork.FalsityNeuralNetworkAlgorithm(0.9);
bool continueBool = false;
//this loops the program in a menu like structure
do
{
    Console.WriteLine("Please select a number to start the training and testing of algorithm \n 1) Bayes Naive algorithm \n 2) Logistic Regression algorithm \n 3) Complementary neural network \n 9) Quit \n");


    //This performs the choice seperations
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("Bayes Naive algorithm selected");
            // Console.WriteLine("Would you like to change the ratio of training data to testing data? Current ratio is 90% training to 10% testing of 100% data \n Select 'y' or 'n'");
            Console.WriteLine("Current ratio is 90% training to 10% testing of 100% data");

            algorithm.NaiveBayesAlgorithm();

            break;

        case "2":
            Console.WriteLine("Logistic Regression algorithm selected");
            //  Console.WriteLine("Would you like to change the ratio of training data to testing data? Current ratio is 90% training to 10% testing of 100% data \n Select 'y' or 'n'");
            Console.WriteLine("Current ratio is 90% training to 10% testing of 100% data");
            algorithm.LogisticRegression();

            break;

        case "3":
            Console.WriteLine("Complementary neural network selected");

            break;

        case "9":
            Console.WriteLine("Exiting the program.");
            continueBool = false;
            break;

        default:
            Console.WriteLine("Invalid choice. Please select a valid option.");
            break;
    }
}
while (continueBool == true);

