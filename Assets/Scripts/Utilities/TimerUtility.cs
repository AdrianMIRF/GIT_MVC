using System;

public static class TimerUtility
{
    const double nrOfSecInOneDay = 3600 * 24;
    static DateTime firstDt = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

    public static DateTime CurrentTime
    {
        get
        {
            return DateTime.UtcNow;
        }
    }

    public static void SetFirstDT(int hourOfDayForReward)
    {
        firstDt = new DateTime(CurrentTime.Year, CurrentTime.Month, CurrentTime.Day, hourOfDayForReward, 0, 0, 0, System.DateTimeKind.Utc);
    }

    public static void SetFirstDT(DateTime dateTime)
    {
        firstDt = dateTime;
    }

    public static DateTime GetFirstDT()
    {
        return firstDt;
    }

    public static DateTime GetNextDT(int hourOfDayForReward)
    {
        DateTime nextDay = CurrentTime.AddDays(1);
        DateTime dateTime = new DateTime(nextDay.Year, nextDay.Month, nextDay.Day, hourOfDayForReward, 0, 0, 0, System.DateTimeKind.Utc);
        return dateTime;
    }

    public static DateTime ConvertTimestampToDateTime(double timestamp)
    {
        return firstDt.AddSeconds(timestamp);
    }

    public static DateTime ConvertDateFromString(string dateString)
    {
        DateTime dateTime = DateTime.Parse(dateString);
        return dateTime;
    }

    public static double ConvertDateTimeToTimestamp(DateTime dateTime)
    {
        return (dateTime - firstDt).TotalSeconds;
    }

    public static string GetFormatedTimeToCoolDown(double totalSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);

        if (totalSeconds > nrOfSecInOneDay)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
        else
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}
