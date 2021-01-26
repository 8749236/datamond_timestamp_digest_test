using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace timestamp_digest_test
{
    public class Function
    {

        public class OCRPatchInfo {
            public string TextLine { get; set; }
            public string BoundingBox { get; set; }
            public float PointX { get; set; }
            public float PointY { get; set; }
            public int Length { get; set; }
            public int Height { get; set; }
            public bool IsProcess { get; set; }
        }
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(Function.OCRPatchInfo[] textPatches, ILambdaContext context)
        {
            var potentialDateStrings = textPatches.Select(patch => patch.TextLine);
            return DateStrRecognizer.FindDateStr(potentialDateStrings.ToArray());
        }
    }
}
