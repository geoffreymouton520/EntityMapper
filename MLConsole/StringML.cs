using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.ML.SVM;
using Encog.ML.SVM.Training;
using Encog.ML.Train;

namespace MLConsole
{
    public class StringMl:ILearnable
    {
        private static readonly string[][] XorInputOriginal =
        {
            new[ ] { "the" } ,
            new[ ] { "cat" } ,
            new[ ] { "in" } ,
            new[ ] { "the"},
            new[ ] { "hat"}
        };

        private static readonly string[][] XorIdealOriginal =
        {
            new[ ] { "The" } ,
            new[ ] { "Cat" } ,
            new[ ] { "In" } ,
            new[ ] { "The"},
            new[ ] { "Hat"}
        };
        
        private readonly List<char> _alphas = new List<char>
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        }; 

        public void Learn()
        {
            var xorInput = Normalize(XorInputOriginal[1][0]);
        
            var classificationIdeal = Classify(XorIdealOriginal[1][0]);

            var svm = new SupportVectorMachine(2, false); 
            IMLDataSet trainingSet = new BasicMLDataSet(xorInput, classificationIdeal);
            IMLTrain train = new SVMSearchTrain(svm, trainingSet);

            var epoch = 1;

            do
            {
                train.Iteration();
                Console.WriteLine(@"Epoch #" + epoch + @" Error:" + train.Error);
                epoch++;
            } while (train.Error > 0.01);
            
            Console.WriteLine(@"SVM Results:");


            var inputString = new StringBuilder();
            var idealString = new StringBuilder();
            var outputString = new StringBuilder();


            foreach (var pair in trainingSet)
            {
                var output = svm.Compute(pair.Input);
                outputString.Append(_alphas[(int)output[0]]);
                inputString.Append(Denormalize(pair.Input));
                idealString.Append(_alphas[(int)pair.Ideal[0]]);

            }
            Console.WriteLine("input=" + inputString + @" , " +@" , actual=" + outputString + @" , ideal=" + idealString);

            Console.WriteLine("Done");
        }

        private double[][] Normalize(string value)
        {
            return value.Select(character => _alphas.Select(alpha => alpha.Equals(character) ? 1D : 0D).ToArray()).ToArray();
        }

        private char Denormalize(IMLData value)
        {
            var query = new double[value.Count];
            value.CopyTo(query, 0, value.Count);
            var index = query.ToList().IndexOf(1D);
            return _alphas[index];

        }

        private double[][] Classify(string value)
        {
            var array = new double[value.Length][];
            var index = 0;
            foreach (var character in value)
            {
                array[index] = new double[] { _alphas.IndexOf(character) };
                index++;
            }
            return array;
        }
    }
}
