using System;
using Encog;
using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.ML.Train;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.ML.SVM;
using Encog.ML.SVM.Training;

namespace MLConsole
{
    public class DoubleMl:ILearnable
    {

        private static readonly double[][] XorInput =
        {
            new[ ] { 0.0 , 0.0 } ,
            new[ ] { 1.0 , 0.0 } ,
            new[ ] { 0.0 , 1.0 } ,
            new[ ] { 1.0 , 1.0}
        };

        private static readonly double[][] XorIdeal =
        {
            new[ ] { 0.0 } ,
            new[ ] { 1.0 } ,
            new[ ] { 1.0 } ,
            new[ ] {0.0}
        };

        public void Learn()
        {
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, true, 2));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(),
                true, 3));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(),
                false, 1));
            network.Structure.FinalizeStructure();
            network.Reset();
            // c r e a t e t r a i n i n g data
            IMLDataSet trainingSet = new BasicMLDataSet(XorInput,
                XorIdeal);
            // t r a i n the neural network
            IMLTrain train = new ResilientPropagation(network,
                trainingSet);
            var epoch = 1;
            do
            {
                train.Iteration();
                Console.WriteLine(@"Epoch #" + epoch + @" Error : " + train.
                    Error);
                epoch++;
            } while (train.Error > 0.01);
            train.FinishTraining();
            // t e s t the neural network
            Console.WriteLine(@"Neural Network Results : ");
            foreach (var pair in trainingSet)
            {
                var output = network.Compute(pair.Input);
                Console.WriteLine(pair.Input[0] + @" , " + pair.Input[1]
                                  + @" , actual=" + output[0] + @" , ideal=" + pair.Ideal[0]);
            }
            EncogFramework.Instance.Shutdown();
            Console.ReadLine();
        }
    }

    //class Program2
    //{
    //    /// 
    //    /// Input for function, normalized to 0 to 1.
    //    /// 
    //    public static double[][] ClassificationInput = {
    //        new[] {0.0, 0.0},
    //        new[] {0.1, 0.0},
    //        new[] {0.2, 0.0},
    //        new[] {0.3, 0.0},
    //        new[] {0.4, 0.5},
    //        new[] {0.5, 0.5},
    //        new[] {0.6, 0.5},
    //        new[] {0.7, 0.5},
    //        new[] {0.8, 0.5},
    //        new[] {0.9, 0.5}
    //        };

    //    /// 
    //    /// Ideal output, these are class numbers, a total of four classes here (0,1,2,3).
    //    /// DO NOT USE FRACTIONAL CLASSES (i.e. there is no class 1.5)
    //    /// 
    //    public static double[][] ClassificationIdeal = {
    //        new[] {0.0},
    //        new[] {0.0},
    //        new[] {0.0},
    //        new[] {0.0},
    //        new[] {1.0},
    //        new[] {1.0},
    //        new[] {2.0},
    //        new[] {2.0},
    //        new[] {3.0},
    //        new[] {3.0}
    //    };

    //    static void Main(string[] args)
    //    {
    //        // create a neural network, without using a factory
    //        var svm = new SupportVectorMachine(2, false); // 2 input, & false for classification

    //        // create training data
    //        IMLDataSet trainingSet = new BasicMLDataSet(ClassificationInput, ClassificationIdeal);

    //        // train the SVM
    //        IMLTrain train = new SVMSearchTrain(svm, trainingSet);

    //        int epoch = 1;

    //        do
    //        {
    //            train.Iteration();
    //            Console.WriteLine(@"Epoch #" + epoch + @" Error:" + train.Error);
    //            epoch++;
    //        } while (train.Error > 0.01);

    //        // test the SVM
    //        Console.WriteLine(@"SVM Results:");
    //        foreach (IMLDataPair pair in trainingSet)
    //        {
    //            IMLData output = svm.Compute(pair.Input);
    //            Console.WriteLine(pair.Input[0]
    //                              + @", actual=" + output[0] + @",ideal=" + pair.Ideal[0]);
    //        }

    //        Console.WriteLine("Done");
    //    }
    //}
}