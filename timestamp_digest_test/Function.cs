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
            public string TextLine;
            public string BoundingBox;
            public float PointX;
            public float PointY;
            public int length;
            public int height;
            public bool isProcess;
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
