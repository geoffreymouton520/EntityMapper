using System;
using System.Collections.Generic;
using System.IO;
using Encog;
using Encog.App.Analyst;
using Encog.App.Analyst.CSV.Normalize;
using Encog.App.Analyst.CSV.Segregate;
using Encog.App.Analyst.CSV.Shuffle;
using Encog.App.Analyst.Script.Normalize;
using Encog.App.Analyst.Wizard;
using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.ML.Train;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.Persist;
using Encog.Util.CSV;
using Encog.Util.Simple;

namespace MLConsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("*** Step 1 ***");
            Step1();
            Console.WriteLine("*** Step 2 ***");
            Step2();
            Console.WriteLine("*** Step 3 ***");
            Step3();
            Console.WriteLine("*** Step 4 ***");
            Step4();
            Console.WriteLine("*** Step 5 ***");
            Step5();
            Console.WriteLine("*** Step 6 ***");
            Step6();
            Console.WriteLine("Press any key to exit..");
            Console.ReadLine();

            #region Old Code

            //var leason = new StringMl();
            //leason.Learn(); 

            #endregion
        }

        private static void Step6()
        {
            Console.WriteLine("Step 6: Evaluate Network");
            Evaluate();
        }

        private static void Evaluate()
        {
            var network = (BasicNetwork)EncogDirectoryPersistence.LoadObject(Config.TrainedNetworkFile);
            var analyst = new EncogAnalyst();
            analyst.Load(Config.AnalystFile.ToString());
            var evaluationSet = EncogUtility.LoadCSV2Memory(Config.NormalizedEvaluateFile.ToString(), network.InputCount,
                network.OutputCount, true, CSVFormat.English, false);

            var count = 0;
            var correctCount = 0;
            foreach (var item in evaluationSet)
            {
                count++;
                var output = network.Compute(item.Input);
                var analystNormalize = analyst.Script.Normalize;
                var normalizedFields = analystNormalize.NormalizedFields;
                var sourceElement = normalizedFields[0].DeNormalize(item.Input[0]);
                var destinationElement = normalizedFields[1].DeNormalize(item.Input[1]);

                var classField = normalizedFields[2];
                var classCount = classField.Classes.Count;
                var normalizationHigh = classField.NormalizedHigh;
                var normalizationLow = classField.NormalizedLow;

                var eq = new Encog.MathUtil.Equilateral(classCount,normalizationHigh,normalizationLow);
                var predictedClassInt = eq.Decode(output);
                var predictedClass = classField.Classes[predictedClassInt].Name;
                var idealClassInt = eq.Decode(output);
                var idealClass = classField.Classes[predictedClassInt].Name;

                if (predictedClassInt == idealClassInt)
                {
                    correctCount++;
                }
                Console.WriteLine("Count :{0} Properties [{1},{2}] ,Ideal : {3} Predicted : {4}",count,sourceElement,destinationElement,idealClass,predictedClass);
            }
            Console.WriteLine("Total Test Count : {0}",count);
            Console.WriteLine("Total Correct Predicted Count  : {0}",correctCount);
            Console.WriteLine("% Success : {0}",((correctCount*100.0)/count));
        }

        private static void Step5()
        {
            Console.WriteLine("Step 5: Train Neural Network");
            TrainNetwork();
        }

        private static void TrainNetwork()
        {
            var network = (BasicNetwork) EncogDirectoryPersistence.LoadObject(Config.TrainedNetworkFile);
            var trainingSet = EncogUtility.LoadCSV2Memory(Config.NormalizedTrainingFile.ToString(), network.InputCount,
                network.OutputCount, true, CSVFormat.English, false);

            var train = new ResilientPropagation(network,trainingSet);
            var epoch = 1;
            do
            {
                train.Iteration();
                Console.WriteLine("Epoch : {0} Error : {1}",epoch,train.Error);
                epoch++;
            } while (train.Error>0.01);
            EncogDirectoryPersistence.SaveObject(Config.TrainedNetworkFile, network);
        }

        private static void Step4()
        {
            Console.WriteLine("Step 4: Create Neural Network");
            CreateNetwork(Config.TrainedNetworkFile);
        }

        private static void CreateNetwork(FileInfo networkFile)
        {
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null,true,2));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(),true,3));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(),false,1));
            network.Structure.FinalizeStructure();
            network.Reset();
            EncogDirectoryPersistence.SaveObject(networkFile,network);
        }

        private static void Step3()
        {
            Console.WriteLine("Step 3: Normalize Training and Evaluation Data");

            //Analyst
            var analyst = new EncogAnalyst();

            //Wizard
            var wizard = new AnalystWizard(analyst);
            wizard.Wizard(Config.BaseFile, true, AnalystFileFormat.DecpntComma);

            //Norm for Training
            var norm = new AnalystNormalizeCSV();
            norm.Analyze(Config.TrainingFile, true, CSVFormat.English, analyst);
            norm.ProduceOutputHeaders = true;
            norm.Normalize(Config.NormalizedTrainingFile);

            //Norm for evaluation
            norm.Analyze(Config.EvaluateFile, true, CSVFormat.English, analyst);
            norm.ProduceOutputHeaders = true;
            norm.Normalize(Config.NormalizedEvaluateFile);

            analyst.Save(Config.AnalystFile);

        }

        private static void Step2()
        {
            Console.WriteLine("Step 2 Generate training and Evaluation file");
            Segregate(Config.ShuffledBaseFile);
        }

        private static void Segregate(FileInfo source)
        {
            var seg =  new SegregateCSV();
            seg.Targets.Add(new SegregateTargetPercent(Config.TrainingFile,75));
            seg.Targets.Add(new SegregateTargetPercent(Config.EvaluateFile,25));
            seg.ProduceOutputHeaders = true;
            seg.Analyze(source,true,CSVFormat.English);
            seg.Process();
        }

        private static void Step1()
        {
           Console.WriteLine("Step 1: Shuffle CSV Data File");
            Shuffle(Config.BaseFile);
        }

        private static void Shuffle(FileInfo source)
        {
            var shuffle = new ShuffleCSV();
            shuffle.Analyze(source,true,CSVFormat.English);
            shuffle.ProduceOutputHeaders = true;
            shuffle.Process(Config.ShuffledBaseFile);
        }
    }
}




