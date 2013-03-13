using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RightScale.netClient.Test.objects
{
    [TestClass]
    public class ScheduleEntryTest
    {
        [TestMethod]
        public void MondayTest()
        {
            ScheduleEntry se = new ScheduleEntry("00:14", "Monday", "1", "5");
            Assert.IsNotNull(se);
        }

        [TestMethod]
        public void TuesdayTest()
        {
            ScheduleEntry se = new ScheduleEntry("10:01", "Tuesday", "5", "10");
            Assert.IsNotNull(se);
        }

        [TestMethod]
        public void WednesdayTest()
        {
            ScheduleEntry se = new ScheduleEntry("12:01", "Wednesday", "5", "10");
            Assert.IsNotNull(se);
        }

        [TestMethod]
        public void ThursdayTest()
        {
            ScheduleEntry se = new ScheduleEntry("14:01", "Thursday", "5", "10");
            Assert.IsNotNull(se);
        }

        [TestMethod]
        public void FridayTest()
        {
            ScheduleEntry se = new ScheduleEntry("09:10", "Friday", "5", "10");
            Assert.IsNotNull(se);
        }

        [TestMethod]
        public void SaturdayTest()
        {
            ScheduleEntry se = new ScheduleEntry("23:01", "Saturday", "5", "10");
            Assert.IsNotNull(se);
        }

        [TestMethod]
        public void SundayTest()
        {
            ScheduleEntry se = new ScheduleEntry("19:01", "Sunday", "5", "10");
            Assert.IsNotNull(se);
        }


        [TestMethod]
        public void BadTuesdayTest()
        {
            try
            {
                ScheduleEntry se = new ScheduleEntry("10:01", "tuesday", "5", "10");
                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
    }
}
