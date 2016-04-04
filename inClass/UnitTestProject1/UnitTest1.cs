using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using inClass;
/*
 * Csv is a spreadsheet format
 * Csv stands for comma seperated values
 * Formaet lookds like
 * row0col0,row0col1rawocol2
 * row1col0,row1col1,row1col2
 * */
using UnitTestProject1;



namespace UnitTestProject1
{
    [TestClass]
    public class CsvUnitTest
    {
        [TestMethod]
        public void ParseSimpleRowTest()
        {
            CsvParser reader = new CsvParser();
            var result = reader.ParseRow("one,two");

            // from the above, we expect two entries
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("one", result[0]);
            Assert.AreEqual("two", result[1]);
            // how to check cover result?
        }

        [TestMethod]
        public void EmptySimpleRowTest()
        {
            CsvParser parser = new CsvParser();
            var result = parser.ParseRow(",,,");
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual("", result[0]);
            Assert.AreEqual("", result[1]);
        }

        [TestMethod]
        public void EmbeddedCommaTest()
        {
            CsvParser parser = new CsvParser();
            var result = parser.ParseRow("\"Smith, Bob\",A");
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Smith, Bob", result[0]);
            Assert.AreEqual("A", result[1]);
        }

        [TestMethod]
        public void EmbeddedQuoteTest()
        {
            CsvParser parser = new CsvParser();
            var result = parser.ParseRow("\"\"\"H\"\"ello\",A");
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("\"H\"ello", result[0]);
            Assert.AreEqual("A", result[1]);
        }

        [TestMethod]
        public void EmbeddedQuoteAndCommaTest()
        {
            CsvParser parser = new CsvParser();
            var result = parser.ParseRow("\"\"\"H\"\"ello\",A");
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("\"H\"ello", result[0]);
            Assert.AreEqual("A", result[1]);
        }
    }
}
