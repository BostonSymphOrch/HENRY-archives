using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bso.Archive.BusObj;

namespace BSO.Archive.BusObj.Test.Editable
{
    [TestClass]
    public class EventWorkTest
    {
        /// <summary>
        /// Test the AddEventWorkTest method
        /// </summary>
        [TestMethod]
        public void AddEventWorkTest()
        {
            Event evt = Event.NewEvent();
            evt.EventID = 1;
            Work work = Work.NewWork();
            work.WorkID = 1;
            
            EventWork eventWork = EventWork.AddEventWork(evt, work);
            Assert.IsNotNull(eventWork);
            Assert.IsTrue(eventWork.WorkID == work.WorkID && eventWork.EventID == evt.EventID);
        }
    }
}
