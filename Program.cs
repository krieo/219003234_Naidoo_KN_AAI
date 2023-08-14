﻿using _219003234_Naidoo_KN_AAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("Bayes Naive algorithm selected");
            Console.WriteLine("Would you like to change the ratio of training data to testing data? Current ratio is 90% training to 10% testing of 100% data \n Select 'y' or 'n'");
            string choicetraining = Console.ReadLine();
            if (choicetraining == "y")
            {
                Console.WriteLine("Please enter numerical value for ratio from 0.00 to 1.00 for example 0.9 for 90% \n");
                string ratio = Console.ReadLine();
                 
                
                    algorithm.NaiveBayesAlgorithm(double.Parse(ratio));
                
                
            }
            else
            {
                algorithm.NaiveBayesAlgorithm();
            }
            break;

        case "2":
            Console.WriteLine("Logistic Regression algorithm selected");
            Console.WriteLine("Would you like to change the ratio of training data to testing data? Current ratio is 90% training to 10% testing of 100% data \n Select 'y' or 'n'");
            string choicetraining2 = Console.ReadLine();
            if (choicetraining2 == "y")
            {
                Console.WriteLine("Please enter numerical value for ratio from 0.00 to 1.00 for example 0.9 for 90% \n");
                string ratio2 = Console.ReadLine();
              
                    algorithm.LogisticRegression(double.Parse(ratio2));
              
            }
            else
            {
                algorithm.LogisticRegression();
            }
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

