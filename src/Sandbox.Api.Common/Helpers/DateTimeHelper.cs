namespace Sandbox.Api.Common.Helpers;

public static class DateTimeHelper
{
    /// <summary>
    /// Wrapper around DateTimeOffset.Now, unless otherwise configured using <see cref="Set"/>
    /// </summary>
    public static Func<DateTimeOffset> Now = () => DateTimeOffset.Now;

    /// <summary>
    /// Overrides the default DateTimeOffset.Now response with a custom value
    /// </summary>
    /// <param name="dateTimeOffset"></param>
    public static void Set(DateTimeOffset dateTimeOffset)
        => Now = () => dateTimeOffset;

    /// <summary>
    /// Resets the Now response to DateTimeOffset.Now
    /// </summary>
    public static void Reset()
        => Now = () => DateTimeOffset.Now;
}