using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace _219003234_Naidoo_KN_AAI
{
    class Program
    {
        static void Main(string[] args)
        {

           ImplementationOfAlgorithms algorithm = new ImplementationOfAlgorithms();
            //can use doubles to cut the training data into testing data as well
            //algorithm.NaiveBayesAlgorithm(0.99);
            // algorithm.NaiveBayesAlgorithm();


            algorithm.RunLogisticRegression(0.9);

        }

    }
}
