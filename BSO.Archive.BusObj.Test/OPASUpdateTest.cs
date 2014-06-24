using Bso.Archive.BusObj;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BSO.Archive.BusObj.Test
{
    
    
    /// <summary>
    ///This is a test class for OPASUpdateTest and is intended
    ///to contain all OPASUpdateTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OPASUpdateTest
    {
        /// <summary>
        ///A test for OPASUpdate Constructor
        ///</summary>
        [TestMethod()]
        public void OPASUpdateConstructorTest()
        {
            OPASUpdate target = new OPASUpdate();

            target.UpdateOPASData();
        }
    }
}
