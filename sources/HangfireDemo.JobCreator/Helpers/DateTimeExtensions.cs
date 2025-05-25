namespace DustInTheWind.HangfireDemo.JobCreator.Helpers;

internal static class DateTimeExtensions
{
    public static string ToCustomString(this DateTime dateTime)
    {
        return dateTime.ToString() + dateTime.Kind switch
        {
            DateTimeKind.Unspecified => string.Empty,
            DateTimeKind.Utc => " UTC",
            DateTimeKind.Local => " Local",
            _ => throw new NotImplementedException()
        };
    }
}
