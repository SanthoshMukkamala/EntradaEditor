using DevExpress.XtraBars.Docking2010.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.Utils;
using Entrada.Entities;

namespace Entrada.Editor
{
    public static class MiscellaneousExtensions
	{
        public static DefaultBoolean ToDefaultBool (this bool value)
        {
            return value ? DefaultBoolean.True : DefaultBoolean.False;
        }

        public static bool ToBool (this DefaultBoolean value)
        {
            switch (value) {
                case DefaultBoolean.False:
                case DefaultBoolean.Default:
                default:
                    return false;
                case DefaultBoolean.True:
                    return true;
            }
        }

        public static string FormatDuration (int duration)
        {
            var ts = new TimeSpan (0, 0, duration);

            return ts.ToString ("c");
        }

        public static string FormatTAT (DateTime time)
        {
            var tat = time.Subtract (DateTime.Now);
            
            var tat_str = tat.ToFriendlySpan ();

            if (tat.TotalMilliseconds < 0)
                tat_str += " ago";

            return tat_str;            
        }

        private static string Plural (string format, string singular, int value)
        {
            if (value == 1)
                return string.Format (format, value, singular);

            return string.Format (format + "s", value, singular);
        }

        public static string ToFriendlySpan (this TimeSpan diff)
        {
            if (diff.TotalMilliseconds < 0)
                diff = diff.Negate ();

            if (diff.TotalDays >= 365)
                return Plural ("{0} {1}", "year", diff.Days / 365);
            if (diff.TotalDays >= 60)
                return Plural ("{0} {1}", "month", diff.Days / 30);
            if (diff.TotalDays >= 14)
                return Plural ("{0} {1}", "week", diff.Days / 7);
            if (diff.TotalDays >= 2)
                return Plural ("{0} {1}", "day", diff.Days);
            if (diff.TotalHours >= 1)
                return Plural ("{0} {1}", "hour", (int)diff.TotalHours);
            if (diff.TotalMinutes >= 1)
                return Plural ("{0} {1}", "minute", (int)diff.TotalMinutes);

            return Plural ("{0} {1}", "second", diff.Seconds);
        }
    }
}
