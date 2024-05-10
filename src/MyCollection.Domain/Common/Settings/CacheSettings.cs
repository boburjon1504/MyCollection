namespace MyCollection.Domain.Common.Settings;
public class CacheSettings
{
    public int AbsoluteExpirationTimeInSeconds { get; set; }

    public int SlidingExpirationTimeInSeconds { get; set; }
}
