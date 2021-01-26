using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using timestamp_digest_test;

namespace timestamp_digest_test.Tests
{
    public class DateStrRecognizerTest
    {
        [Fact]
        public void TestDateString1()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var input = new string[] { "Hello world", "Date: 1/3/2020" };
            var rslt = DateStrRecognizer.FindDateStr(input);

            Assert.Equal("1/3/2020", rslt);
        }
        [Fact]
        public void TestDateString2()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var input = new string[] { "Another format", "日期: 2016-11-24" };
            var rslt = DateStrRecognizer.FindDateStr(input);

            Assert.Equal("2016-11-24", rslt);
        }
        [Fact]
        public void TestDateString3()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var input = new string[] { "Today is Jan 26, 2021", "Completed on 1/3/2020" };
            var rslt = DateStrRecognizer.FindDateStr(input);

            Assert.Equal("Jan 26, 2021", rslt);
        }
    }
}
