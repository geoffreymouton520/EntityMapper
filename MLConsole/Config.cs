using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encog.Util.File;

namespace MLConsole
{
    public static class Config
    {
        public static FileInfo BasePath = new FileInfo(@"C:\Users\geoffrey\OneDrive\Documents\Personal");

        public static FileInfo BaseFile = FileUtil.CombinePath(BasePath, "New_TrainData.csv");
        public static FileInfo ShuffledBaseFile = FileUtil.CombinePath(BasePath, "TrainingData_Shuffled.csv");
        public static FileInfo TrainingFile = FileUtil.CombinePath(BasePath,"TrainingFile.csv");
        public static FileInfo EvaluateFile = FileUtil.CombinePath(BasePath,"EvaluateFile.csv");
        public static FileInfo NormalizedTrainingFile = FileUtil.CombinePath(BasePath, "Train_Norm.csv");
        public static FileInfo NormalizedEvaluateFile = FileUtil.CombinePath(BasePath, "Eval_Norm.csv");
        public static FileInfo AnalystFile = FileUtil.CombinePath(BasePath, "Analyst.ega");
        public static FileInfo TrainedNetworkFile = FileUtil.CombinePath(BasePath, "Trained.eg");

    }
}
