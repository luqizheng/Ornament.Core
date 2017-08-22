using System;
using Xunit;

namespace Ornament.UnitTest
{
    /// <summary>
    ///This is a test class for TimeTest and is intended
    ///to contain all TimeTest Unit Tests
    ///</summary>
   
    public class TimeTest
    {
       

        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [Fact]
        public void op_SubtractionTest()
        {
            var a = new Time(23, 59, 59);
            var b = new Time(23, 0, 0);
            var expected = new TimeSpan(0, 59, 59);
            TimeSpan actual;
            actual = (a - b);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for AddSecond
        ///</summary>
        [Fact]
        public void AddSecondTest()
        {
            int hour = 8;
            int mins = 0;
            int second = 1;
            var target = new Time(hour, mins, second);
            int second1 = 1;
            target = target.AddSeconds(second1);

            var expect = new TimeSpan(hour, mins, second1 + second);
            Assert.Equal(expect.Ticks, target.Ticks);
        }

        /// <summary>
        ///A test for AddMins
        ///</summary>
        [Fact]
        public void AddMinsTest()
        {
            int hour = 8;
            int mins = 9;
            int second = 1;
            var target = new Time(hour, mins, second);
            int min = 55;
            target = target.AddMinutes(min);
            var expect = new TimeSpan(hour, (min + mins), second);
            Assert.Equal(expect.Ticks, target.Ticks);
        }

        /// <summary>
        ///A test for AddMillsecond
        ///</summary>
        [Fact]
        public void AddMillsecondTest()
        {
            int hour = 1;
            int mins = 2;
            int second = 3;
            var target = new Time(hour, mins, second);
            int millsecond = 3;
            target = target.AddMillseconds(millsecond);
            var expcet = new TimeSpan(0, 1, 2, 3, 3);
            Assert.Equal(expcet.Ticks, expcet.Ticks);
        }

        /// <summary>
        ///A test for AddHour
        ///</summary>
        [Fact]
        public void AddHourTest()
        {
            int hour = 1;
            int mins = 2;
            int second = 3;
            var target = new Time(hour, mins, second);
            int hour1 = 3;
            target.AddHours(hour1);
            var expect = new DateTime(1, 1, 1, hour, mins, second);
            expect.AddHours(hour1);

            Assert.Equal(expect.Ticks, target.Ticks);
        }
        [Fact]
        public void AddHourMoreThan_23_hour()
        {
            int hour = 1;
            int mins = 0;
            int second = 0;
            var target = new Time(hour, mins, second);
            int hour1 = 29;
            target = target.AddHours(hour1);

            Assert.Equal("6:0:0", target.ToString());
        }


        /// <summary>
        ///A test for Time Constructor
        ///</summary>
        [Fact]
        public void TimeConstructorTest1()
        {
            int hour = 1;
            int mins = 2;
            int second = 3;
            var target = new Time(hour, mins, second);
            var except = new DateTime(1, 1, 1, hour, mins, second);
            Assert.Equal(except.Ticks, target.Ticks);
        }

        /// <summary>
        ///A test for Time Constructor
        ///</summary>
        [Fact]
        public void TimeConstructorTest()
        {
            int hour = 1;
            int mins = 2;
            int second = 3;
            int millsecond = 1;
            var target = new Time(hour, mins, second, millsecond);
            var except = new DateTime(1, 1, 1, hour, mins, second, millsecond);
            Assert.Equal(except.Ticks, target.Ticks);
        }

        /// <summary>
        ///A test for Ticks
        ///</summary>
        [Fact]
        public void TicksTest()
        {
            var target = new Time(1, 0, 0);
            long actual;
            actual = target.Ticks;
            var expect = new TimeSpan(1, 0, 0);

            Assert.Equal(expect.Ticks, target.Ticks);
        }

        /// <summary>
        ///A test for Seconds
        ///</summary>
        [Fact]
        public void SecondsTest()
        {
            var target = new Time(23, 1, 0);
            int actual;
            actual = target.Seconds;
            Assert.Equal(actual, 0);

        }

        /// <summary>
        ///A test for Minutes
        ///</summary>
        [Fact]
        public void MinutesTest()
        {
            var target = new Time(0, 23, 0);
            int actual;
            actual = target.Minutes;
            Assert.Equal(23, actual);
        }

        /// <summary>
        ///A test for Millseconds
        ///</summary>
        [Fact]
        public void MillsecondsTest()
        {
            var target = new Time(1, 2, 3, 4);
            int actual;
            actual = target.Millseconds;
            Assert.Equal(4, actual);
        }

        /// <summary>
        ///A test for Hours
        ///</summary>
        [Fact]
        public void HoursTest()
        {
            var target = new Time(1, 0, 0);
            int actual;
            actual = target.Hours;
            Assert.Equal(actual, 1);
        }

        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [Fact]
        public void op_SubtractionTest1()
        {
            var a = new Time(2, 2, 2);
            var b = new Time(1, 1, 1);
            var expected = new TimeSpan(1, 1, 1);
            TimeSpan actual;
            actual = (a - b);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [Fact]
        public void op_AdditionTest()
        {
            var a = new Time(23, 0, 0);
            var b = new Time(2, 0, 0);
            var expected = new TimeSpan(25, 0, 0);
            TimeSpan actual;
            actual = (a + b);
            Assert.Equal(expected, actual);

        }




        /// <summary>
        ///A test for Time Constructor
        ///</summary>
        [Fact]
        public void TimeConstructorTest2()
        {
            var expect = new TimeSpan(23, 22, 22);
            long ticks = expect.Ticks;
            var target = new Time(ticks);
            Assert.Equal(expect.Ticks, target.Ticks);
        }


        ///// <summary>
        /////A test for CheckTimeSpanBound
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("Qi.dll")]
        //public void CheckTimeSpanBoundTest()
        //{
        //    TimeSpan time = new TimeSpan(10, 23, 2, 3);
        //    TimeSpan expected = new TimeSpan(23, 2, 3);
        //    TimeSpan actual;
        //    actual = Time_Accessor.CheckTimeSpanBound(time);
        //    Assert.Equal(expected, actual);

        //}
    }
}