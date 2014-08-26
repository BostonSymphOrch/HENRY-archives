using Bso.Archive.BusObj;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Bso.Archive.BusObj.Utility;

namespace BSO.Archive.BusObj.Test
{
    /// <summary>
    /// Summary description for Utility
    /// </summary>
    [TestClass]
    public class UtilityTest
    {
        [TestMethod]
        public void TestConcatString()
        {
            Assert.AreEqual("Test, Adage", Helper.ConcatString(new string[] { "Test", "Adage" }));

            Assert.AreEqual("Adage", Helper.ConcatString(new string[] { "", "Adage" }));

            Assert.AreEqual("Adage", Helper.ConcatString(new string[] { "Adage", String.Empty }));

            Assert.AreEqual("Adage", Helper.ConcatString(new string[] { "", "Adage", null }));

            Assert.AreEqual("Test, Adage", Helper.ConcatString(new string[] { "Test", "", "Adage" }));

            Assert.AreEqual("Test, Adage", Helper.ConcatString(new string[] { "Test", null, "Adage" }));

            Assert.AreEqual("Test, Adage, Tech.", Helper.ConcatString(new string[] { "Test", "Adage", "Tech." }));
        }

        [TestMethod]
        public void TestGetTabType()
        {
            Assert.AreEqual("#tabs-performance", Helper.GetTabType("performance"));

            Assert.AreEqual("#tabs-artist", Helper.GetTabType("artist"));

            Assert.AreEqual("#tabs-repertoire", Helper.GetTabType("repertoire"));
        }
    }
}
