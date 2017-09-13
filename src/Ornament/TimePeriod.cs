using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornament
{
    public class TimePeriod
    {
        public Time? Start { get; set; }
        public Time? End { get; set; }

        public bool In(DateTime dateTime)
        {
            var time = new Time(dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
            var start = this.Start ?? Time.Min;
            var end = this.End ?? Time.Max;
            return time >= start && time <= end;
        }

        public bool NotIn(DateTime dateTime)
        {
            var time = new Time(dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
            var start = this.Start ?? Time.Min;
            var end = this.End ?? Time.Max;
            return time < start && time > end;
        }
    }
}
