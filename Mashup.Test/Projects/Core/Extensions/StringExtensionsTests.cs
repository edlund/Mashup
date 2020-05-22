using Mashup.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Mashup.Test.Projects.Core.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void Test_Chunk_ValidSizesGiven_CorrectChunk()
        {
            CollectionAssert.AreEqual(
                new List<string> { "a", "b", "c", "d", "e" },
                "abcde".Chunk(1).ToList());
            CollectionAssert.AreEqual(
                new List<string> { "abc", "de" },
                "abcde".Chunk(3).ToList());
        }

        [TestMethod]
        public void Test_SplitCamelCase_CamelCaseStringsGiven_CorrectSplit()
        {
            CollectionAssert.AreEqual(
                new List<string> { "Hello" },
                "Hello".SplitCamelCase().ToList());
            CollectionAssert.AreEqual(
                new List<string> { "Hello", "World" },
                "HelloWorld".SplitCamelCase().ToList());
            CollectionAssert.AreEqual(
                new List<string> { "Hello73", "World17" },
                "Hello73World17".SplitCamelCase().ToList());
            CollectionAssert.AreEqual(
                new List<string> { "Hello", "C", "Sharp", "World" },
                "HelloCSharpWorld".SplitCamelCase().ToList());
            CollectionAssert.AreEqual(
                new List<string> { "Super", "JSON", "Thingy" },
                "SuperJSONThingy".SplitCamelCase().ToList());
        }
    }
}
