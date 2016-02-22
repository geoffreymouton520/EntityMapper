using System.Collections.Generic;
using Data.Models;
using FuzzyString;

namespace Learning
{
    public class LearningSession
    {
        //private readonly ILearningStrategyIdentifier _identifier;
        //private readonly TrainingData _trainingData;
        private readonly UntrainedData _untrainedData;
        //private readonly IContains _contains;

        public LearningSession(UntrainedData untrainedData)
        {
            //_identifier = identifier;
            //_trainingData = trainingData;
            _untrainedData = untrainedData;
            //_contains = contains;
        }


        public LearningResult StartLearning()
        {
            var learntMappings = new List<LearntMapping>();
            //var weightStrategies = new WeighedStrategies();
            //foreach (var mapping in _trainingData)
            //{
            //    var strategy = _identifier.Identify(mapping);
            //    weightStrategies.Add(strategy);
            //}
            foreach (var source in _untrainedData.Sources)
            {
                foreach (var destination in _untrainedData.Destinations)
                {
                    if (source.ApproximatelyEquals(destination, new List<FuzzyStringComparisonOptions> { FuzzyStringComparisonOptions.UseLongestCommonSubsequence }, FuzzyStringComparisonTolerance.Strong))
                    {
                        learntMappings.Add(new LearntMapping
                        {
                            Mapping = new Mapping
                            {

                                Source = source,
                                Destination = destination
                            }
                        });
                    }
                }
                //foreach (var strategy in weightStrategies.Strategies)
                //{
                //    var resultingDestination = strategy.Apply(source);
                //    if(_contains.Contains(_untrainedData.Destinations,resultingDestination))
                //    //if (_untrainedData.Destinations.Any(t => t.Equals(resultingDestination)))
                //    {
                //        learntMappings.Add(new LearntMapping
                //        {
                //            Mapping = new Mapping
                //            {
                //                Source = source,
                //                Destination = resultingDestination
                //            }
                //        });
                //        break;
                //    }
                //}
            }


            return new LearningResult(learntMappings);
        }


    }
}