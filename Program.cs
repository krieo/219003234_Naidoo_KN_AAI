using _219003234_Naidoo_KN_AAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

ImplementationOfAlgorithms algorithm = new ImplementationOfAlgorithms();

//can use doubles to cut the training data into testing data as well
//algorithm.NaiveBayesAlgorithm(0.99);
// algorithm.NaiveBayesAlgorithm();

bool continueBool = true;
//this loops the program in a menu like structure
do
{
    Console.WriteLine("Please select a number to start the training and testing of algorithm \n 1) Bayes Naive algorithm \n 2) Logistic Regression algorithm \n 3) Complementary neural network \n 9) Quit \n");


    //This performs the choice seperations
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    string choice = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    switch (choice)
    {
        case "1":
            Console.WriteLine("Starting Bayes Naive algorithm training and testing...");
            
            break;

        case "2":
            Console.WriteLine("Starting Logistic Regression algorithm training and testing...");
                        break;

        case "3":
            Console.WriteLine("Starting Complementary neural network training and testing...");
            
            break;

        case "9":
            Console.WriteLine("Exiting the program.");
            
            break;

        default:
            Console.WriteLine("Invalid choice. Please select a valid option.");
            break;
    }
}
while (continueBool == true);

