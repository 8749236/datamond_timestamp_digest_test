using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using timestamp_digest_test;

namespace timestamp_digest_test.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestDateString1()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var patchInfo = new Function.OCRPatchInfo();
            patchInfo.TextLine = "Date: 1/3/2020";
            var input = new Function.OCRPatchInfo[] { patchInfo };
            var upperCase = function.FunctionHandler(input, context);

            Assert.Equal("1/3/2020", upperCase);
        }
        [Fact]
        public void TestDateString2()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var patchInfo = new Function.OCRPatchInfo();
            patchInfo.TextLine = "ÈÕÆÚ£º 2021-01-22";
            var input = new Function.OCRPatchInfo[] { patchInfo };
            var upperCase = function.FunctionHandler(input, context);

            Assert.Equal("2021-01-22", upperCase);
        }
        [Fact]
        public void TestDateString3()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var patchInfo = new Function.OCRPatchInfo();
            patchInfo.TextLine = "Jan 14, 2020";
            var input = new Function.OCRPatchInfo[] { patchInfo };
            var upperCase = function.FunctionHandler(input, context);

            Assert.Equal("Jan 14, 2020", upperCase);
        }
    }
}
