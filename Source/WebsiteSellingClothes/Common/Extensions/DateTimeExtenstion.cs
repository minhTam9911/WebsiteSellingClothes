namespace Common.Extensions;

public static class DateTimeExtenstion
{
    public static long GetTimeStamp(this DateTime dateTime)
    {
        return (long)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
    }
    
}
