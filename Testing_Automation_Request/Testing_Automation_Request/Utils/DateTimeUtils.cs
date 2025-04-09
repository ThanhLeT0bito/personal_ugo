using System;
namespace CloudBanking.Utilities
{
    public static class DateTimeUtils
    {
        public static int UnixTime()
        {
            TimeSpan ts = DateTime.UtcNow - UnixEpoch;
            return (int)Math.Round(ts.TotalSeconds);
        }

        public static DateTime FromUnixTime(int unixtime)
        {
            return UnixEpoch.AddSeconds(unixtime).ToLocalTime();
        }

        public static int? GetUtcOffsetInMinutes(this DateTime? localTime)
        {
            if (localTime == null || !localTime.HasValue)
            {
                return null;
            }

            var localTimeValue = localTime.Value;

            return (int)(localTimeValue - localTimeValue.ToUniversalTime()).TotalMinutes;
        }

        public static double TotalSecondOfTime(this DateTime dateTime) => dateTime.TimeOfDay.TotalSeconds;
        public static double TotalDate(this DateTime dateTime) => (dateTime.Date - new DateTime()).TotalDays;


        public static DateTime UnixEpoch { get { return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc); } }

        public static string FormatHour(this int iHour)
        {
            return DateTime.Today.Add(TimeSpan.FromHours(iHour)).ToString(GlobalConstants.FORMAT_TIME_HH_TT);
        }

        public static string FormatHourSecond(this long lHour, bool fHtml = false)
        {
            var strFormatHour = string.Empty;

            if (lHour == 0)

                strFormatHour = "12AM";

            else if (lHour <= 11)
            {
                if (lHour <= 9)
                    strFormatHour += (fHtml ? "&ensp;" : " ");

                strFormatHour += lHour.ToString();

                strFormatHour += "AM";
            }
            else if (lHour == 12)

                strFormatHour = "12PM";

            else if (lHour <= 23)
            {
                lHour -= 12;

                if (lHour <= 9)
                    strFormatHour += (fHtml ? "&ensp;" : " ");

                strFormatHour += lHour.ToString();

                strFormatHour += "PM";
            }

            return strFormatHour;
        }

        public static int GetAge(this DateTime bday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - bday.Year;
            if (now < bday.AddYears(age))
                age--;

            return age;
        }
    }
}
